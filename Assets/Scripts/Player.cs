using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _movementSpeed;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movementVector = Vector3.zero;

        movementVector = new Vector3(SimpleInput.GetAxis("Horizontal"), 0f, SimpleInput.GetAxis("Vertical"));
        _controller.Move(movementVector * _movementSpeed * Time.deltaTime);
    }

    private void Look()
    {

        
    }

    void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");

    }
}

