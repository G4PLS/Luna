using HarmonyLib;
using KinematicCharacterController;
using UnityEngine;

namespace Luna
{
    //!IMPORTANT! Under Player->attackTrailMesh is the part that collects the cranes

    /// <summary>
    /// Implements Fields and Functions for the PlayerController Class.
    /// When a Field returns 0 or false one of the reasons could be that the controller is not set. 
    /// To check the Controller the Function NotNull exists
    /// </summary>
    public static class Player
    {
        public static Bun.PlayerController Controller { get; private set; }
        private static KinematicCharacterMotor _kinematic;

        /// <summary>
        /// Function to check if an Actual Player Controller exists in the scene
        /// </summary>
        /// <returns>True when a controller exists. False when no controller is in the scene</returns>
        public static bool NotNull() => Controller != null;

        #region FIX FIELDS

        /// <summary>
        /// The Speed when Sprinting
        /// </summary>
        public static float SprintSpeed
        {
            get => Controller == null ? Controller.sprintSpeed : 0f;
            set
            {
                if (Controller != null)
                    Controller.sprintSpeed = value;
            }
        }

        /// <summary>
        /// The Speed when Walking
        /// </summary>
        public static float RunSpeed
        {
            get => Controller == null ? Controller.runSpeed : 0f;
            set
            {
                if (Controller != null)
                    Controller.runSpeed = value;
            }
        }

        /// <summary>
        /// The Speed when on a Booser Block
        /// </summary>
        public static float TurboSpeed
        {
            get => Controller == null ? Controller.turboSpeed : 0f;
            set
            {
                if (Controller != null)
                    Controller.turboSpeed = value;
            }
        }

        /// <summary>
        /// The Acceleration to reach max Speed
        /// </summary>
        public static float Acceleration
        {
            get => Controller == null ? Controller.acceleration : 0f;
            set
            {
                if (Controller != null)
                    Controller.acceleration = value;
            }
        }

        /// <summary>
        /// How fast hana will reach the walking direction
        /// Lower value = Character reaches direction faster
        /// </summary>
        public static float RotationDampening
        {
            get => Controller != null ? Controller.rotationDamp : 0f;
            set
            {
                if (Controller != null)
                    Controller.rotationDamp = value;
            }
        }

        /// <summary>
        /// IDK
        /// </summary>
        public static float GroundRotationDamp
        {
            get => Controller != null ? Controller.groundRotationDamp : 0f;
            set
            {
                if (Controller != null)
                    Controller.groundRotationDamp = value;
            }
        }

        /// <summary>
        /// How fast you loose speed when jumping out of a rail
        /// Lower value = More speed is being kept when jumpin out of it
        /// </summary>
        public static float RailBoostDampening
        {
            get => Controller != null ? Controller.railBoostDamp : 0f;
            set
            {
                if (Controller != null)
                    Controller.railBoostDamp = value;
            }
        }

        // Bun.PlayerController.PushSpeed //IS ALWAYS OVERRIDDEN
        //controller.pushBuffer // Is Probably not implemented because there is no _current field for it

        /// <summary>
        /// Friction after Being pushed
        /// Lower Value = Slower speed decrease
        /// No check for negative values = Weird player behaviour
        /// </summary>
        public static float PushFriction
        {
            get => Controller != null ? Controller.pushFriction : 0f;
            set
            {
                if (Controller != null)
                    Controller.pushFriction = value;
            }
        }

        /// <summary>
        /// The Time it takes to Jump again
        /// </summary>
        public static float JumpBuffer
        {
            get => Controller != null ? Controller.jumpBuffer : 0f;
            set
            {
                if (Controller != null)
                    Controller.jumpBuffer = value;
            }
        }

        /// <summary>
        /// The friction the player has when on the ground
        /// </summary>
        public static float Friction
        {
            get => Controller != null ? Controller.friction : 0f;
            set
            {
                if (Controller != null)
                    Controller.friction = value;
            }
        }

