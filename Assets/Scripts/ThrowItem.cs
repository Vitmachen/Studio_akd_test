using UnityEngine;
using UnityEngine.UI;

public class ThrowItem : MonoBehaviour
{
    public Button throwButton; // Ссылка на кнопку "Бросать"
    public float throwForce = 10f; // Сила броска
    private GameObject heldItem; // Захваченный предмет

    void Start()
    {
        // Скрываем кнопку при старте
        throwButton.gameObject.SetActive(false);

        // Назначаем метод на нажатие кнопки
        throwButton.onClick.AddListener(ThrowHeldItem);
    }

    // Вызывается, когда предмет захвачен
    public void PickupItem(GameObject item)
    {
        heldItem = item;
        throwButton.gameObject.SetActive(true); // Показываем кнопку
    }

    // Вызывается при нажатии на кнопку "Бросать"
    private void ThrowHeldItem()
    {
        if (heldItem != null)
        {
            // Включаем физику у предмета (если она была отключена)
            Rigidbody rb = heldItem.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(Camera.main.transform.forward * throwForce, ForceMode.Impulse);
            }

            // Сбрасываем захваченный предмет и скрываем кнопку
            heldItem = null;
            throwButton.gameObject.SetActive(false);
        }
    }
    
}
