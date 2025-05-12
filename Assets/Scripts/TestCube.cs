using UnityEngine;
using UnityEngine.InputSystem;

public class TestCube : MonoBehaviour
{
    Vector2 Wsad;
    float AD;
    [SerializeField] float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(AD * Time.deltaTime * speed, 0, 0);
        float ws = Wsad.y * Time.deltaTime * speed;
        float ad = Wsad.x * Time.deltaTime * speed;
        transform.Translate(ad, 0, -ws);
    }
    public void Control(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            print("hello");
        }
        if (context.performed)
        {
            print("completed");
        }
        if (context.canceled)
        {
            print("goodbye");
        }
    }
    public void Lr(InputAction.CallbackContext context)
    {
        AD = context.ReadValue<float>();
       
    }
    public void Wasd(InputAction.CallbackContext context)
    {
        Wsad = context.ReadValue<Vector2>();

    }
}
