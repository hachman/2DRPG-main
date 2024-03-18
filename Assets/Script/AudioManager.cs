using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [System.Serializable]
    public class SoundData
    {
        public string soundName;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
        public bool loop = false;
    }

    public List<SoundData> backgroundMusicList;
    public List<SoundData> soundEffectList;
    private SoundData previousBackgroundMusic;
    [SerializeField] private AudioSource backgroundMusicAudioSource;
    [SerializeField] private AudioSource soundEffectAudioSource;
    private Dictionary<string, SoundData> soundDataDictionary = new Dictionary<string, SoundData>();

    private float backgroundMusicVolume = 1f;
    private float soundEffectsVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeSoundDataDictionary(backgroundMusicList);
            InitializeSoundDataDictionary(soundEffectList);

            LoadVolumeSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSoundDataDictionary(List<SoundData> list)
    {
        foreach (SoundData data in list)
        {
            soundDataDictionary[data.soundName] = data;
        }
    }

    private void LoadVolumeSettings()
    {
        backgroundMusicVolume = PlayerPrefs.GetFloat("BackgroundMusicVolume", 1f);
        soundEffectsVolume = PlayerPrefs.GetFloat("SoundEffectsVolume", 1f);
        ApplyVolumeSettings();
    }

    private void ApplyVolumeSettings()
    {
        backgroundMusicAudioSource.volume = backgroundMusicVolume;
    }

    public float GetBackgroundMusicVolume()
    {
        return backgroundMusicVolume;
    }

    public void SetBackgroundMusicVolume(float volume)
    {
        backgroundMusicVolume = volume;
        PlayerPrefs.SetFloat("BackgroundMusicVolume", volume);
        ApplyVolumeSettings();
    }

    public float GetSoundEffectsVolume()
    {
        return soundEffectsVolume;
    }

    public void SetSoundEffectsVolume(float volume)
    {
        soundEffectsVolume = volume;
        PlayerPrefs.SetFloat("SoundEffectsVolume", volume);
    }

    public void PlayPreviousBackgroundMusic()
    {
        if (previousBackgroundMusic != null)
        {
            PlayBackgroundMusic(previousBackgroundMusic.soundName);
        }
    }

    public void PlayBackgroundMusic(string name)
    {
        if (soundDataDictionary.TryGetValue(name, out SoundData soundData))
        {
            if (backgroundMusicAudioSource.isPlaying)
            {

                foreach (var item in soundDataDictionary)
                {
                    if (item.Value.clip == backgroundMusicAudioSource.clip) previousBackgroundMusic = item.Value;
                }
                backgroundMusicAudioSource.Stop();
            }
            backgroundMusicAudioSource.clip = soundData.clip;
            backgroundMusicAudioSource.loop = soundData.loop;
            backgroundMusicAudioSource.volume = soundData.volume;
            backgroundMusicAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music with name " + name + " not found.");
        }
    }

    public void PlaySoundEffect(string name)
    {
        if (soundDataDictionary.TryGetValue(name, out SoundData soundData))
        {
            soundEffectAudioSource.PlayOneShot(soundData.clip, soundData.volume);
        }
        else
        {
            Debug.LogWarning("Sound effect with name " + name + " not found.");
        }
    }

    public void MusicMuteToggle()
    {
        backgroundMusicAudioSource.mute = !backgroundMusicAudioSource.mute;
    }

    public void MuteSoundToggle()
    {
        soundEffectAudioSource.mute = !soundEffectAudioSource.mute;
    }

    public bool IsBGM_Muted() { return backgroundMusicAudioSource.mute; }

    public bool IsSFX_Muted() { return soundEffectAudioSource.mute; }
}
