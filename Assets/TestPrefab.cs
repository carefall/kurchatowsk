using UnityEngine;
using UnityEngine.InputSystem;

public class TestPrefab : MonoBehaviour
{
    [SerializeField] GameObject testCube2;
    GameObject clone;
        public void Control(InputAction.CallbackContext context)
        {
            if (context.started)
            {
             clone=Instantiate(testCube2);
            }
            if (context.performed)
            {
            clone.GetComponent<AudioSource>().Play();
            }
            if (context.canceled)
            {
                Destroy(clone);
            }
        }
}
