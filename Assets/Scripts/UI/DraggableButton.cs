using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButton : CustomButton, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private IDraggableButtonHandler draggableButtonHandler;
    [SerializeField] private RectTransform rectTransform;

    private void OnValidate()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Awake()
    {
        draggableButtonHandler = GetComponentInParent<IDraggableButtonHandler>();
    }
    public void SetText(string _text)
    {
        text.text = _text;
    }
    public string GetText()
    {
        return text.text;
    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        draggableButtonHandler.OnDrag(rectTransform);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        draggableButtonHandler.OnEndDrag(rectTransform);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggableButtonHandler.OnBeginDrag(rectTransform);
    }
}
