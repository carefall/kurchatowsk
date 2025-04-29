using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{

    public ItemSO item;
    public int amount;

    [SerializeField] TextMeshProUGUI amountText;
    [SerializeField] Image sprite;

    public void Process()
    {
        sprite.sprite = item.sprite;
        amountText.text = $"x{amount}";
    }

}
