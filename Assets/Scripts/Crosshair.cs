using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    //public float size = 10f; // ������ �����
    //public Color color = Color.white; // ���� �����

    //private Image crosshairImage;

    public float interactionDistance = 5f; // ��������� ��������������
    /*
    void Start()
    {
        // ������� Canvas, ���� ��� ���
        GameObject canvasGO = new GameObject("CrosshairCanvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();

        // ������� Image ��� �����
        GameObject imageGO = new GameObject("CrosshairImage");
        imageGO.transform.SetParent(canvasGO.transform);
        crosshairImage = imageGO.AddComponent<Image>();
        crosshairImage.color = color;

        // ������������� ������ � �������
        RectTransform rectTransform = crosshairImage.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(size, size);
        rectTransform.anchoredPosition = Vector2.zero;
    }
    */
    private void Update()
    {
        // ������� ��� �� ������ ������
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        // ���������, ����� �� ��� � ������
        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            Debug.Log("������ ���������: " + hit.collider.name);
            // ����� ����� �������� ������ ��������������
        }
    }
}
