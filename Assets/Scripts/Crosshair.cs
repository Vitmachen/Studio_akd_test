using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    //public float size = 10f; // Размер точки
    //public Color color = Color.white; // Цвет точки

    //private Image crosshairImage;

    public float interactionDistance = 5f; // Дистанция взаимодействия
    /*
    void Start()
    {
        // Создаем Canvas, если его нет
        GameObject canvasGO = new GameObject("CrosshairCanvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // Создаем Image для точки
        GameObject imageGO = new GameObject("CrosshairImage");
        imageGO.transform.SetParent(canvasGO.transform);
        crosshairImage = imageGO.AddComponent<Image>();
        crosshairImage.color = color;

        // Устанавливаем размер и позицию
        RectTransform rectTransform = crosshairImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(size, size);
        rectTransform.anchoredPosition = Vector2.zero;
    }
    */
    private void Update()
    {
        // Создаем луч из центра экрана
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // Проверяем, попал ли луч в объект
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Debug.Log("Объект обнаружен: " + hit.collider.name);
            // Здесь можно добавить логику взаимодействия
        }
    }
}
