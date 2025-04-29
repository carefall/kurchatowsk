using UnityEngine;

public class NotEnoughSpaceText : MonoBehaviour
{

    public static NotEnoughSpaceText instance;

    [SerializeField] private float duration;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Invoke(nameof(Hide), duration);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    

}