        /// <summary>
        /// The friciton the player has when in the air
        /// </summary>
        public static float AirFriction
        {
            get => Controller != null ? Controller.airFriction : 0f;
            set
            {
                if (Controller != null)
                    Controller.airFriction = value;
            }
        }

        /// <summary>
        /// The height the player gets when doing an attack while jumping
        /// </summary>
        public static float AttackJumpHeight
        {
            get => Controller != null ? Controller.attackJump : 0f;
            set
            {
                if (Controller != null)
                    Controller.attackJump = value;
            }
        }

        public static float JumpHeight
        {
            get => Controller != null ? Controller.jumpHeight : 0f;
            set
            {
                if (Controller != null)
                    Controller.jumpHeight = value;
            }
        }

        /// <summary>
        /// The coyote time the player has
        /// </summary>
        public static float CoyoteTime
        {
            get => Controller != null ? Controller.coyoteTime : 0f;
            set
            {
                if (Controller != null)
                    Controller.coyoteTime = value;
            }
        }

        /// <summary>
        /// The max life the player has
        /// </summary>
        public static int MaxLife
        {
            get => Controller != null ? Controller.maxLife : 0;
            set
            {
                if (Controller != null)
                    Controller.maxLife = value;
            }
        }

        /// <summary>
        /// The max amount of double jumps the player has
        /// </summary>
        public static int MaxDoubleJumps
        {
            get => Controller != null ? Controller.maxDoubleJumps : 0;
            set
            {
                if (Controller != null)
                    Controller.maxDoubleJumps = value;
            }
        }

        /// <summary>
        /// When the player has double jumps left
        /// </summary>
        public static bool HasDoubleJump => Controller != null && Controller.HasDoubleJump;

        /// <summary>
        /// The time the player cant move after being hit
        /// </summary>
        public static float KnockbackTime
        {
            get => Controller != null ? Controller.knockbackTime : 0f;
            set
            {
                if (Controller != null)
                    Controller.knockbackTime = value;
            }
        }

        /// <summary>
        /// The height you gain when getting hit by an enemy
        /// </summary>
        public static float KnockbackHeight
        {
            get => Controller != null ? Controller.knockbackHeight : 0f;
            set
            {
                if (Controller != null)
                    Controller.knockbackHeight = (float) value;
            }
        }

        /// <summary>
        /// The extra movement you get when being hit by an enemy
        /// </summary>
        public static float KnockbackStrength
        {
            get => Controller != null ? Controller.knockbackStrength : 0f;
            set
            {
                if (Controller != null)
                    Controller.knockbackStrength = value;
            }
        }

        /// <summary>
        /// The invincibility time you have after being hit by an enemy. This does include the KnockbackTime
        /// </summary>
        public static float Invincibility
        {
            get => Controller != null ? Controller.invincibility : 0f;
            set
            {
                if (Controller != null)
                    Controller.invincibility = value;
            }
        }

        /// <summary>
        /// Duration of the attack hitbox and the overall attack
        /// </summary>
        public static float AttackHitboxTime
        {
            get => Controller != null ? Controller.hitboxTime : 0f;
            set
            {
                if (Controller != null)
                    Controller.hitboxTime = value;
            }
        }

        /// <summary>
        /// Offset of the attack hitbox relative to the player center
        /// </summary>
        public static Vector3 AttackHitboxOffset
        {
            get => Controller != null ? Controller.hitboxOffset : Vector3.zero;
            set
            {
                if (Controller != null)
                    Controller.hitboxOffset = value;
            }
        }

        /// <summary>
        /// Size of the Attack Hitbox on the X/Y and Z axis
        /// </summary>
        public static Vector2 AttackHitboxSize
        {
            get => Controller != null ? Controller.hitboxSize : Vector2.zero;
            set
            {
                if (Controller != null)
                    Controller.hitboxSize = value;
            }
        }

        /// <summary>
        /// The Speed how fast the player dissolves after a death
        /// Lower Number = Slower Dissolve
        /// </summary>
        public static float DissolveSpeedDeath
        {
            get => Controller != null ? Controller.dissolveSpeedDeath : 0f;
            set
            {
                if (Controller != null)
                    Controller.dissolveSpeedDeath = value;
            }
        }

