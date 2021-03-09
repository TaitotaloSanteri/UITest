using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuHandler : MonoBehaviour, IButtonPressedHandler
{
    [SerializeField] private Transform mainMenu, settingsMenu;
    private MenuState menuState;

    private void Awake()
    {
        ChangeState(MenuState.MAINMENU);
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
            case "Settings":
                ChangeState(MenuState.SETTINGS);
                break;
            case "Back":
                ChangeState(MenuState.MAINMENU);
                break;
        }
    }

    private enum MenuState
    {
        MAINMENU,
        SETTINGS
    }
}
