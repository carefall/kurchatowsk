using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReplicaUI : MonoBehaviour
{

    [SerializeField] Image avatar;
    [SerializeField] TextMeshProUGUI Name, text;
    [SerializeField] RectTransform ui;

    public void Fill(Sprite sprite, string Name, string text)
    {
        avatar.sprite = sprite;
        this.Name.text = Name;
        this.text.text = text;
        Vector2 size = this.text.rectTransform.sizeDelta;
        size.y = this.text.preferredHeight;
        this.text.rectTransform.sizeDelta = size;
        size = ui.sizeDelta;
        size.y = 110 + this.text.rectTransform.sizeDelta.y;
        ui.sizeDelta = size;
    }

}
