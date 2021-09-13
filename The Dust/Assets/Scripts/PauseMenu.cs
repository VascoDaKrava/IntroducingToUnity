using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private AudioClip _buttonSelect;
    [SerializeField] private AudioClip _buttonPress;
    private AudioSource _audioSourceMenu;

    private Slider _volumeMasterSlider;
    private Slider _volumeMusicSlider;
    private Slider _volumeMenuSlider;
    private Slider _volumeFXSlider;
    private Toggle _volumeMasterToggle;

    private float _volumeMasterValue = 0f;

    private GameObject _optionsItems;
    private GameObject _menuItems;

    private Button _backToMainButton;
    private Button _optionsButton;
    private Button _resumeButton;
    private Button _exitButton;

    private bool _isPauseActive = false;

    // Start is called before the first frame update
    void Start()
    {
        _audioSourceMenu = GameObject.Find("Audio Source Menu").GetComponent<AudioSource>();

        _backToMainButton = GameObject.Find("BackToMainMenu").GetComponent<Button>();
        _optionsButton = GameObject.Find("Options").GetComponent<Button>();
        _resumeButton = GameObject.Find("Resume").GetComponent<Button>();
        _exitButton = GameObject.Find("ExitGame").GetComponent<Button>();

        _volumeMasterSlider = GameObject.Find("MainVolumeSlider").GetComponent<Slider>();
        _volumeMasterSlider.value = _volumeMasterValue;
        _volumeMasterSlider.onValueChanged.AddListener(delegate { MasterVolumeChange(); });

        _volumeMasterToggle = GameObject.Find("MainVolumeToggle").GetComponent<Toggle>();
        _volumeMasterToggle.onValueChanged.AddListener(delegate { MasterMuteChange(); });

        _volumeMusicSlider = GameObject.Find("MusicVolumeSlider").GetComponent<Slider>();
        _volumeMenuSlider = GameObject.Find("MenuButtonsVolumeSlider").GetComponent<Slider>();
        _volumeFXSlider = GameObject.Find("FXVolumeSlider").GetComponent<Slider>();

        _volumeMusicSlider.onValueChanged.AddListener(delegate { MusicVolumeChange(); });
        _volumeMenuSlider.onValueChanged.AddListener(delegate { MenuVolumeChange(); });
        _volumeFXSlider.onValueChanged.AddListener(delegate { FXVolumeChange(); });

        _optionsItems = GameObject.Find("OptionsItem");
        _optionsItems.SetActive(false);

        _menuItems = GameObject.Find("MenuItems");
        _menuItems.SetActive(false);

        // Load audio settings
        if (Settings.VolumeMute)
            _mixer.SetFloat(Storage.VolumeNameMaster, -80f);
        else
            _mixer.SetFloat(Storage.VolumeNameMaster, Settings.VolumeMaster);
        _volumeMasterValue = Settings.VolumeMaster;

        _volumeMasterToggle.isOn = Settings.VolumeMute;
        _volumeMasterSlider.value = Settings.VolumeMaster;

        _mixer.SetFloat(Storage.VolumeNameMusic, Settings.VolumeMusic);
        _volumeMusicSlider.value = Settings.VolumeMusic;

        _mixer.SetFloat(Storage.VolumeNameMenu, Settings.VolumeMenu);
        _volumeMenuSlider.value = Settings.VolumeMenu;

        _mixer.SetFloat(Storage.VolumeNameFX, Settings.VolumeFX);
        _volumeFXSlider.value = Settings.VolumeFX;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ShowHidePauseMenu();
        }
    }

    /// <summary>
    /// Reverse show/hide menu
    /// </summary>
    public void ShowHidePauseMenu()
    {
        if (_isPauseActive)
        {
            if (_optionsItems.activeSelf)
            {
                GoToOptionsMenu(false);
                return;
            }
            _isPauseActive = !_isPauseActive;
            _menuItems.SetActive(_isPauseActive);
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
            _isPauseActive = !_isPauseActive;
            _menuItems.SetActive(_isPauseActive);
        }
    }

    public void LoadMainMenu()
    {
        Settings.VolumeMaster = _volumeMasterValue;
        Settings.VolumeMenu = _volumeMenuSlider.value;
        Settings.VolumeMusic = _volumeMusicSlider.value;
        Settings.VolumeFX = _volumeFXSlider.value;
        Settings.VolumeMute = _volumeMasterToggle.isOn;
        _menuItems.SetActive(false);
        SceneManager.LoadScene(0);
    }

    #region OptionsMenu

    /// <summary>
    /// Open Options menu. Set "true" to open, "false" to close
    /// </summary>
    /// <param name="isToOptions"></param>
    public void GoToOptionsMenu(bool isToOptions)
    {
        _backToMainButton.interactable = !isToOptions;
        _optionsButton.interactable = !isToOptions;
        _resumeButton.gameObject.SetActive(!isToOptions);
        _exitButton.interactable = !isToOptions;
        _optionsItems.SetActive(isToOptions);
    }

    /// <summary>
    /// Change state to all Audio items
    /// </summary>
    /// <param name="state"></param>
    private void ChangeInteractableAudioItems(bool state)
    {
        _volumeMasterSlider.interactable = state;
        _volumeMusicSlider.interactable = state;
        _volumeMenuSlider.interactable = state;
        _volumeFXSlider.interactable = state;
    }

    public void MasterVolumeChange()
    {
        _mixer.SetFloat(Storage.VolumeNameMaster, _volumeMasterSlider.value);
        _volumeMasterValue = _volumeMasterSlider.value;
    }

    public void MasterMuteChange()
    {
        if (_volumeMasterToggle.isOn)
        {
            ChangeInteractableAudioItems(false);
            //_volumeMasterSlider.interactable = false;
            _mixer.SetFloat(Storage.VolumeNameMaster, -80f);
        }
        else
        {
            ChangeInteractableAudioItems(true);
            //_volumeMasterSlider.interactable = true;
            _mixer.SetFloat(Storage.VolumeNameMaster, _volumeMasterValue);
        }
    }

    public void MusicVolumeChange()
    {
        _mixer.SetFloat(Storage.VolumeNameMusic, _volumeMusicSlider.value);
    }

    public void MenuVolumeChange()
    {
        _mixer.SetFloat(Storage.VolumeNameMenu, _volumeMenuSlider.value);
    }

    public void FXVolumeChange()
    {
        _mixer.SetFloat(Storage.VolumeNameFX, _volumeFXSlider.value);
    }

    #endregion

    public void Exit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Enter mouse to the button
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name == "Text")
        {
            _audioSourceMenu.clip = _buttonSelect;
            _audioSourceMenu.Play();
        }
    }

    public void ClickButton()
    {
        _audioSourceMenu.clip = _buttonPress;
        _audioSourceMenu.Play();
    }
}
