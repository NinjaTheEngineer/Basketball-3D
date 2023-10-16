using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaTools;
public class AudioManager : NinjaMonoBehaviour {
    private static AudioManager _instance;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip startGameSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip buttonClickSound;
    [SerializeField] private AudioClip throwSound;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private List<AudioClip> ballBounceSounds;
    [SerializeField] private List<AudioClip> netHitSounds;
    [SerializeField] private AudioClip backgroundMusic;
    public static AudioManager Instance {
        get {
            if(_instance == null) {
                _instance = FindObjectOfType<AudioManager>();
                if (_instance == null) {
                    _instance = new GameObject("AudioManager").AddComponent<AudioManager>();
                }
            }
            return _instance;
        }
    }
    private void Awake() {
        if (_instance) {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }
    private void Start() {
        PlayBackgroundMusic();
    }
    public void PlaySFX(AudioClip clip) {
        var logId = "PlaySFX";
        if(clip==null) {
            logw(logId, "Clip="+clip.logf()+" => no-op");
            return;
        }
        sfxSource.PlayOneShot(clip);
    }

    public void PlayBounceSound() {
        var random = Random.Range(0, ballBounceSounds.Count);
        PlaySFX(ballBounceSounds[random]);
    }
    public void PlayNetHitSound() {
        var random = Random.Range(0, netHitSounds.Count);
        PlaySFX(netHitSounds[random]);
    }
    public void PlayScoreSound() {
        PlaySFX(scoreSound);
    }
    public void PlayStartGameSound() {
        PlaySFX(startGameSound);
    }
    public void PlayButtonClick() {
        PlaySFX(buttonClickSound);
    }
    public void PlayThrowSound() {
        PlaySFX(throwSound);
    }
    public void PlayGameOverSound() {
        PlaySFX(gameOverSound);
    }
    public void PlayBackgroundMusic() {
        var logId = "PlayBackgroundMusic";
        if(backgroundMusic==null) {
            logw(logId, "BackgroundMusic="+backgroundMusic.logf()+" => no-op");
            return;
        }
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void StopBackgroundMusic() {
        musicSource.Stop();
    }
}