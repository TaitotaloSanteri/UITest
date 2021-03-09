using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour, IButtonPressedHandler
{
    [SerializeField] private Transform mainMenu, settingsMenu;
    [SerializeField] private TMP_Dropdown dropdown;
    private MenuState menuState;

    private void Awake()
    {
        ChangeState(MenuState.MAINMENU);
        dropdown.ClearOptions();
        List<string> resolutions = new List<string>();
        int index = 0;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutions.Add($"{Screen.resolutions[i].width}x{Screen.resolutions[i].height}");
            if (Screen.resolutions[i].width == Screen.currentResolution.width &&
                Screen.resolutions[i].height == Screen.currentResolution.height)
            {
                index = i;
            }
        }
        dropdown.AddOptions(resolutions);
        dropdown.value = index;
        dropdown.onValueChanged.AddListener(delegate { ChangeResolution(dropdown.value); });
    }

    private void ChangeResolution(int index)
    {
        Debug.Log("Resolution changed " + index);
        Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, true);
    }


    private void ChangeState(MenuState newState)
    {
        menuState = newState;

        if (menuState == MenuState.MAINMENU)
        {
            mainMenu.gameObject.SetActive(true);
            settingsMenu.gameObject.SetActive(false);
        }

        else if (menuState == MenuState.SETTINGS)
        {
            mainMenu.gameObject.SetActive(false);
            settingsMenu.gameObject.SetActive(true);
        }
    }

    public void OnButtonPressed(Transform t)
    {
        switch (t.name)
        {
            case "Start Game":
                SceneManager.LoadScene(1);
                break;
            case "Settings":
                ChangeState(MenuState.SETTINGS);
                break;
            case "Back":
                ChangeState(MenuState.MAINMENU);
                break;
            case "Quit Game":
                Application.Quit();
                break;
        }
    }

    private enum MenuState
    {
        MAINMENU,
        SETTINGS
    }
}
