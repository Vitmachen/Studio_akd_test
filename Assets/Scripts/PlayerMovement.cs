using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �������� �����������
    public Transform cameraTransform; // ������ �� ������

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // �������� ���� �� ���������
        float horizontal = SimpleInput.GetAxis("Horizontal");
        float vertical = SimpleInput.GetAxis("Vertical");

        // ��������� ����������� �������� ������������ ������
        Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        moveDirection.y = 0; // ���������� ������������ ��������

        // ����������� ������ � ��������� ��������
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // ��������� ��������
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
