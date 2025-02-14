using UnityEngine;
using UnityEngine.EventSystems;

public class MouseCameraRotation : MonoBehaviour
{
    public float mouseSensitivity = 100f; // ���������������� ����/�������
    public Transform playerBody; // ������ �� ������ ���������

    private float xRotation = 0f;
    private Vector2 lastInputPosition;
    private bool isRotating = false;

    void Update()
    {
        // ��� Android (�������)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // ���������, ��� ������� �� �� ���������
            if (!IsPointerOverUIObject(touch.position))
            {
                HandleInput(touch.position, touch.phase);
            }
        }
        // ��� ��������� (����)
        else if (Input.GetMouseButton(0)) // ����� ������ ���� ������
        {
            // ���������, ��� ���� �� ��� UI
            if (!IsPointerOverUIObject(Input.mousePosition))
            {
                HandleInput(Input.mousePosition, TouchPhase.Moved);
            }
        }
        else
        {
            // ���������� ��������� ��������, ���� ������ ���� �������� ��� ������� ���������
            isRotating = false;
        }
    }

    private void HandleInput(Vector2 inputPosition, TouchPhase phase)
    {
        if (phase == TouchPhase.Began || !isRotating)
        {
            // ���������� ��������� ������� ��� ������ ������� ��� �������
            lastInputPosition = inputPosition;
            isRotating = true;
        }
        else if (phase == TouchPhase.Moved && isRotating)
        {
            // ��������� ������� ����� ������� � ���������� ��������
            Vector2 delta = inputPosition - lastInputPosition;

            // �������� �� ��������� (������)
            xRotation -= delta.y * mouseSensitivity * Time.deltaTime;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // ����������� ����

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // �������� �� ����������� (��������)
            playerBody.Rotate(Vector3.up * delta.x * mouseSensitivity * Time.deltaTime);

            // ��������� ���������� ������� ��� ���������� �����
            lastInputPosition = inputPosition;
        }
    }

    // ��������, ��������� �� �������/���� ��� UI
    private bool IsPointerOverUIObject(Vector2 position)
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }

        // ��� ������� �� Android
        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            return true;
        }

        return false;
    }
}
