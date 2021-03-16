using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableButton : CustomButton, IDragHandler
{
    private IDraggableButtonHandler draggableButtonHandler;

    private void Awake()
    {
        draggableButtonHandler = GetComponentInParent<IDraggableButtonHandler>();
    }
    public void SetText(string _text)
    {
        text.text = _text;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        draggableButtonHandler.OnDrag(transform);
    }
}
