using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuHandler : MonoBehaviour, IButtonPressedHandler
{
    [SerializeField] private Transform mainMenu, settingsMenu;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Slider brightnessSlider, fontSlider;
    [SerializeField] private Image brightnessImage;
    [SerializeField] private TextMeshProUGUI brightnessText, fontText;
    [SerializeField] private TMP_InputField inputField;
    private TextMeshProUGUI[] allTexts;
    private float[] allTextOriginalSizes;
    private float savedBrightnessValue, savedFontSizeValue;

    private MenuState menuState;

    private void Awake()
    {
        savedFontSizeValue = PlayerPrefs.GetFloat("FontSize", 1f);
        savedBrightnessValue = PlayerPrefs.GetFloat("Brightness", 1f);
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
        //brightnessSlider.onValueChanged.AddListener(delegate { ChangeBrightness(brightnessSlider.value); });
        fontSlider.onValueChanged.AddListener(delegate { ChangeFontSize(fontSlider.value); });
        inputField.onValueChanged.AddListener(delegate { ChangeInputField(inputField.text); });
        inputField.onEndEdit.AddListener(delegate { EndInputField(inputField.text); });

        brightnessSlider.value = savedBrightnessValue;
    }

    private void EndInputField(string text)
    {
        Debug.Log($"Lopetit kirjoittamisen. Kirjoitit: {text}");
    }

    private void ChangeInputField(string text)
    {
        Debug.Log(text);
    }

    private void ChangeFontSize(float value)
    {
        fontText.text = $"Font size {NormalizeValue(value, fontSlider.minValue, fontSlider.maxValue) * 100:00}";
        for (int i = 0; i < allTexts.Length; i++)
        {
            allTexts[i].fontSize = allTextOriginalSizes[i] * value;
        }
        PlayerPrefs.SetFloat("FontSize", value);
    }

    private float NormalizeValue(float value, float minValue, float maxValue)
    {
        float val = value - minValue;
        float val0 = maxValue - minValue;
        return val / val0;
    }

    private void Start()
    {
        allTexts = FindObjectsOfType<TextMeshProUGUI>(true);
        allTextOriginalSizes = new float[allTexts.Length];
        for (int i = 0; i < allTexts.Length; i++)
        {
            allTextOriginalSizes[i] = allTexts[i].fontSize;
        }
        fontSlider.value = savedFontSizeValue;
    }

    public void ChangeBrightness(float value)
    {
        brightnessText.text = $"Brightness {value * 100:00}";
        brightnessImage.color = Color.black * (brightnessSlider.maxValue - value);
        PlayerPrefs.SetFloat("Brightness", value);
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
