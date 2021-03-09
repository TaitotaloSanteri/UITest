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
    [SerializeField] protected Image backgroundImage;
    [SerializeField] protected TextMeshProUGUI text;
    [Space]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color onEnterColor;
    [SerializeField] private Color onDownColor;
    [SerializeField] private IButtonPressedHandler handler;

    private void Start()
    {
        handler = GetComponentInParent<IButtonPressedHandler>();
        if (name == "ButtonPrefab") return;
     
        if (handler == null) 
            Debug.Log("No handler for " + name);
    }

    private void OnValidate()
    {
        backgroundImage.color = normalColor;
    }

    private void OnEnable()
    {
        backgroundImage.color = normalColor;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        backgroundImage.color = onDownColor;
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        backgroundImage.color = onEnterColor;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        backgroundImage.color = normalColor;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        backgroundImage.color = onEnterColor;
        handler.OnButtonPressed(transform);
    }
}
