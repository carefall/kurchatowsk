using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Camera cam;
    private CharacterController controller;
    private Vector2 direction, pointer;
    [SerializeField] private float moveSpeed = 100;
    private void Start()
    {
        cam = Camera.main;
        controller = GetComponent<CharacterController>();

    }
    public void Move(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();

    }
    public void Look(InputAction.CallbackContext context)
    {
        pointer = context.ReadValue<Vector2>();

    }
    private void Update()
    {
        controller.Move((transform.forward * direction.y + transform.right * direction.x) * Time.deltaTime * moveSpeed);
    }
}
