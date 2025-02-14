using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public float pickupDistance = 3f; // ��������� �������
    public LayerMask interactableLayer; // ���� ��� ��������������
    public float holdDistance = 2f; // ��������� ��������� ��������
    public float throwForce = 10f; // ���� ������
    public Button throwButton; // ������ "�������"
    public float smoothSpeed = 10f; // �������� �������� �����������

    private GameObject heldItem; // ����������� �������
    private Rigidbody heldItemRb; // Rigidbody ������������ ��������

    void Start()
    {
        // �������� ������ ��� ������
        throwButton.gameObject.SetActive(false);

        // ��������� ����� �� ������� ������
        throwButton.onClick.AddListener(ThrowItem);
    }

    void Update()
    {
        // ��������� ���� ����� ��� �������
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            if (!IsPointerOverUIObject())
            {
                if (heldItem == null)
                {
                    TryPickupItem();
                }
            }
        }

        // ���� ������� � ����, ���������� ���
        if (heldItem != null)
        {
            MoveHeldItem();
        }
    }

    private void TryPickupItem()
    {
        // �������� ������� ����� (���� ��� �������)
        Vector2 inputPosition;
        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position;
        }
        else
        {
            inputPosition = Input.mousePosition;
        }

        // ������� ��� �� ������� �����
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance, interactableLayer))
        {
            // ���������, ���� �� � ������� Rigidbody
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                heldItem = hit.collider.gameObject;
                heldItemRb = rb;
                heldItemRb.isKinematic = true; // ��������� ������
                heldItemRb.interpolation = RigidbodyInterpolation.Interpolate; // �������� ������������
                throwButton.gameObject.SetActive(true); // ���������� ������
            }
        }
    }

    private void MoveHeldItem()
    {
        // ������� �������� ����� �������
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;

        // ������� �����������
        heldItemRb.position = Vector3.Lerp(heldItemRb.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

    private void ThrowItem()
    {
        if (heldItem != null)
        {
            // �������� ������ � ������� �������
            heldItemRb.isKinematic = false;
            heldItemRb.interpolation = RigidbodyInterpolation.None; // ��������� ������������
            heldItemRb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

            // ���������� ����������� ������� � �������� ������
            heldItem = null;
            heldItemRb = null;
            throwButton.gameObject.SetActive(false);
        }
    }

    // ��������, ��������� �� ����/������� ��� UI
    private bool IsPointerOverUIObject()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }

        if (Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        {
            return true;
        }

        return false;
    }
}
