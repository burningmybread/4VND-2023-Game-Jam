using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer mixer;
    public AudioMixerGroup AMG_Music;
    public AudioMixerGroup AMG_SFX;

    //public const string MUSIC_KEY = "musicVolume"; //shows player prefs where to save the data; called in VolumeSettings.cs
    //public const string SFX_KEY = "sfxVolume";

    [System.Serializable] //nested class; stores tracks in a singular list
    public class Sounds
    {
        public string TrackName;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
    }

    public List<Sounds> MusicTracks;
    public List<Sounds> SFXTracks;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectSource;
    [SerializeField] private AudioSource _walkSource;
    [SerializeField] private bool musicFadingOut = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //LoadVolumeSettings();
    }

    private void LoadVolumeSettings()
    {
        //float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f); //loads the settings done by player, else default max volume
        //float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        //mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        //mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }

    public void PlayWalk(string clip) //specifically for the walking sfx
    {
        AudioClip _audioClip = GetClipFromList(clip, SFXTracks);

        if (_audioClip != null && _audioClip.name == "Mechwalk")
        {
            _effectSource.Play();

            if (_effectSource.isPlaying)
            {
                _effectSource.PlayDelayed(_audioClip.length);
            }
        }
    }

    public void PlayEffect(string clip) //for menu buttons and such
    {
        AudioClip _audioClip = GetClipFromList(clip, SFXTracks);

        if (_audioClip != null)
        {
            _effectSource.PlayOneShot(_audioClip);
        }
    }

    public void PlayMusic(string clip) //for the songs
    {
        AudioClip _audioClip = GetClipFromList(clip, MusicTracks);

        if (_audioClip != null)
        {
            _musicSource.clip = _audioClip;
            _musicSource.Play();
        }
    }


    private AudioClip GetClipFromList(string clip, List<Sounds> list)
    {
        AudioClip _audioClip = list.FirstOrDefault(obj => obj.TrackName == clip)?.clip; //find the audio clip in the list that matches the name typed in
        if (_audioClip != null)
        {
            return _audioClip;
        }
        else
        {
            Debug.LogError("Audio clip " + "'" + clip + "'" + " not found in audio manager");
            return null;
        }
    }

    public void ResetMusicVolume()
    {
        _musicSource.volume = 1f;
    }

    public void FadeOutMusic(float fadeTime = 1)
    {
        if (!musicFadingOut)
        {
            StartCoroutine(MusicFadeOut(fadeTime));
        }
        else
        {
            Debug.LogWarning("Audio Manager: Music already fading out!");
        }
    }

    IEnumerator MusicFadeOut(float fadeTime)
    {
        float elapsed = 0f;

        musicFadingOut = true;

        while (musicFadingOut)
        {
            if (_musicSource.volume < 0.01f)
            {
                _musicSource.volume = 0f;
                musicFadingOut = false;
                break;
            }

            elapsed += Time.unscaledDeltaTime; //independent from the frame count

            float t = elapsed / fadeTime;

            yield return null;

            _musicSource.volume = Mathf.Lerp(1, 0, t);
        }
    }
}