using UnityEngine;

public interface IButtonPressedHandler
{
    void OnButtonPressed(Transform t);
}
public interface IDraggableButtonHandler
{
    void OnBeginDrag(RectTransform t);
    void OnDrag(RectTransform t);
    void OnEndDrag(RectTransform t);
}