        /// <summary>
        /// The speed at which the player appears again when respawning
        /// Lower Number = Slower Appearance
        /// </summary>
        public static float DissolveSpeedRespawn
        {
            get => Controller != null ? Controller.dissolveSpeedRespawn : 0f;
            set
            {
                if (Controller != null)
                    Controller.dissolveSpeedRespawn = value;
            }
        }

        /// <summary>
        /// The amount of the fake letters that are collected at the moment
        /// </summary>
        public static int CollectedFakeLetters
        {
            get => Controller != null ? Controller.fakeLetters : 0;
            set
            {
                if (Controller != null)
                    Controller.fakeLetters = value;
            }
        }

        /// <summary>
        /// Cooldown until the next FreezeFrameTime can be applied
        /// While the cooldown is active no other freeze frame will occur
        /// Cooldown always resets when hitting a new enemy
        /// </summary>
        public static float FreezeFrameCooldown
        {
            get => Controller != null ? Controller.freezeFrameCooldown : 0f;
            set
            {
                if (Controller != null)
                    Controller.freezeFrameCooldown = value;
            }
        }

        /// <summary>
        /// The actual time the time gets frozen after hitting an enemy
        /// </summary>
        public static float FreezeFrameTime
        {
            get => Controller != null ? Controller.freezeFrameTime : 0f;
            set
            {
                if (Controller != null)
                    Controller.freezeFrameTime = value;
            }
        }

        /// <summary>
        /// Gravity of the Game World
        /// </summary>
        public static Vector3 Gravity
        {
            get => Controller != null ? Controller.gravity : Vector3.zero;
            set
            {
                if (Controller != null)
                    Controller.gravity = value;
            }
        }

        /// <summary>
        /// Controlls after which distance the extra Shadow appears under the character
        /// </summary>
        public static float BlobShadowRayLength
        {
            get => Controller != null ? Controller.blobShadowRayLength : 0f;
            set
            {
                if (Controller != null)
                    Controller.blobShadowRayLength = value;
            }
        }

        /// <summary>
        /// The speed boost you gain when hitting the green zone on a Grind
        /// </summary>
        public static float GrindBoostStrength
        {
            get => Controller != null ? Controller.grindBoostStrength : 0f;
            set
            {
                if (Controller != null)
                    Controller.grindBoostStrength = value;
            }
        }

        /// <summary>
        /// If the player is on the ground
        /// </summary>
        public static bool Grounded
        {
            get => Controller != null && Controller.Grounded;
            set
            {
                if (Controller != null)
                    Controller.Grounded = value;
            }
        }

        /// <summary>
        /// Current Horizontal speed of the player
        /// </summary>
        public static Vector2 HorizontalSpeed
        {
            get => Controller != null ? Controller.HSpeed : Vector2.zero;
            set
            {
                if (Controller != null)
                    Controller.HSpeed = value;
            }
        }

        #endregion

        #region CHANGING FIELDS

        /// <summary>
        /// Returns true when an attack is possible
        /// Returns false when no attack is possible
        /// Can be changed at any time
        /// </summary>
        public static bool AttackPossible
        {
            //_controller._bubbleJump = _controller == null && value;
            get => Controller != null && Controller._attackPossible;
            set
            {
                if (Controller != null)
                    Controller._attackPossible = value;
            }
        }

        /// <summary>
        /// Sets the final scale of the Blob shadow that gets reached after the Ray Length gets exceeded
        /// </summary>
        public static Vector3 BlobShadowFinalScale
        {
            get => Controller != null ? Controller._blobShadowBaseScale : Vector3.zero;
            set
            {
                if (Controller != null)
                    Controller._blobShadowBaseScale = value;
            }
        }

        /// <summary>
        /// When boost is active Player uses its turbo speed
        /// Boost instantly resets after touching normal ground
        /// </summary>
        public static bool BoostActive
        {
            get => Controller != null && Controller._boost;
            set
            {
                if (Controller != null)
                    Controller._boost = value;
            }
        }

