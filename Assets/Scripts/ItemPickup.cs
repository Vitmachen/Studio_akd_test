using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPickup : MonoBehaviour
{
    public float pickupDistance = 3f; // Дистанция захвата
    public LayerMask interactableLayer; // Слой для взаимодействия
    public float holdDistance = 2f; // Дистанция удержания предмета
    public float throwForce = 10f; // Сила броска
    public Button throwButton; // Кнопка "Бросить"
    public float smoothSpeed = 10f; // Скорость плавного перемещения

    private GameObject heldItem; // Захваченный предмет
    private Rigidbody heldItemRb; // Rigidbody захваченного предмета

    void Start()
    {
        // Скрываем кнопку при старте
        throwButton.gameObject.SetActive(false);

        // Назначаем метод на нажатие кнопки
        throwButton.onClick.AddListener(ThrowItem);
    }

    void Update()
    {
        // Проверяем клик мышью или касание
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

        // Если предмет в руке, перемещаем его
        if (heldItem != null)
        {
            MoveHeldItem();
        }
    }

    private void TryPickupItem()
    {
        // Получаем позицию клика (мышь или касание)
        Vector2 inputPosition;
        if (Input.touchCount > 0)
        {
            inputPosition = Input.GetTouch(0).position;
        }
        else
        {
            inputPosition = Input.mousePosition;
        }

        // Создаем луч из позиции клика
        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupDistance, interactableLayer))
        {
            // Проверяем, есть ли у объекта Rigidbody
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                heldItem = hit.collider.gameObject;
                heldItemRb = rb;
                heldItemRb.isKinematic = true; // Отключаем физику
                heldItemRb.interpolation = RigidbodyInterpolation.Interpolate; // Включаем интерполяцию
                throwButton.gameObject.SetActive(true); // Показываем кнопку
            }
        }
    }

    private void MoveHeldItem()
    {
        // Позиция предмета перед камерой
        Vector3 targetPosition = Camera.main.transform.position + Camera.main.transform.forward * holdDistance;

        // Плавное перемещение
        heldItemRb.position = Vector3.Lerp(heldItemRb.position, targetPosition, smoothSpeed * Time.deltaTime);
    }

    private void ThrowItem()
    {
        if (heldItem != null)
        {
            // Включаем физику и бросаем предмет
            heldItemRb.isKinematic = false;
            heldItemRb.interpolation = RigidbodyInterpolation.None; // Отключаем интерполяцию
            heldItemRb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);

            // Сбрасываем захваченный предмет и скрываем кнопку
            heldItem = null;
            heldItemRb = null;
            throwButton.gameObject.SetActive(false);
        }
    }

    // Проверка, находится ли клик/касание над UI
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
