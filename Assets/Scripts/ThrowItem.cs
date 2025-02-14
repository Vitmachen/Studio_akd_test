using UnityEngine;
using UnityEngine.UI;

public class ThrowItem : MonoBehaviour
{
    public Button throwButton; // ������ �� ������ "�������"
    public float throwForce = 10f; // ���� ������
    private GameObject heldItem; // ����������� �������

    void Start()
    {
        // �������� ������ ��� ������
        throwButton.gameObject.SetActive(false);

        // ��������� ����� �� ������� ������
        throwButton.onClick.AddListener(ThrowHeldItem);
    }

    // ����������, ����� ������� ��������
    public void PickupItem(GameObject item)
    {
        heldItem = item;
        throwButton.gameObject.SetActive(true); // ���������� ������
    }

    // ���������� ��� ������� �� ������ "�������"
    private void ThrowHeldItem()
    {
        if (heldItem != null)
        {
            // �������� ������ � �������� (���� ��� ���� ���������)
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
            }

            // ���������� ����������� ������� � �������� ������
            heldItem = null;
            throwButton.gameObject.SetActive(false);
        }
    }
    
}
