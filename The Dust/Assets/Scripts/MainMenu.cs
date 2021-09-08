using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Button _startButton;
    private Button _optionsButton;
    private Button _exitButton;
    private GameObject _optionsItems;

    // Start is called before the first frame update
    void Start()
    {
        _startButton = GameObject.Find("Start").GetComponent<Button>();
        _optionsButton = GameObject.Find("Options").GetComponent<Button>();
        _exitButton = GameObject.Find("Exit").GetComponent<Button>();
        _optionsItems = GameObject.Find("OptionsItem");
        _optionsItems.SetActive(false);
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        _startButton.interactable = false;
        _optionsButton.interactable = false;
        _exitButton.interactable = false;
        _optionsItems.SetActive(true);
        //RemoteSettings.
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Back()
    {
        _startButton.interactable = true;
        _optionsButton.interactable = true;
        _exitButton.interactable = true;
        _optionsItems.SetActive(false);
    }
}
