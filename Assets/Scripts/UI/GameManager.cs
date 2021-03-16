using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IDraggableButtonHandler, IButtonPressedHandler
{
    [SerializeField] private DraggableButton draggableButtonPrefab;
    [SerializeField] private LetterSlot letterSlotPrefab;
    [SerializeField] private string[] words;
    [SerializeField] private RectTransform letterArea;
    [SerializeField] private float spacing = 75f, range = 75f;
    private LetterSlot[] allLetterSlots;
    private string currentWord;

    private void Awake()
    {
        int index = Random.Range(0, words.Length);
        currentWord = words[index];
        allLetterSlots = new LetterSlot[currentWord.Length];

        for (int i = 0; i < currentWord.Length; i++)
        {
            DraggableButton newButton = Instantiate(draggableButtonPrefab, transform);
            newButton.SetText(currentWord[i].ToString());
            // Asetetaan alimmaiseksi hierarkiassa, täten se piirretään aina tyhjien paikkojen päälle.
            newButton.transform.SetAsLastSibling();
            
            float x = Random.Range(letterArea.rect.xMin, letterArea.rect.xMax) + letterArea.anchoredPosition.x;
            float y = Random.Range(letterArea.rect.yMin, letterArea.rect.yMax) + letterArea.anchoredPosition.y;
            newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

            LetterSlot empty = Instantiate(letterSlotPrefab, transform);
            allLetterSlots[i] = empty;
            empty.GetComponent<RectTransform>().anchoredPosition = new Vector2(-800f + (i * spacing), Screen.height * 0.4f);
            empty.transform.SetAsFirstSibling();
        }
    }


    public void OnBeginDrag(Transform t)
    {
    }

    public void OnDrag(Transform t)
    {
        for (int i = 0; i < allLetterSlots.Length; i++)
        {
            float dist = Vector2.Distance(t.position, allLetterSlots[i].transform.position);
            if (allLetterSlots[i].letterSlotState == LetterSlotState.EMPTY && dist < range)
            {
                allLetterSlots[i].ChangeState(LetterSlotState.NEAR);
            }
        }
    }

    public void OnEndDrag(Transform t)
    {
    }

    public void OnButtonPressed(Transform t)
    {
    }
}
