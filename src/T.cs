using System.Collections.Generic;

namespace Project_Luna
{
    public class T
    {
        public List<double> TimePerLevels = new();

        public double CurrentLevelTime = -1;

        public double TotalTime()
        {
            double acc = 0;

            for (int i = 0; i < TimePerLevels.Count; i++)
            {
                acc += TimePerLevels[i];
            }
            acc += CurrentLevelTime;

            return acc;
        }
    }
}