        /// <summary>
        /// When the player can bubble jump the First Jump will have extra height
        /// </summary>
        public static bool CanBubbleJump
        {
            get => Controller != null && Controller._bubbleJump;
            set
            {
                if (Controller != null)
                    Controller._bubbleJump = value;
            }
        }

        /// <summary>
        /// The Current coyote time of the player
        /// </summary>
        public static float CurrentCoyoteTime
        {
            get => Controller != null ? Controller._currentCoyoteTime : 0f;
            set
            {
                if (Controller != null)
                    Controller._currentCoyoteTime = value;
            }
        }

        /// <summary>
        /// The range of the Dissolve time is 0->1 when the player gets killed. 1-> -1 when the player respawns
        /// </summary>
        public static float CurrentDissolveTime
        {
            get => Controller != null ? Controller._currentDissolve : 0f;
            set
            {
                if (Controller != null)
                    Controller._currentDissolve = value;
            }
        }

        /// <summary>
        /// The Current time before a new freezeframe can occur
        /// </summary>
        public static float CurrentFreezeFrameCooldown
        {
            get => Controller != null ? Controller._currentFreezeFrameCooldown : 0f;
            set
            {
                if (Controller != null)
                    Controller._currentFreezeFrameCooldown = value;
            }
        }

        /// <summary>
        /// The current duration timer of the freeze frame
        /// </summary>
        public static float CurrentFreezeFrameTime
        {
            get => Controller != null ? Controller._currentFreezeFrameTime : 0f;
            set
            {
                if (Controller != null)
                    Controller._currentFreezeFrameTime = value;
            }
        }

        /// <summary>
        /// The timer where the attack hitbox is active
        /// </summary>
        public static float CurrentAttackHitboxTime
        {
            get => Controller != null ? Controller._currentHitboxTime : 0f;
            set
            {
                if (Controller != null)
                    Controller._currentHitboxTime = value;
            }
        }

        /// <summary>
        /// Current Invincibility Timer
        /// </summary>
        public static float CurrentInvincibility
        {
            get => Controller != null ? Controller._currentInvincibility : 0f;
            set
            {
                if (Controller != null)
                    Controller._currentInvincibility = value;
            }
        }

        /// <summary>
        /// The timer until the player can jump again
        /// </summary>
        public static float CurrentJumpBuffer
        {
            get => Controller != null ? Controller._currentJumpBuffer : 0f;
            set
            {
                if (Controller != null)
                    Controller._currentJumpBuffer = value;
            }
        }

        /// <summary>
        /// The timer until the player can jump again
        /// </summary>
        public static float CurrentKnockbackTime
        {
            get => Controller != null ? Controller._currentKnockbackTime : 0f;
            set
            {
                if (Controller != null)
                    Controller._currentKnockbackTime = value;
            }
        }

        /// <summary>
        /// The timer until the player can attack after being boosted by a Pusher
        /// </summary>
        public static float CurrentPusherAttackBuffer
        {
            get => Controller != null ? Controller._currentPusherAttackBuffer : 0f;
            set
            {
                if (Controller != null)
                    Controller._currentPusherAttackBuffer = value;
            }
        }

        /// <summary>
        /// The current speed of the player
        /// </summary>
        public static float CurrentSpeed
        {
            get => Controller != null ? Controller._currentSpeed : 0f;
            set
            {
                if (Controller != null)
                    Controller._currentSpeed = value;
            }
        }

        public static bool IsJumping() => Controller != null && Controller._doJump;

        /// <summary>
        /// Current amount of Double Jumps the player has
        /// </summary>
        public static int DoubleJumps
        {
            get => Controller != null ? Controller._doubleJumps : 0;
            set
            {
                if (Controller != null)
                    Controller._doubleJumps = value;
            }
        }

        /// <summary>
        /// The Gravity that gets set after the player lands again on the ground
        /// </summary>
        public static Vector3 GravityReset
        {
            get => Controller != null ? Controller._gravityReset : Vector3.zero;
            set
            {
                if (Controller != null)
                    Controller._gravityReset = value;
            }
        }

