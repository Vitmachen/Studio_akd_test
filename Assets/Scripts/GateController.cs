using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public Transform leftGate; // Левая створка ворот
    public Transform rightGate; // Правая створка ворот
    public float openAngle = 90f; // Угол открытия ворот
    public float openSpeed = 2f; // Скорость открытия

    private bool isOpen = false; // Состояние ворот (открыты/закрыты)
    private Quaternion leftGateStartRotation; // Начальный поворот левой створки
    private Quaternion rightGateStartRotation; // Начальный поворот правой створки

    void Start()
    {
        // Сохраняем начальные повороты створок
        leftGateStartRotation = leftGate.rotation;
        rightGateStartRotation = rightGate.rotation;

        Invoke(nameof(OpenGate), 0.5f);
    }


    public void OpenGate()
    {
        if (!isOpen)
        {
            isOpen = true;
            StartCoroutine(OpenGateAnimation());
        }
    }

    private IEnumerator OpenGateAnimation()
    {
        float elapsedTime = 0f;

        // Целевые повороты для створок
        Quaternion leftGateTargetRotation = leftGateStartRotation * Quaternion.Euler(0, -openAngle, 0);
        Quaternion rightGateTargetRotation = rightGateStartRotation * Quaternion.Euler(0, openAngle, 0);

        // Плавное открытие ворот
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * openSpeed;
            leftGate.rotation = Quaternion.Slerp(leftGateStartRotation, leftGateTargetRotation, elapsedTime);
            rightGate.rotation = Quaternion.Slerp(rightGateStartRotation, rightGateTargetRotation, elapsedTime);
            yield return null;
        }

        // Фиксируем конечные повороты
        leftGate.rotation = leftGateTargetRotation;
        rightGate.rotation = rightGateTargetRotation;
    }
}
