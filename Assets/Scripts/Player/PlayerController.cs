using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(PlayerInput), typeof(Collider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject mesh;
    [SerializeField] Camera cam;

    [SerializeField] private float speed, sensitivityX, sensitivityY, followSpeed;

    private Rigidbody rb;
    private CapsuleCollider col;

    private Vector2 direction, pDelta;
    private Vector3 cameraRot;

    [HideInInspector] public Transform entityFace;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (PlayerData.instance.viewingDialogue)
        {
            if (entityFace != null)
            {
                cam.transform.LookAt(entityFace);
                cameraRot = cam.transform.localEulerAngles;
            }
            return;
        }
        Vector3 moveDirection = speed * (transform.right * direction.x + transform.forward * direction.y).normalized;
        float y = rb.linearVelocity.y;
        moveDirection.y = y;
        rb.linearVelocity = moveDirection;
        transform.eulerAngles += new Vector3(0, pDelta.x * sensitivityX, 0);
        cameraRot.x += -pDelta.y * sensitivityY;
        cameraRot.x = Mathf.Clamp(cameraRot.x, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(cameraRot);
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        direction = ctx.ReadValue<Vector2>();
    }

    public void Look(InputAction.CallbackContext ctx)
    {
        pDelta = ctx.ReadValue<Vector2>();
    }
}
