using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость перемещения
    public Transform cameraTransform; // Ссылка на камеру

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Получаем ввод от джойстика
        float horizontal = SimpleInput.GetAxis("Horizontal");
        float vertical = SimpleInput.GetAxis("Vertical");

        // Вычисляем направление движения относительно камеры
        Vector3 moveDirection = cameraTransform.forward * vertical + cameraTransform.right * horizontal;
        moveDirection.y = 0; // Игнорируем вертикальное движение

        // Нормализуем вектор и применяем скорость
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        // Применяем движение
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
