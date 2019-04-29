using UnityEngine;

public class AudioManager : MonoBehaviour {
    // ---------- Static path references ----------
    public static readonly string PLAYER_UNIT_CREATION_SOUND = "Audio/PlayerUnitCreation";
    public static readonly string UNIT_MIX_SOUND = "Audio/UnitMix";
    public static readonly string BUTTON_CLICK_SOUND = "Audio/ClickHeavy";
    
    // ---------- Object References ----------
    public AudioSource soundEffects;
    
    // ---------- Sound Effects ----------
    /* 
     * Generic function for playing a sound effect. Path argument should come from a static string in this class.
     */
    public void PlaySound(string path) {
        soundEffects.clip = Resources.Load<AudioClip>(path);
        soundEffects.PlayOneShot(soundEffects.clip);
    }

    // ---------- Enemy Death Sounds ----------
    public void PlayRegularEnemyUnitDeathSound(int level) {
        string path = "Audio/EnemyDeath/drone_death";
        
        if (level < 6) {

        } else if (level < 11) {

        } else if (level < 16) {

        } else if (level < 21) {

        } else if (level < 26) {
        
        } else if (level < 31) {

        } else if (level < 36) {

        } else if (level < 41) {

        } else {
            throw new GameplayException("Unrecognized level value: " + level + ". Cannot play enemy death sound.");
        }

        soundEffects.clip = Resources.Load<AudioClip>(path);
        soundEffects.PlayOneShot(soundEffects.clip);
    }

    public void PlaySpecialUnitDeathSound(string type) {
        string path;
        switch (type) {
            case "Bounty":
                path = "Audio/EnemyDeath/observer_death";
                break;
            default:
                throw new GameplayException("Unrecognized special unit: " + type + ". Unable to play special enemy death sound.");
        }

        soundEffects.clip = Resources.Load<AudioClip>(path);
        soundEffects.PlayOneShot(soundEffects.clip);
    }

    // ---------- Projectile Attacks ----------
    public void PlayProjectileAttackSound(UnitClass unitClass, PlayerUnitRank rank) {
        string path;
        switch (unitClass) {
            case UnitClass.INFANTRY:
                path = GetInfantryProjectileAttackPath(rank);
                break;
            case UnitClass.MECH:
                path = GetMechProjectileAttackPath(rank);
                break;
            case UnitClass.LASER:
                path = GetLaserProjectileAttackPath(rank);
                break;
            case UnitClass.PSIONIC:
                path = GetPsionicProjectileAttackPath(rank);
                break;
            case UnitClass.ACID:
                path = GetAcidProjectileAttackPath(rank);
                break;
            case UnitClass.BLADE:
                path = GetBladeProjectileAttackPath(rank);
                break;
            case UnitClass.MAGIC:
                path = GetMagicProjectileAttackPath(rank);
                break;
            case UnitClass.FLAME:
                path = GetFlameProjectileAttackPath(rank);
                break;
            default:
                throw new GameplayException("Unrecognized unit class: " + unitClass.ToString() + ". Cannot play projectile attack sound.");
        }

        soundEffects.clip = Resources.Load<AudioClip>(path);
        soundEffects.PlayOneShot(soundEffects.clip);
    }

    private string GetInfantryProjectileAttackPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.D:
                return "Audio/ProjectileLaunch/Infantry/ghost_attack";
            case PlayerUnitRank.C:
                return "Audio/ProjectileLaunch/Infantry/ghost_attack";
            case PlayerUnitRank.B:
                return "Audio/ProjectileLaunch/Infantry/marine_attack";
            case PlayerUnitRank.A:
                return "Audio/ProjectileLaunch/Infantry/ghost_attack";
            case PlayerUnitRank.S:
                return "Audio/ProjectileLaunch/Infantry/ghost_attack";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLaunch/Infantry/marine_attack";
            default:
                throw new GameplayException("Unrecognized unit rank value: " + rank.ToString() + ". Cannot play infantry attack sound.");
        }
    }

    private string GetMechProjectileAttackPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.D:
                return "Audio/ProjectileLaunch/Mech/vulture_attack";
            case PlayerUnitRank.C:
                return "Audio/ProjectileLaunch/Mech/goliath_attack";
            case PlayerUnitRank.B:
                return "Audio/ProjectileLaunch/Mech/tank_attack";
            case PlayerUnitRank.A:
                return "Audio/ProjectileLaunch/Mech/vulture_attack";
            case PlayerUnitRank.S:
                return "Audio/ProjectileLaunch/Mech/tanksiege_attack";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLaunch/Mech/missile_launcher";
            default:
                throw new GameplayException("Unrecognized unit rank value: " + rank.ToString() + ". Cannot play mech attack sound.");
        }
    }

    private string GetLaserProjectileAttackPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.D:
                return "Audio/ProjectileLaunch/Laser/laser_attack";
            case PlayerUnitRank.C:
                return "Audio/ProjectileLaunch/Laser/laser_attack";
            case PlayerUnitRank.B:
                return "Audio/ProjectileLaunch/Laser/wraith_laser";
            case PlayerUnitRank.A:
                return "Audio/ProjectileLaunch/Laser/wraith_laser";
            case PlayerUnitRank.S:
                return "Audio/ProjectileLaunch/Laser/wraithair_attack";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLaunch/Laser/yamato_attack";
            default:
                throw new GameplayException("Unrecognized unit rank value: " + rank.ToString() + ". Cannot play laser attack sound.");
        }
    }

    private string GetPsionicProjectileAttackPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.D:
                return "Audio/ProjectileLaunch/Psionic/hightemplar_attack";
            case PlayerUnitRank.C:
                return "Audio/ProjectileLaunch/Psionic/dragoon_attack";
            case PlayerUnitRank.B:
                return "Audio/ProjectileLaunch/Psionic/shockwave_attack";
            case PlayerUnitRank.A:
                return "Audio/ProjectileLaunch/Psionic/hightemplar_attack";
            case PlayerUnitRank.S:
                return "Audio/ProjectileLaunch/Psionic/shockwave_attack";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLaunch/Psionic/dragoon_attack";
            default:
                throw new GameplayException("Unrecognized unit rank value: " + rank.ToString() + ". Cannot play psionic attack sound.");
        }
    }

    private string GetAcidProjectileAttackPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.D:
                return "Audio/ProjectileLaunch/Acid/mutalisk_attack";
            case PlayerUnitRank.C:
                return "Audio/ProjectileLaunch/Acid/guardian_attack";
            case PlayerUnitRank.B:
                return "Audio/ProjectileLaunch/Acid/devourer_attack";
            case PlayerUnitRank.A:
                return "Audio/ProjectileLaunch/Acid/hydralisk_attack";
            case PlayerUnitRank.S:
                return "Audio/ProjectileLaunch/Acid/hydralisk_attack";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLaunch/Acid/devourer_attack";
            default:
                throw new GameplayException("Unrecognized unit rank value: " + rank.ToString() + ". Cannot play acid attack sound.");
        }
    }

    private string GetBladeProjectileAttackPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.D:
                return "Audio/ProjectileLaunch/Blade/glaive_attack";
            case PlayerUnitRank.C:
                return "Audio/ProjectileLaunch/Blade/glaive_attack";
            case PlayerUnitRank.B:
                return "Audio/ProjectileLaunch/Blade/glaive_attack_2";
            case PlayerUnitRank.A:
                return "Audio/ProjectileLaunch/Blade/glaive_attack_2";
            case PlayerUnitRank.S:
                return "Audio/ProjectileLaunch/Blade/interceptor_attack";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLaunch/Blade/interceptor_attack";
            default:
                throw new GameplayException("Unrecognized unit rank value: " + rank.ToString() + ". Cannot play blade attack sound.");
        }
    }

    private string GetMagicProjectileAttackPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.B:
                return "Audio/ProjectileLaunch/Magic/magic_attack";
            case PlayerUnitRank.A:
                return "Audio/ProjectileLaunch/Magic/magic_attack";
            case PlayerUnitRank.S:
                return "Audio/ProjectileLaunch/Magic/magic_attack_2";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLaunch/Magic/magic_attack_2";
            default:
                throw new GameplayException("Unrecognized unit rank value: " + rank.ToString() + ". Cannot play magic attack sound.");
        }
    }

    private string GetFlameProjectileAttackPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.B:
                return "Audio/ProjectileLaunch/Flame/flame_attack";
            case PlayerUnitRank.A:
                return "Audio/ProjectileLaunch/Flame/flame_attack";
            case PlayerUnitRank.S:
                return "Audio/ProjectileLaunch/Flame/flame_attack_2";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLaunch/Flame/flame_attack_2";
            default:
                throw new GameplayException("Unrecognized unit rank value: " + rank.ToString() + ". Cannot play flame attack sound.");
        }
    }
}
