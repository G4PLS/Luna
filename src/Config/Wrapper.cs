using System;
using BepInEx.Configuration;

namespace Luna.Config;

public class Wrapper<T>
{
    public delegate void SettingChanged(object sender, ConfigEntryBase configEntryBase);
    public event SettingChanged OnSettingChanged;
    
    public ConfigEntry<T> ConfigEntry { get; }
    public T Value
    {
        get => ConfigEntry.Value;
        set => ConfigEntry.Value = value;
    }
    public object BoxedValue
    {
        get => ConfigEntry.BoxedValue;
        set => ConfigEntry.BoxedValue = value;
    }
    public object DefaultValue => ConfigEntry.DefaultValue;
    public ConfigFile ConfigFile => ConfigEntry.ConfigFile;
    public Type SettingType => ConfigEntry.SettingType;
    public ConfigDefinition Definition => ConfigEntry.Definition;
    public ConfigDescription Description => ConfigEntry.Description;

    public Wrapper(ConfigFile configFile, string section, string key, T defaultValue, string description, SettingChanged settingChanged)
    {
        ConfigEntry = configFile.Bind(section, key, defaultValue, description);
        ConfigEntry.SettingChanged += (sender, eventArgs) =>
        {
            if (eventArgs is not SettingChangedEventArgs settingChangedEventArgs) return;
            
            settingChanged.Invoke(sender, settingChangedEventArgs.ChangedSetting);
            OnSettingChanged?.Invoke(sender, settingChangedEventArgs.ChangedSetting);
        };
    }

    public void ResetValue() => BoxedValue = DefaultValue;
}