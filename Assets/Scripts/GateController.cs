using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateController : MonoBehaviour
{
    public Transform leftGate; // ����� ������� �����
    public Transform rightGate; // ������ ������� �����
    public float openAngle = 90f; // ���� �������� �����
    public float openSpeed = 2f; // �������� ��������

    private bool isOpen = false; // ��������� ����� (�������/�������)
    private Quaternion leftGateStartRotation; // ��������� ������� ����� �������
    private Quaternion rightGateStartRotation; // ��������� ������� ������ �������

    void Start()
    {
        // ��������� ��������� �������� �������
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

        // ������� �������� ��� �������
        Quaternion leftGateTargetRotation = leftGateStartRotation * Quaternion.Euler(0, -openAngle, 0);
        Quaternion rightGateTargetRotation = rightGateStartRotation * Quaternion.Euler(0, openAngle, 0);

        // ������� �������� �����
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * openSpeed;
            leftGate.rotation = Quaternion.Slerp(leftGateStartRotation, leftGateTargetRotation, elapsedTime);
            rightGate.rotation = Quaternion.Slerp(rightGateStartRotation, rightGateTargetRotation, elapsedTime);
            yield return null;
        }

        // ��������� �������� ��������
        leftGate.rotation = leftGateTargetRotation;
        rightGate.rotation = rightGateTargetRotation;
    }
}
