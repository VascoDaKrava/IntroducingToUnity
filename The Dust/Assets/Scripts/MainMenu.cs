using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour, IPointerEnterHandler
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
    private GameObject _loadingItems;
    private GameObject _menuItems;

    private Button _startButton;
    private Button _optionsButton;
    private Button _exitButton;

    private Text _loadingText;
    private Image _loadingProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        _audioSourceMenu = GameObject.Find("Audio Source Menu").GetComponent<AudioSource>();

        _startButton = GameObject.Find("Start").GetComponent<Button>();
        _optionsButton = GameObject.Find("Options").GetComponent<Button>();
        _exitButton = GameObject.Find("Exit").GetComponent<Button>();

        _volumeMasterSlider = GameObject.Find("MainVolumeSlider").GetComponent<Slider>();
        _volumeMasterSlider.value = _volumeMasterValue;
        _volumeMasterSlider.onValueChanged.AddListener(delegate { MasterVolumeChange(); });

        _volumeMasterToggle = GameObject.Find("MainVolumeToggle").GetComponent<Toggle>();
        _volumeMasterToggle.isOn = false;
        _volumeMasterToggle.onValueChanged.AddListener(delegate { MasterMuteChange(); });

        _volumeMusicSlider = GameObject.Find("MusicVolumeSlider").GetComponent<Slider>();
        _volumeMenuSlider = GameObject.Find("MenuButtonsVolumeSlider").GetComponent<Slider>();
        _volumeFXSlider = GameObject.Find("FXVolumeSlider").GetComponent<Slider>();

        _volumeMusicSlider.onValueChanged.AddListener(delegate { MusicVolumeChange(); });
        _volumeMenuSlider.onValueChanged.AddListener(delegate { MenuVolumeChange(); });
        _volumeFXSlider.onValueChanged.AddListener(delegate { FXVolumeChange(); });

        _optionsItems = GameObject.Find("OptionsItem");
        _optionsItems.SetActive(false);

        _loadingText = GameObject.Find("LoadingText").GetComponent<Text>();
        _loadingProgressBar = GameObject.Find("LoadingProgress").GetComponent<Image>();

        _loadingItems = GameObject.Find("Loading");
        _loadingItems.SetActive(false);

        _menuItems = GameObject.Find("MenuItems");
    }

    public void StartNewGame()
    {
        Settings.VolumeMaster = _volumeMasterValue;
        Settings.VolumeMenu = _volumeMenuSlider.value;
        Settings.VolumeMusic = _volumeMusicSlider.value;
        Settings.VolumeFX = _volumeFXSlider.value;
        Settings.VolumeMute = _volumeMasterToggle.isOn;
        _menuItems.SetActive(false);
        _loadingItems.SetActive(true);

        StartCoroutine(LoadWithProgress(1));
    }

    IEnumerator LoadWithProgress(int loadingScene)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(loadingScene);
        asyncOperation.allowSceneActivation = false;
        _loadingProgressBar.fillAmount = asyncOperation.progress;

        while (!asyncOperation.isDone)
        {
            _loadingProgressBar.fillAmount = asyncOperation.progress;

            if (asyncOperation.progress >= 0.9f)
            {
                _loadingProgressBar.fillAmount = 1f;
                _loadingText.text = "Press any key to continue";
                if (Input.anyKeyDown)
                    asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }


    #region OptionsMenu

    /// <summary>
    /// Open Options menu. Set "true" to open, "false" to close
    /// </summary>
    /// <param name="isToOptions"></param>
    public void GoToOptionsMenu(bool isToOptions)
    {
        _startButton.interactable = !isToOptions;
        _optionsButton.interactable = !isToOptions;
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
