using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterSlot : MonoBehaviour
{
    [HideInInspector] public LetterSlotState letterSlotState;
    [HideInInspector] public string letter;
    [SerializeField] private Image image;
    [SerializeField] private Color empty, hasLetter, near;

    private void OnValidate()
    {
        image.color = empty;
    }

    public void ChangeState(LetterSlotState newState, string letter = "")
    {
        letterSlotState = newState;
        switch (letterSlotState)
        {
            case LetterSlotState.EMPTY:
                image.color = empty;
                letter = "";
                break;
            case LetterSlotState.HASLETTER:
                image.color = hasLetter;
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
