using UnityEngine;
using System.Collections.Generic;
using Inkblade.Utils;

namespace Inkblade.Systems
{
    /// <summary>
    /// Manages all audio playback (SFX and music).
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private int poolSize = 10;

        [Header("Volume Settings")]
        [Range(0f, 1f)]
        [SerializeField] private float masterVolume = 1f;
        [Range(0f, 1f)]
        [SerializeField] private float musicVolume = 0.7f;
        [Range(0f, 1f)]
        [SerializeField] private float sfxVolume = 1f;

        [Header("Audio Clips")]
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private AudioClip retrieveSound;
        [SerializeField] private AudioClip dashSound;
        [SerializeField] private AudioClip enemyHitSound;
        [SerializeField] private AudioClip enemyDeathSound;
        [SerializeField] private AudioClip playerHitSound;
        [SerializeField] private AudioClip playerDeathSound;
        [SerializeField] private AudioClip mainTheme;

        private Queue<AudioSource> _audioSourcePool = new Queue<AudioSource>();
        private List<AudioSource> _activeAudioSources = new List<AudioSource>();

        // Singleton
        public static AudioManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                InitializeAudioSources();
                LoadVolumeSettings();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (mainTheme != null && musicSource != null)
            {
                PlayMusic(mainTheme);
            }
        }

        private void InitializeAudioSources()
        {
            // Create music source if not assigned
            if (musicSource == null)
            {
                GameObject musicObj = new GameObject("MusicSource");
                musicObj.transform.SetParent(transform);
                musicSource = musicObj.AddComponent<AudioSource>();
                musicSource.loop = true;
                musicSource.playOnAwake = false;
            }

            // Create SFX source if not assigned
            if (sfxSource == null)
            {
                GameObject sfxObj = new GameObject("SFXSource");
                sfxObj.transform.SetParent(transform);
                sfxSource = sfxObj.AddComponent<AudioSource>();
                sfxSource.playOnAwake = false;
            }

            // Create audio source pool
            for (int i = 0; i < poolSize; i++)
            {
                GameObject poolObj = new GameObject($"AudioSource_{i}");
                poolObj.transform.SetParent(transform);
                AudioSource source = poolObj.AddComponent<AudioSource>();
                source.playOnAwake = false;
                poolObj.SetActive(false);
                _audioSourcePool.Enqueue(source);
            }
        }

        public void PlaySFX(AudioClip clip, float volume = 1f)
        {
            if (clip == null) return;

            AudioSource source = GetPooledAudioSource();
            if (source != null)
            {
                source.clip = clip;
                source.volume = volume * sfxVolume * masterVolume;
                source.Play();
                StartCoroutine(ReturnToPoolAfterPlay(source, clip.length));
            }
        }

        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (musicSource == null || clip == null) return;

            musicSource.clip = clip;
            musicSource.loop = loop;
            musicSource.volume = musicVolume * masterVolume;
            musicSource.Play();
        }

        public void StopMusic()
        {
            if (musicSource != null)
            {
                musicSource.Stop();
            }
        }

        private AudioSource GetPooledAudioSource()
        {
            if (_audioSourcePool.Count > 0)
            {
                AudioSource source = _audioSourcePool.Dequeue();
                source.gameObject.SetActive(true);
                _activeAudioSources.Add(source);
                return source;
            }

            // Create new source if pool is exhausted
            GameObject newObj = new GameObject("AudioSource_Pooled");
            newObj.transform.SetParent(transform);
            AudioSource newSource = newObj.AddComponent<AudioSource>();
            newSource.playOnAwake = false;
            _activeAudioSources.Add(newSource);
            return newSource;
        }

        private System.Collections.IEnumerator ReturnToPoolAfterPlay(AudioSource source, float duration)
        {
            yield return new WaitForSeconds(duration);
            ReturnToPool(source);
        }

        private void ReturnToPool(AudioSource source)
        {
            if (source == null) return;

            source.Stop();
            source.clip = null;
            source.gameObject.SetActive(false);
            _activeAudioSources.Remove(source);
            _audioSourcePool.Enqueue(source);
        }

        // Volume controls
        public void SetMasterVolume(float volume)
        {
            masterVolume = Mathf.Clamp01(volume);
            UpdateVolumes();
            SaveVolumeSettings();
        }

        public void SetMusicVolume(float volume)
        {
            musicVolume = Mathf.Clamp01(volume);
            UpdateVolumes();
            SaveVolumeSettings();
        }

        public void SetSFXVolume(float volume)
        {
            sfxVolume = Mathf.Clamp01(volume);
            UpdateVolumes();
            SaveVolumeSettings();
        }

        private void UpdateVolumes()
        {
            if (musicSource != null)
            {
                musicSource.volume = musicVolume * masterVolume;
            }
        }

        // Convenience methods for common sounds
        public void PlayShootSound() => PlaySFX(shootSound);
        public void PlayRetrieveSound() => PlaySFX(retrieveSound);
        public void PlayDashSound() => PlaySFX(dashSound);
        public void PlayEnemyHitSound() => PlaySFX(enemyHitSound);
        public void PlayEnemyDeathSound() => PlaySFX(enemyDeathSound);
        public void PlayPlayerHitSound() => PlaySFX(playerHitSound);
        public void PlayPlayerDeathSound() => PlaySFX(playerDeathSound);

        private void LoadVolumeSettings()
        {
            masterVolume = PlayerPrefs.GetFloat(Constants.PREF_MASTER_VOLUME, 1f);
            musicVolume = PlayerPrefs.GetFloat(Constants.PREF_MUSIC_VOLUME, 0.7f);
            sfxVolume = PlayerPrefs.GetFloat(Constants.PREF_SFX_VOLUME, 1f);
            UpdateVolumes();
        }

        private void SaveVolumeSettings()
        {
            PlayerPrefs.SetFloat(Constants.PREF_MASTER_VOLUME, masterVolume);
            PlayerPrefs.SetFloat(Constants.PREF_MUSIC_VOLUME, musicVolume);
            PlayerPrefs.SetFloat(Constants.PREF_SFX_VOLUME, sfxVolume);
            PlayerPrefs.Save();
        }

        // Getters
        public float MasterVolume => masterVolume;
        public float MusicVolume => musicVolume;
        public float SFXVolume => sfxVolume;
    }
}

