using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DragItem : MonoBehaviour
{
    [SerializeField] Image image;
    public void SetUp(Sprite sprite, int x, int y)
    {
        image.sprite = sprite;
        image.rectTransform.sizeDelta = new Vector2(x, y);
        gameObject.SetActive(true);
    }
    public void Stop()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        image.rectTransform.position = Mouse.current.position.value;
    }
}
