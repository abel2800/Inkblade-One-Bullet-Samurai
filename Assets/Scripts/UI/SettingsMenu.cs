using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Inkblade.Systems;
using Inkblade.Utils;

namespace Inkblade.UI
{
    /// <summary>
    /// Handles settings menu UI and volume controls.
    /// </summary>
    public class SettingsMenu : MonoBehaviour
    {
        [Header("Volume Sliders")]
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;

        [Header("Volume Labels")]
        [SerializeField] private TextMeshProUGUI masterVolumeText;
        [SerializeField] private TextMeshProUGUI musicVolumeText;
        [SerializeField] private TextMeshProUGUI sfxVolumeText;

        [Header("Buttons")]
        [SerializeField] private Button applyButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button resetButton;

        private AudioManager _audioManager;
        private UIManager _uiManager;

        private float _masterVolume;
        private float _musicVolume;
        private float _sfxVolume;

        private void Awake()
        {
            _audioManager = AudioManager.Instance;
            _uiManager = FindObjectOfType<UIManager>();
        }

        private void Start()
        {
            LoadSettings();
            SetupSliders();
            SetupButtons();
        }

        private void LoadSettings()
        {
            if (_audioManager != null)
            {
                _masterVolume = _audioManager.MasterVolume;
                _musicVolume = _audioManager.MusicVolume;
                _sfxVolume = _audioManager.SFXVolume;
            }
            else
            {
                // Load from PlayerPrefs as fallback
                _masterVolume = PlayerPrefs.GetFloat(Constants.PREF_MASTER_VOLUME, 1f);
                _musicVolume = PlayerPrefs.GetFloat(Constants.PREF_MUSIC_VOLUME, 0.7f);
                _sfxVolume = PlayerPrefs.GetFloat(Constants.PREF_SFX_VOLUME, 1f);
            }
        }

        private void SetupSliders()
        {
            if (masterVolumeSlider != null)
            {
                masterVolumeSlider.value = _masterVolume;
                masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
            }

            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = _musicVolume;
                musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
            }

            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = _sfxVolume;
                sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
            }

            UpdateVolumeLabels();
        }

        private void SetupButtons()
        {
            if (applyButton != null)
            {
                applyButton.onClick.AddListener(ApplySettings);
            }

            if (backButton != null)
            {
                backButton.onClick.AddListener(GoBack);
            }

            if (resetButton != null)
            {
                resetButton.onClick.AddListener(ResetToDefaults);
            }
        }

        private void OnMasterVolumeChanged(float value)
        {
            _masterVolume = value;
            UpdateVolumeLabels();

            // Apply immediately for preview
            if (_audioManager != null)
            {
                _audioManager.SetMasterVolume(value);
            }
        }

        private void OnMusicVolumeChanged(float value)
        {
            _musicVolume = value;
            UpdateVolumeLabels();

            // Apply immediately for preview
            if (_audioManager != null)
            {
                _audioManager.SetMusicVolume(value);
            }
        }

        private void OnSFXVolumeChanged(float value)
        {
            _sfxVolume = value;
            UpdateVolumeLabels();

            // Apply immediately for preview
            if (_audioManager != null)
            {
                _audioManager.SetSFXVolume(value);
            }
        }

        private void UpdateVolumeLabels()
        {
            if (masterVolumeText != null)
            {
                masterVolumeText.text = $"Master: {Mathf.RoundToInt(_masterVolume * 100)}%";
            }

            if (musicVolumeText != null)
            {
                musicVolumeText.text = $"Music: {Mathf.RoundToInt(_musicVolume * 100)}%";
            }

            if (sfxVolumeText != null)
            {
                sfxVolumeText.text = $"SFX: {Mathf.RoundToInt(_sfxVolume * 100)}%";
            }
        }

        private void ApplySettings()
        {
            // Settings are already applied in real-time
            // This button can be used for confirmation or additional actions
            if (_audioManager != null)
            {
                _audioManager.SetMasterVolume(_masterVolume);
                _audioManager.SetMusicVolume(_musicVolume);
                _audioManager.SetSFXVolume(_sfxVolume);
            }

            // Save to PlayerPrefs
            PlayerPrefs.SetFloat(Constants.PREF_MASTER_VOLUME, _masterVolume);
            PlayerPrefs.SetFloat(Constants.PREF_MUSIC_VOLUME, _musicVolume);
            PlayerPrefs.SetFloat(Constants.PREF_SFX_VOLUME, _sfxVolume);
            PlayerPrefs.Save();
        }

        private void ResetToDefaults()
        {
            _masterVolume = 1f;
            _musicVolume = 0.7f;
            _sfxVolume = 1f;

            if (masterVolumeSlider != null)
            {
                masterVolumeSlider.value = _masterVolume;
            }

            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = _musicVolume;
            }

            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = _sfxVolume;
            }

            ApplySettings();
        }

        private void GoBack()
        {
            if (_uiManager != null)
            {
                _uiManager.HideSettings();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            // Clean up listeners
            if (masterVolumeSlider != null)
            {
                masterVolumeSlider.onValueChanged.RemoveAllListeners();
            }

            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.onValueChanged.RemoveAllListeners();
            }

            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.onValueChanged.RemoveAllListeners();
            }

            if (applyButton != null)
            {
                applyButton.onClick.RemoveAllListeners();
            }

            if (backButton != null)
            {
                backButton.onClick.RemoveAllListeners();
            }

            if (resetButton != null)
            {
                resetButton.onClick.RemoveAllListeners();
            }
        }
    }
}

