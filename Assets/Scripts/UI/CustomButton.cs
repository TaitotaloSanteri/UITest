using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CustomButton : MonoBehaviour, 
                            IPointerDownHandler, 
                            IPointerEnterHandler, 
                            IPointerExitHandler, 
                            IPointerUpHandler
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI text;
    [Space]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color onEnterColor;
    [SerializeField] private Color onDownColor;
    [SerializeField] private IButtonPressedHandler handler;

    private void OnValidate()
    {
        if (name == "ButtonPrefab") return;
        handler = GetComponentInParent<IButtonPressedHandler>();
        if (handler == null) 
            Debug.Log("No handler for " + name);
        backgroundImage.color = normalColor;
    }

    private void OnEnable()
    {
        backgroundImage.color = normalColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        backgroundImage.color = onDownColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backgroundImage.color = onEnterColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backgroundImage.color = normalColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        backgroundImage.color = onEnterColor;
        handler.OnButtonPressed(transform);
    }
}