        /// <summary>
        /// The value for the automatic grind attack from toree
        /// Only works with toree
        /// </summary>
        public static bool TorreRailAttackActive
        {
            get => Controller != null && Controller._grindAttackActive;
            set
            {
                if (Controller != null)
                    Controller._grindAttackActive = value;
            }
        }

        /// <summary>
        /// The values corresponding to the current input.
        /// The values range from -1 to 1
        /// </summary>
        public static Vector2 InputVector
        {
            get => Controller != null ? Controller._inputVector : Vector2.zero;
            set
            {
                if (Controller != null)
                    Controller._inputVector = value;
            }
        }

        /// <summary>
        /// Current life of the player
        /// </summary>
        public static int CurrentLife
        {
            get => Controller != null ? Controller.Life : 0;
            set
            {
                if (Controller != null)
                    Controller.Life = value;
            }
        }

        public static Vector3 CurrentVelocity { get; private set; }

        #endregion

        #region EVENTS

        public delegate void HitboxHit(Vector3 sourcePosition, Transform source);
        public delegate void CheckHitbox();
        public delegate void PlayerLoaded(Timer.Character loadedCharacter);

        /// <summary>
        /// Gets called when the player gets damaged
        /// </summary>
        public static event HitboxHit OnPlayerHit;
        /// <summary>
        /// Gets called multiple times until the player is no longer attacking
        /// </summary>
        public static event CheckHitbox OnPlayerAttack;
        /// <summary>
        /// Gets called when the Player got loaded
        /// </summary>
        public static event PlayerLoaded OnPlayerLoaded;

        #endregion

        [HarmonyPatch(typeof(Bun.PlayerController), "Update")]
        [HarmonyPostfix]
        private static void A()
        {
            //controller._railBoost - The boost you get when jumping out of a rail, idk where it comes from
            //RailBoostStrength - Should probably be applied when jumping out of a rail

            //controller.RailAttack Boolean to set the player attacking while on a rail
            //Life and _life do the same thing?

            //leanDamp (When set to 1 hana always leans extremely when facing the opposite direciton of the spawning facing direction

            //Flicker:
            //376 - Dissolve Bottom Player
            //377 - Normal Dissolve
        }

        public static void Init()
        {
            Game.OnTransitionDone += CheckforLevel;
        }

        private static void CheckforLevel()
        {
            if (Game.GameState == GameState.Mission)
            {
                Controller = GameManager.Instance.playerRef;
                _kinematic = Controller.gameObject.GetComponent<KinematicCharacterMotor>();
                OnPlayerLoaded?.Invoke(LevelTimer.Character);
            }
            else
                Controller = null;
        }

        public static void ResetVelocityState() => Controller?.ResetVelocityState();
        public static void ResetHitboxTime() => Controller?.ResetHitboxTime();
        public static void ResetCoyoteTime() => Controller?.ResetCoyoteTime();
        public static void SetPlayerInputs(Bun.PlayerInputs inputs) => Controller?.SetInputs(ref inputs);
        public static void KillPlayer() => Controller?.KillPlayer();
        public static void LeaveRail() => Controller?.LeaveRail();
        public static void Teleport(Vector3 point) => Controller?.Teleport(point);


        [HarmonyPatch(typeof(Bun.PlayerController), "CalculateVelocity")]
        [HarmonyPostfix]
        private static void PlayerController_CalculateVelocity(Vector3 currentVelocity) =>
            CurrentVelocity = currentVelocity;

        [HarmonyPatch(typeof(Bun.PlayerController), "CheckHitbox")]
        [HarmonyPostfix]
        private static void PlayerController_CheckHitbox() => OnPlayerAttack?.Invoke();

        [HarmonyPatch(typeof(Bun.PlayerController), "OnHitboxHit")]
        [HarmonyPostfix]
        private static void PlayerController_OnHitboxHit(Vector3 sourcePos, Transform source) =>
            OnPlayerHit?.Invoke(sourcePos, source);
    }
}