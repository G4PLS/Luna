using UnityEngine;
using HarmonyLib;
using System.Threading;
using Rewired.Utils.Classes.Utility;

namespace Project_Luna
{
    //!IMPORTANT! Under Player->attackTrailMesh is the part that collects the cranes
    public class Player
    {
        private static Bun.PlayerController controller;

        #region FIX FIELDS
        /// <summary>
        /// The Speed when Sprinting
        /// </summary>
        public static float? SprintSpeed
        {
            get
            {
                if (controller == null) return controller.sprintSpeed;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.sprintSpeed = 0f;
                    else controller.sprintSpeed = (float)value;
                }
            }
        }

        /// <summary>
        /// The Speed when Walking
        /// </summary>
        public static float? RunSpeed
        {
            get
            {
                if (controller == null) return controller.runSpeed;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.runSpeed = 0f;
                    else controller.runSpeed = (float)value;
                }
            }
        }

        /// <summary>
        /// The Speed when on a Booser Block
        /// </summary>
        public static float? TurboSpeed
        {
            get
            {
                if (controller == null) return controller.turboSpeed;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.turboSpeed = 0f;
                    else controller.turboSpeed = (float)value;
                }
            }
        }

        /// <summary>
        /// The Acceleration to reach max Speed
        /// </summary>
        public static float? Acceleration
        {
            get
            {
                if (controller == null) return controller.acceleration;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.acceleration = 0f;
                    else controller.acceleration = (float)value;
                }
            }
        }

        /// <summary>
        /// How fast hana will reach the walking direction
        /// Lower value = Character reaches direction faster
        /// </summary>
        public static float? RotationDampening
        {
            get
            {
                if (controller != null) return controller.rotationDamp;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.rotationDamp = 0f;
                    else controller.rotationDamp = (float)value;
                }
            }
        }

        /// <summary>
        /// IDK
        /// </summary>
        public static float? GroundRotationDamp
        {
            get
            {
                if (controller != null) return controller.groundRotationDamp;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.groundRotationDamp = 0f;
                    else controller.groundRotationDamp = (float)value;
                }
            }
        }

        /// <summary>
        /// How fast you loose speed when jumping out of a rail
        /// Lower value = More speed is being kept when jumpin out of it
        /// </summary>
        public static float? RailBoostDampening
        {
            get
            {
                if (controller != null) return controller.railBoostDamp;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.railBoostDamp = 0f;
                    else controller.railBoostDamp = (float)value;
                }
            }
        }

        /// <summary>
        /// ---------
        /// IS ALWAYS OVERRIDDEN
        /// </summary>
        public static float? PushSpeed
        {
            get
            {
                return Bun.PlayerController.PushSpeed;
            }
            set
            {
                if (value == null) Bun.PlayerController.PushSpeed = 0f;
                else Bun.PlayerController.PushSpeed = (float)value;
            }
        }

        /// <summary>
        /// Friction after Being pushed
        /// Lower Value = Slower speed decrease
        /// No check for negative values = Weird player behaviour
        /// </summary>
        public static float? PushFriction
        {
            get
            {
                if (controller != null) return controller.pushFriction;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.pushFriction = 0f;
                    else controller.pushFriction = (float)value;
                }
            }
        }

        /// <summary>
        /// IDK
        /// Is Probably not implemented because there is no _current field for it
        /// </summary>
        public static float? PushBuffer
        {
            get
            {
                if (controller != null) return controller.pushBuffer;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.pushBuffer = 0f;
                    else controller.pushBuffer = (float)value;
                }
            }
        }

        /// <summary>
        /// The Time it takes to Jump again
        /// </summary>
        public static float? JumpBuffer
        {
            get
            {
                if (controller != null) return controller.jumpBuffer;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.jumpBuffer = 0f;
                    else controller.jumpBuffer = (float)value;
                }
            }
        }

        /// <summary>
        /// The friction the player has when on the ground
        /// </summary>
        public static float? Friction
        {
            get
            {
                if(controller != null) return controller.friction;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller.friction = 0f;
                    else controller.friction = (float)value;
                }
            }
        }

        /// <summary>
        /// The friciton the player has when in the air
        /// </summary>
        public static float? AirFriction
        {
            get
            {
                if (controller != null) return controller.airFriction;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.airFriction = 0f;
                    else controller.airFriction = (float)value;
                }
            }
        }

        /// <summary>
        /// The height the player gets when doing an attack while jumping
        /// </summary>
        public static float? AttackJumpHeight
        {
            get
            {
                if (controller != null) return controller.attackJump;
                else return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.attackJump = 0f;
                    else controller.attackJump = (float)value;
                }
            }
        }

        /// <summary>
        /// The coyote time the player has
        /// </summary>
        public static float? CoyoteTime
        {
            get
            {
                if (controller != null) return controller.coyoteTime;
                else return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller.coyoteTime = 0f;
                    else controller.coyoteTime = (float)value;
                }
            }
        }

        /// <summary>
        /// The max life the player has
        /// </summary>
        public static int? MaxLife
        {
            get
            {
                if (controller != null) return controller.maxLife;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.maxLife = 0;
                    else controller.maxLife = (int)value;
                }
            }
        }

        /// <summary>
        /// The max amount of double jumps the player has
        /// </summary>
        public static int? MaxDoubleJumps
        {
            get
            {
                if (controller != null) return controller.maxDoubleJumps;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.maxDoubleJumps = 0;
                    else controller.maxDoubleJumps = (int)value;
                }
            }
        }

        /// <summary>
        /// When the player has double jumps left
        /// </summary>
        public static bool? HasDoubleJump
        {
            get
            {
                if (controller != null) return controller.HasDoubleJump;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    //if (value == null) controller.HasDoubleJump = false;
                    //else controller.HasDoubleJump = (bool)value;
                }
            }
        }

        /// <summary>
        /// The time the player cant move after being hit
        /// </summary>
        public static float? KnockbackTime
        {
            get
            {
                if (controller != null) return controller.knockbackTime;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller.knockbackTime = 0f;
                    else controller.knockbackTime = (float)value;
                }
            }
        }

        /// <summary>
        /// The height you gain when getting hit by an enemy
        /// </summary>
        public static float? KnockbackHeight
        {
            get
            {
                if(controller != null) return controller.knockbackHeight;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller.knockbackHeight = 0f;
                    else controller.knockbackHeight = (float)value;
                }
            }
        }

        /// <summary>
        /// The extra movement you get when being hit by an enemy
        /// </summary>
        public static float? KnockbackStrength
        {
            get
            {
                if (controller != null) return controller.knockbackStrength;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller.knockbackStrength = 0f;
                    else controller.knockbackStrength = (float)value;
                }
            }
        }

        /// <summary>
        /// The invincibility time you have after being hit by an enemy. This does include the KnockbackTime
        /// </summary>
        public static float? Invincibility
        {
            get
            {
                if (controller != null) return controller.invincibility;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller.invincibility = 0f;
                    else controller.invincibility = (float)value;
                }
            }
        }

        /// <summary>
        /// Duration of the attack hitbox and the overall attack
        /// </summary>
        public static float? AttackHitboxTime
        {
            get
            {
                if (controller != null) return controller.hitboxTime;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller.hitboxTime = 0f;
                    else controller.hitboxTime = (float)value;
                }
            }
        }

        /// <summary>
        /// Offset of the attack hitbox relative to the player center
        /// </summary>
        public static Vector3? AttackHitboxOffset
        {
            get
            {
                if (controller != null) return controller.hitboxOffset;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.hitboxOffset = Vector3.zero;
                    else controller.hitboxOffset = (Vector3)value;
                }
            }
        }

        /// <summary>
        /// Size of the Attack Hitbox on the X/Y and Z axis
        /// </summary>
        public static Vector2? AttackHitboxSize
        {
            get
            {
                if (controller != null) return controller.hitboxSize;
                return null;
            }
            set
            {
                if(controller!= null)
                {
                    if(value == null) controller.hitboxSize = Vector2.zero;
                    else controller.hitboxSize = (Vector2)value;
                }
            }
        }

        /// <summary>
        /// The Speed how fast the player dissolves after a death
        /// Lower Number = Slower Dissolve
        /// </summary>
        public static float? DissolveSpeedDeath
        {
            get
            {
                if (controller != null) return controller.dissolveSpeedDeath;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller.dissolveSpeedDeath = 0f;
                    else controller.dissolveSpeedDeath = (float)value;
                }
            }
        }

        /// <summary>
        /// The speed at which the player appears again when respawning
        /// Lower Number = Slower Appearance
        /// </summary>
        public static float? DissolveSpeedRespawn
        {
            get
            {
                if (controller != null) return controller.dissolveSpeedRespawn;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller.dissolveSpeedRespawn = 0f;
                    else controller.dissolveSpeedRespawn = (float)value;
                }
            }
        }

        /// <summary>
        /// The amount of the fake letters that are collected at the moment
        /// </summary>
        public static int? CollectedFakeLetters
        {
            get
            {
                if (controller != null) return controller.fakeLetters;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.fakeLetters = 0;
                    else controller.fakeLetters = (int)value;
                }
            }
        }

        /// <summary>
        /// Cooldown until the next FreezeFrameTime can be applied
        /// While the cooldown is active no other freeze frame will occur
        /// Cooldown always resets when hitting a new enemy
        /// </summary>
        public static float? FreezeFrameCooldown
        {
            get
            {
                if (controller != null) return controller.freezeFrameCooldown;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller.freezeFrameCooldown = 0f;
                    else controller.freezeFrameCooldown = (float)value;
                }
            }
        }

        /// <summary>
        /// The actual time the time gets frozen after hitting an enemy
        /// </summary>
        public static float? FreezeFrameTime
        {
            get
            {
                if (controller != null) return controller.freezeFrameTime;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.freezeFrameTime = 0f;
                    else controller.freezeFrameTime = (float)value;
                }
            }
        }

        /// <summary>
        /// Gravity of the Game World
        /// </summary>
        public static Vector3? Gravity
        {
            get
            {
                if (controller != null) return controller.gravity;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.gravity = Vector3.zero;
                    else controller.gravity = (Vector3)value;
                }
            }
        }

        /// <summary>
        /// Controlls after which distance the extra Shadow appears under the character
        /// </summary>
        public static float? BlobShadowRayLength
        {
            get
            {
                if (controller != null) return controller.blobShadowRayLength;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.blobShadowRayLength = 0f;
                    else controller.blobShadowRayLength = (float)value;
                }
            }
        }

        /// <summary>
        /// The speed boost you gain when hitting the green zone on a Grind
        /// </summary>
        public static float? GrindBoostStrength
        {
            get
            {
                if (controller != null) return controller.grindBoostStrength;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.grindBoostStrength = 0f;
                    else controller.grindBoostStrength = (float)value;
                }
            }
        }

        /// <summary>
        /// If the player is on the ground
        /// </summary>
        public static bool? Grounded
        {
            get
            {
                if (controller != null) return controller.Grounded;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.Grounded = false;
                    else controller.Grounded = (bool)value;
                }
            }
        }

        /// <summary>
        /// Current Horizontal speed of the player
        /// </summary>
        public static Vector2? HorizontalSpeed
        {
            get
            {
                if (controller != null) return controller.HSpeed;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller.HSpeed = Vector2.zero;
                    else controller.HSpeed = (Vector2)value;
                }
            }
        }

        #endregion

        #region CHANGING FIELDS

        /// <summary>
        /// Returns true when an attack is possible
        /// Returns false when no attack is possible
        /// Can be changed at any time
        /// </summary>
        public static bool? AttackPossible
        {
            get
            {
                if (controller != null) return controller._attackPossible;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._attackPossible = false;
                    else controller._attackPossible = (bool)value;
                }
            }
        }

        /// <summary>
        /// Sets the final scale of the Blob shadow that gets reached after the Ray Length gets exceeded
        /// </summary>
        public static Vector3? BlobShadowFinalScale
        {
            get
            {
                if (controller != null) return controller._blobShadowBaseScale;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._blobShadowBaseScale = Vector3.zero;
                    else controller._blobShadowBaseScale = (Vector3)value;
                }
            }
        }

        /// <summary>
        /// When boost is active Player uses its turbo speed
        /// Boost instantly resets after touching normal ground
        /// </summary>
        public static bool? BoostActive
        {
            get
            {
                if (controller != null) return controller._boost;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller._boost = false;
                    else controller._boost = (bool)value;
                }
            }
        }

        /// <summary>
        /// When the player can bubble jump the First Jump will have extra height
        /// </summary>
        public static bool? CanBubbleJump
        {
            get
            {
                if (controller != null) return controller._bubbleJump;
                return null;
            }
            set
            {
                if (controller != null) controller._bubbleJump = false;
                else controller._bubbleJump = (bool)value;
            }
        }

        /// <summary>
        /// The Current coyote time of the player
        /// </summary>
        public static float? CurrentCoyoteTime
        {
            get
            {
                if (controller != null) return controller._currentCoyoteTime;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._currentCoyoteTime = 0f;
                    else controller._currentCoyoteTime = (float)value;
                }
            }
        }

        /// <summary>
        /// The range of the Dissolve time is 0->1 when the player gets killed. 1-> -1 when the player respawns
        /// </summary>
        public static float? CurrentDissolveTime
        {
            get
            {
                if (controller != null) return controller._currentDissolve;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._currentDissolve = 0f;
                    else controller._currentDissolve = (float) value;
                }
            }
        }

        /// <summary>
        /// The Current time before a new freezeframe can occur
        /// </summary>
        public static float? CurrentFreezeFrameCooldown
        {
            get
            {
                if (controller != null) return controller._currentFreezeFrameCooldown;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller._currentFreezeFrameCooldown = 0f;
                    else controller._currentFreezeFrameCooldown = (float)value;
                }
            }
        }

        /// <summary>
        /// The current duration timer of the freeze frame
        /// </summary>
        public static float? CurrentFreezeFrameTime
        {
            get
            {
                if (controller != null) return controller._currentFreezeFrameTime;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._currentFreezeFrameTime = 0f;
                    else controller._currentFreezeFrameTime = (float)value;
                }
            }
        }

        /// <summary>
        /// The timer where the attack hitbox is active
        /// </summary>
        public static float? CurrentAttackHitboxTime
        {
            get
            {
                if (controller != null) return controller._currentHitboxTime;
                return null;
            }
            set
            {
                if (controller != null) controller._currentHitboxTime = 0f;
                else controller._currentHitboxTime = (float)value;
            }
        }

        /// <summary>
        /// Current Invincibility Timer
        /// </summary>
        public static float? CurrentInvincibility
        {
            get
            {
                if (controller != null) return controller._currentInvincibility;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._currentInvincibility = 0f;
                    else controller._currentInvincibility = (float)value;
                }
            }
        }

        /// <summary>
        /// The timer until the player can jump again
        /// </summary>
        public static float? CurrentJumpBuffer
        {
            get
            {
                if (controller != null) return controller._currentJumpBuffer;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._currentJumpBuffer = 0f;
                    else controller._currentJumpBuffer = (float)value;
                }
            }
        }

        /// <summary>
        /// The timer until the player can jump again
        /// </summary>
        public static float? CurrentKnockbackTime
        {
            get
            {
                if (controller != null) return controller._currentKnockbackTime;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._currentKnockbackTime = 0f;
                    else controller._currentKnockbackTime = (float)value;
                }
            }
        }

        /// <summary>
        /// The timer until the player can attack after being boosted by a Pusher
        /// </summary>
        public static float? CurrentPusherAttackBuffer
        {
            get
            {
                if (controller != null) return controller._currentPusherAttackBuffer;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._currentPusherAttackBuffer = 0f;
                    else controller._currentPusherAttackBuffer = (float)value;
                }
            }
        }

        /// <summary>
        /// The current speed of the player
        /// </summary>
        public static float? CurrentSpeed
        {
            get
            {
                if(controller != null) return controller._currentSpeed;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller._currentSpeed = 0f;
                    else controller._currentSpeed = (float)value;
                }
            }
        }

        public static bool? IsJumping() => controller != null ? controller._doJump : null;

        /// <summary>
        /// Current amount of Double Jumps the player has
        /// </summary>
        public static int? DoubleJumps
        {
            get
            {
                if (controller != null) return controller._doubleJumps;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._doubleJumps = 0;
                    else controller._doubleJumps = (int)value;
                }
            }
        }

        /// <summary>
        /// The Gravity that gets set after the player lands again on the ground
        /// </summary>
        public static Vector3? GravityReset
        {
            get
            {
                if(controller != null) return controller._gravityReset;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._gravityReset = Vector3.zero;
                    else controller._gravityReset = (Vector3)value;
                }
            }
        }

        /// <summary>
        /// The value for the automatic grind attack from toree
        /// Only works with toree
        /// </summary>
        public static bool? TorreRailAttackActive
        {
            get
            {
                if (controller != null) return controller._grindAttackActive;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if (value == null) controller._grindAttackActive = false;
                    else controller._grindAttackActive = (bool)value;
                }
            }
        }

        /// <summary>
        /// The values corresponding to the current input.
        /// The values range from -1 to 1
        /// </summary>
        public static Vector2? InputVector
        {
            get
            {
                if(controller != null) return controller._inputVector;
                return null;
            }
            set
            {
                if(controller != null)
                {
                    if(value == null) controller._inputVector = Vector2.zero;
                    else controller._inputVector = (Vector2)value;
                }
            }
        }

        /// <summary>
        /// Current life of the player
        /// </summary>
        public static int? CurrentLife
        {
            get
            {
                if (controller != null) return controller.Life;
                return null;
            }
            set
            {
                if (controller != null)
                {
                    if (value == null) controller.Life = 0;
                    else controller.Life = (int)value;
                }
            }
        }

        #endregion

        #region EVENTS
        public delegate void HitboxHit(Vector3 sourcePosition, Transform source);
        public static event HitboxHit OnPlayerHit;
        #endregion

        /*
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
        */


        public static void Init()
        {
            Plugin.OnTransitionDone += CheckforLevel;
        }
        private static void CheckforLevel()
        {
            if (GameManager.Instance.gameState == GameState.Mission)
                controller = GameManager.Instance.playerRef;
            else
                controller = null;
        }



        public static void ResetVelocityState() => controller?.ResetVelocityState();
        public static void ResetHitboxTime() => controller?.ResetHitboxTime();
        public static void ResetCoyoteTime() => controller?.ResetCoyoteTime();
        public static void SetPlayerInputs(Bun.PlayerInputs inputs) => controller?.SetInputs(ref inputs);


        /// <summary>
        /// Gets triggered after the player gets hit by something
        /// </summary>
        /// <param name="sourcePos">Position of the Damaging object</param>
        /// <param name="source">The transform of the Damaging object</param>
        [HarmonyPatch(typeof(Bun.PlayerController), "OnHitboxHit")]
        [HarmonyPostfix]
        private static void PlayerController_OnHitboxHit(Vector3 sourcePos, Transform source) => OnPlayerHit?.Invoke(sourcePos, source);
    }
}
