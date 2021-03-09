using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour, IDraggableButtonHandler, IButtonPressedHandler
{
    [SerializeField] private DraggableButton draggableButtonPrefab;
    [SerializeField] private Transform emptyPrefab;
    [SerializeField] private string[] words;
    [SerializeField] private RectTransform letterArea;
    [SerializeField] private float spacing = 75f;
    private string currentWord;

    private void Awake()
    {
        int index = Random.Range(0, words.Length);
        currentWord = words[index];
        Debug.Log(letterArea.rect.yMin + ", " + letterArea.rect.yMax);
        for (int i = 0; i < currentWord.Length; i++)
        {
            DraggableButton newButton = Instantiate(draggableButtonPrefab, transform);
            newButton.SetText(currentWord[i].ToString());
            float x = Random.Range(letterArea.rect.xMin, letterArea.rect.xMax) + letterArea.anchoredPosition.x;
            float y = Random.Range(letterArea.rect.yMin, letterArea.rect.yMax) + letterArea.anchoredPosition.y;
            newButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);

            Transform empty = Instantiate(emptyPrefab, transform);
            empty.GetComponent<RectTransform>().anchoredPosition = new Vector2(-800f + (i * spacing), Screen.height * 0.4f);
        }
    }


    public void OnBeginDrag(Transform t)
    {
    }

    public void OnDrag(Transform t)
    {
    }

    public void OnEndDrag(Transform t)
    {
    }

    public void OnButtonPressed(Transform t)
    {
    }
}
