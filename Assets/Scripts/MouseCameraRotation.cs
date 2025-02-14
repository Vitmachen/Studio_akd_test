using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f; // Чувствительность мыши/касаний
    public Transform playerBody; // Ссылка на объект персонажа

    private float xRotation = 0f;
    private Vector2 lastInputPosition;
    private bool isRotating = false;

    void Update()
    {
        // Для Android (касания)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Проверяем, что касание не на джойстике
            if (!IsPointerOverUIObject(touch.position))
            {
                HandleInput(touch.position, touch.phase);
            }
        }
        // Для редактора (мышь)
        else if (Input.GetMouseButton(0)) // Левая кнопка мыши зажата
        {
            // Проверяем, что мышь не над UI
            if (!IsPointerOverUIObject(Input.mousePosition))
            {
                HandleInput(Input.mousePosition, TouchPhase.Moved);
            }
        }
        else
        {
            // Сбрасываем состояние вращения, если кнопка мыши отпущена или касание завершено
            isRotating = false;
        }
    }

    private void HandleInput(Vector2 inputPosition, TouchPhase phase)
    {
        if (phase == TouchPhase.Began || !isRotating)
        {
            // Запоминаем начальную позицию при первом касании или нажатии
            lastInputPosition = inputPosition;
            isRotating = true;
        }
        else if (phase == TouchPhase.Moved && isRotating)
        {
            // Вычисляем разницу между текущей и предыдущей позицией
            Vector2 delta = inputPosition - lastInputPosition;

            // Вращение по вертикали (камера)
            xRotation -= delta.y * mouseSensitivity * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Ограничение угла

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Вращение по горизонтали (персонаж)
            playerBody.Rotate(Vector3.up * delta.x * mouseSensitivity * Time.deltaTime);

            // Обновляем предыдущую позицию для следующего кадра
            lastInputPosition = inputPosition;
        }
    }

    // Проверка, находится ли касание/мышь над UI
    private bool IsPointerOverUIObject(Vector2 position)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }

        // Для касаний на Android
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            return true;
        }

        return false;
    }
}
