using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickRectTransform;
    private Vector2 inputVector;

    void Start()
    {
        joystickRectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickRectTransform, eventData.position, eventData.pressEventCamera, out position);

        position.x = (position.x / joystickRectTransform.sizeDelta.x);
        position.y = (position.y / joystickRectTransform.sizeDelta.y);

        inputVector = new Vector2(position.x * 2 - 1, position.y * 2 - 1);
        inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

        // Burada inputVector deðiþkeni, joystick'in yatay ve dikey hareketlerini temsil eder.
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
    }

    public float GetHorizontal()
    {
        return inputVector.x;
    }

    public float GetVertical()
    {
        return inputVector.y;
    }
}
