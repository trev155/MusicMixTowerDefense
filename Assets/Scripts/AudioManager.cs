using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
    // ---------- Static path references ----------
    public static readonly string PLAYER_UNIT_CREATION_SOUND = "Audio/PlayerUnitCreation";
    public static readonly string UNIT_MIX_SOUND = "Audio/UnitMix";
    public static readonly string BUTTON_CLICK_SOUND = "Audio/ClickHeavy";
    public static readonly string MESSAGE_INFO = "Audio/MessageInfo";
    public static readonly string MESSAGE_POSITIVE = "Audio/MessagePositive";
    public static readonly string MESSAGE_NEGATIVE = "Audio/MessageNegative";
    public static readonly string MESSAGE_ACHIEVEMENT = "Audio/AchievementObtained";
    public static readonly string GAME_OVER = "Audio/GameOver";
    public static readonly string DRUM_ONE = "Audio/Drum1";
    public static readonly string DRUM_TWO = "Audio/Drum2";
    public static readonly string DRUM_THREE = "Audio/Drum3";
    public static readonly string PIANO_ONE = "Audio/Piano1";
    public static readonly string PIANO_TWO = "Audio/Piano2";
    public static readonly string SELL_UNIT = "Audio/SellUnit";
    
    // ---------- Fields ----------
    public Queue<string> queuedMusicPaths = new Queue<string>();
    private bool isPlayingBGM = false;

    // ---------- Audio Source References ----------
    public AudioSource gameEffectsAudioSource;  // Generic audio source
    public AudioSource enemyDeathAudioSource;
    public AudioSource projectileAudioSource;
    public AudioSource projectileLandingAudioSource;
    public AudioSource backgroundMusic;

    // ---------- General Functions ----------
    /* 
     * Generic function for playing a sound effect. Path argument should come from a static string in this class.
     */
    public void PlayAudio(string path) {
        PlayAudio(path, gameEffectsAudioSource);
    }

    public void PlayAudio(string path, AudioSource audioSource) {
        audioSource.clip = Resources.Load<AudioClip>(path);
        audioSource.PlayOneShot(audioSource.clip);
    }

    public void PlayAudioAfterTime(string path, float time) {
        PlayAudioAfterTime(path, gameEffectsAudioSource, time);
    }

    public void PlayAudioAfterTime(string path, AudioSource audioSource, float time) {
        StartCoroutine(PlayAudioAfterTimeCR(path, audioSource, time));
    }

    private IEnumerator PlayAudioAfterTimeCR(string path, AudioSource audioSource, float time) {
        yield return new WaitForSeconds(time);
        PlayAudio(path, audioSource);
    }

    // --------- Background Music ----------
    private void Update() {
        if (!isPlayingBGM && queuedMusicPaths.Count > 0) {
            string path = queuedMusicPaths.Dequeue();
            backgroundMusic.clip = Resources.Load<AudioClip>(path);
            backgroundMusic.PlayOneShot(backgroundMusic.clip);
            isPlayingBGM = true;
        }
        if (!backgroundMusic.isPlaying) {
            isPlayingBGM = false;
        }
    }

    // This should be called whenever we start a new level.
    public void AddLevelMusicToQueue(int level) {
        string path = level.ToString();
        if (level < 10) {
            path = "0" + path;
        }
        path = "Audio/BGM/L" + path;
        queuedMusicPaths.Enqueue(path);
    }

    // ---------- Generic Sound Effects ----------
    public void PlayRandomDrumEffect() {
        string path;
        int choice = GameEngine.GetInstance().random.Next(0, 3);
        if (choice == 0) {
            path = DRUM_ONE;
        } else if (choice == 1) {
            path = DRUM_TWO;
        } else {
            path = DRUM_THREE;
        }
        PlayAudioAfterTime(path, gameEffectsAudioSource, 0.1f);
    }

    public void PlayRandomPianoEffect() {
        string path;
        int choice = GameEngine.GetInstance().random.Next(0, 2);
        if (choice == 0) {
            path = PIANO_ONE;
        } else {
            path = PIANO_TWO;
        }
        PlayAudioAfterTime(path, gameEffectsAudioSource, 0.1f);
    }

    // ---------- Enemy Deaths ----------
    public void PlayRegularEnemyUnitDeathSound(int level) {
        string path;
        
        if (level < 6) {
            path = "Audio/EnemyDeath/drone_death";
        } else if (level < 11) {
            path = "Audio/EnemyDeath/ursadon_death";
        } else if (level < 16) {
            path = "Audio/EnemyDeath/probe_death";
        } else if (level < 21) {
            path = "Audio/EnemyDeath/tank_death";
        } else if (level < 26) {
            path = "Audio/EnemyDeath/scout_death";
        } else if (level < 31) {
            path = "Audio/EnemyDeath/rangasaur_death";
        } else if (level < 36) {
            path = "Audio/EnemyDeath/arbiter_death";
        } else if (level < 41) {
            path = "Audio/EnemyDeath/vulture_death";
        } else {
            throw new GameplayException("Unrecognized level value: " + level + ". Cannot play enemy death sound.");
        }

        if (!enemyDeathAudioSource.isPlaying) {
            PlayAudio(path, enemyDeathAudioSource);
        }
    }

    public void PlaySpecialUnitDeathSound(EnemyType enemyType) {
        string path;
        switch (enemyType) {
            case EnemyType.BOUNTY:
                path = "Audio/EnemyDeath/observer_death";
                break;
            case EnemyType.BONUS:
                path = "";
                break;
            case EnemyType.BOSS:
                path = "";
                break;
            default:
                throw new GameplayException("Unrecognized special unit: " + enemyType.ToString() + ". Unable to play special enemy death sound.");
        }

        PlayAudio(path, enemyDeathAudioSource);
    }

    // ---------- Projectiles ----------
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

        PlayAudio(path, projectileAudioSource);
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

    // Projectile Landings - not all units will have projectile landings, only select few ones
    public void PlayProjectileLandingSound(UnitClass unitClass, PlayerUnitRank rank) {
        string path;
        switch (unitClass) {
            case UnitClass.INFANTRY:
                path = GetInfantryProjectileLandingPath(rank);
                break;
            case UnitClass.MECH:
                path = GetMechProjectileLandingPath(rank);
                break;
            case UnitClass.LASER:
                path = GetLaserProjectileLandingPath(rank);
                break;
            case UnitClass.PSIONIC:
                path = GetPsionicProjectileLandingPath(rank);
                break;
            case UnitClass.ACID:
                path = GetAcidProjectileLandingPath(rank);
                break;
            case UnitClass.BLADE:
                path = GetBladeProjectileLandingPath(rank);
                break;
            case UnitClass.MAGIC:
                path = GetMagicProjectileLandingPath(rank);
                break;
            case UnitClass.FLAME:
                path = GetFlameProjectileLandingPath(rank);
                break;
            default:
                throw new GameplayException("Unrecognized unit class: " + unitClass.ToString() + ". Cannot play projectile landing attack sound.");
        }
        
        if (path.Length > 0) {
            PlayAudio(path, projectileLandingAudioSource);
        }
    }

    private string GetInfantryProjectileLandingPath(PlayerUnitRank rank) {
        return "";
    }

    private string GetMechProjectileLandingPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.D:
                return "Audio/ProjectileLanding/Mech/grenade_landing";
            case PlayerUnitRank.C:
                return "";
            case PlayerUnitRank.B:
                return "";
            case PlayerUnitRank.A:
                return "Audio/ProjectileLanding/Mech/grenade_landing";
            case PlayerUnitRank.S:
                return "";
            case PlayerUnitRank.X:
                return "";
            default:
                return "";
        }
    }

    private string GetLaserProjectileLandingPath(PlayerUnitRank rank) {
        return "";
    }

    private string GetPsionicProjectileLandingPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.D:
                return "";
            case PlayerUnitRank.C:
                return "Audio/ProjectileLanding/Psionic/psionic_explosion";
            case PlayerUnitRank.B:
                return "";
            case PlayerUnitRank.A:
                return "";
            case PlayerUnitRank.S:
                return "";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLanding/Psionic/scarab_hit";
            default:
                return "";
        }
    }

    private string GetAcidProjectileLandingPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.D:
                return "";
            case PlayerUnitRank.C:
                return "Audio/ProjectileLanding/Acid/guardian_hit";
            case PlayerUnitRank.B:
                return "Audio/ProjectileLanding/Acid/corrosive_acid";
            case PlayerUnitRank.A:
                return "";
            case PlayerUnitRank.S:
                return "";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLanding/Acid/corrosive_acid";
            default:
                return "";
        }
    }

    private string GetBladeProjectileLandingPath(PlayerUnitRank rank) {
        return "";
    }

    private string GetMagicProjectileLandingPath(PlayerUnitRank rank) {
        switch (rank) {
            case PlayerUnitRank.B:
                return "Audio/ProjectileLanding/Magic/shockwave_landing";
            case PlayerUnitRank.A:
                return "Audio/ProjectileLanding/Magic/shockwave_landing";
            case PlayerUnitRank.S:
                return "Audio/ProjectileLanding/Magic/shockwave_landing";
            case PlayerUnitRank.X:
                return "Audio/ProjectileLanding/Magic/shockwave_landing";
            default:
                return "";
        }
    }

    private string GetFlameProjectileLandingPath(PlayerUnitRank rank) {
        return "";
    }

    // ---------- Audio Levels ----------
    public void SetBGMAudioLevel(float value) {
        backgroundMusic.volume = value;
    }

    public void SetSoundEffectsVolumeLevel(float value) {
        gameEffectsAudioSource.volume = value;
        enemyDeathAudioSource.volume = value;
        projectileAudioSource.volume = value;
        projectileLandingAudioSource.volume = value;
    }
}
