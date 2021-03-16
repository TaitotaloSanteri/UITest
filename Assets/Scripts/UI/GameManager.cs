using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour, IDraggableButtonHandler, IButtonPressedHandler
{
    [SerializeField] private DraggableButton draggableButtonPrefab;
    [SerializeField] private LetterSlot letterSlotPrefab;
    [SerializeField] private string[] words;
    [SerializeField] private RectTransform letterArea;
    [SerializeField] private float spacing = 75f, range = 75f;
    private LetterSlot[] allLetterSlots;
    private string currentWord;
    private string activeWord;
    private bool isNear;

    private void Awake()
    {
        int index = Random.Range(0, words.Length);
        currentWord = words[index];
        for (int i = 0; i < currentWord.Length; i++)
        {
            activeWord += "*";
        }

        allLetterSlots = new LetterSlot[currentWord.Length];

        float size = (float)currentWord.Length * 0.5f;
        for (int i = 0; i < currentWord.Length; i++)
        {
            DraggableButton newButton = Instantiate(draggableButtonPrefab, transform);
            newButton.SetText(currentWord[i].ToString());
            // Asetetaan alimmaiseksi hierarkiassa, täten se piirretään aina tyhjien paikkojen päälle.
            newButton.transform.SetAsLastSibling();
            
            float x = Random.Range(letterArea.rect.xMin, letterArea.rect.xMax) + letterArea.anchoredPosition.x;
            float y = Random.Range(letterArea.rect.yMin, letterArea.rect.yMax) + letterArea.anchoredPosition.y;
            newButton.GetComponent<RectTransform>().anchoredPosition = newButton.startPosition = new Vector2(x, y);

            LetterSlot empty = Instantiate(letterSlotPrefab, transform);
            allLetterSlots[i] = empty;
            float imageSize = empty.image.rectTransform.sizeDelta.x;
            Debug.Log(imageSize);
            empty.GetComponent<RectTransform>().anchoredPosition = new Vector2(-size * (imageSize + spacing) + (i * (imageSize + spacing)), Screen.height * 0.4f);
            empty.transform.SetAsFirstSibling();
        }
    }

    private void Update()
    {
        if (activeWord == currentWord)
        {
            Debug.Log("JEE TIESIT SANAN OIKEIN!");
        }
    }

    public void OnBeginDrag(RectTransform t)
    {
        // Tarkistetaan kaikista letter sloteista, että onko niissä kirjaimia ja onko kyseinen 
        // kirjain sama nappula, kuin mitä aloitetaan raahaamaan.
        for (int i = 0; i < allLetterSlots.Length; i++)
        {
            if (allLetterSlots[i].letterSlotState == LetterSlotState.HASLETTER &&
                allLetterSlots[i].activeDraggableButton == t.GetComponent<DraggableButton>())
            {
                // Jos on, niin asetetaan letter slotin state emptyksi.
                allLetterSlots[i].ChangeState(LetterSlotState.EMPTY);
                ChangeCharacterInActiveWord('*', i);
            }
        }
    }

    public void OnDrag(RectTransform t)
    {
        for (int i = 0; i < allLetterSlots.Length; i++)
        {
            float dist = Vector2.Distance(t.anchoredPosition, allLetterSlots[i].rectTransform.anchoredPosition);
            if (!isNear && allLetterSlots[i].letterSlotState == LetterSlotState.EMPTY && dist < range)
            {
                allLetterSlots[i].ChangeState(LetterSlotState.NEAR);
                isNear = true;
            }
            else if (allLetterSlots[i].letterSlotState == LetterSlotState.NEAR && dist >= range)
            {
                allLetterSlots[i].ChangeState(LetterSlotState.EMPTY);
                isNear = false;
            }
        }
    }

    public void OnEndDrag(RectTransform t)
    {
        isNear = false;
        for (int i = 0; i < allLetterSlots.Length; i++)
        {
            if (allLetterSlots[i].letterSlotState == LetterSlotState.NEAR)
            {
                DraggableButton button = t.GetComponent<DraggableButton>();
                allLetterSlots[i].ChangeState(LetterSlotState.HASLETTER, button.GetText(), button);
                t.anchoredPosition = allLetterSlots[i].rectTransform.anchoredPosition;
                ChangeCharacterInActiveWord(button.GetText().ToCharArray()[0], i);
                return;
            }
        }
        DraggableButton btn = t.GetComponent<DraggableButton>();
        t.anchoredPosition = btn.startPosition;
    }

    // Funktio, jolla vaihdetaan kirjain kerralla arvattavasta sanasta.
    private void ChangeCharacterInActiveWord(char letter, int index)
    {
        StringBuilder stringBuilder = new StringBuilder(activeWord);
        stringBuilder[index] = letter;
        activeWord = stringBuilder.ToString();
        Debug.Log(activeWord);
    }

    public void OnButtonPressed(Transform t)
    {
    }
}
