using UnityEngine;

public interface IButtonPressedHandler
{
    void OnButtonPressed(Transform t);
}
public interface IDraggableButtonHandler
{
    void OnBeginDrag(Transform t);
    void OnDrag(Transform t);
    void OnEndDrag(Transform t);
}