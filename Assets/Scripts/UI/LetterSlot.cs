using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterSlot : MonoBehaviour
{
    [HideInInspector] public LetterSlotState letterSlotState;
    [HideInInspector] public string letter;
    [HideInInspector] public DraggableButton activeDraggableButton;
    [SerializeField] public Image image;
    [SerializeField] private Color empty, hasLetter, near;
    [SerializeField] public RectTransform rectTransform;

    private void OnValidate()
    {
        image.color = empty;
        rectTransform = GetComponent<RectTransform>();
    }

    public void ChangeState(LetterSlotState newState, string letter = "", DraggableButton button = null)
    {
        letterSlotState = newState;
        switch (letterSlotState)
        {
            case LetterSlotState.EMPTY:
                image.color = empty;
                activeDraggableButton = null;
                letter = "";
                break;
            case LetterSlotState.HASLETTER:
                image.color = hasLetter;
                if (button != null)
                {
                    activeDraggableButton = button;
                }
                this.letter = letter;
                break;
            case LetterSlotState.NEAR:
                image.color = near;
                break;
        }
    }
}
public enum LetterSlotState
{
    EMPTY,
    HASLETTER,
    NEAR
}
