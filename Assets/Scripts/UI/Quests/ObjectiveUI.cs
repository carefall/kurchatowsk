using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveUI : MonoBehaviour
{

    [SerializeField] private Image stateSprite;
    [SerializeField] private TextMeshProUGUI objectiveName;

    public void Build(Objective objective, Sprite[] sprites)
    {
        objectiveName.text = objective.Name;
        int state = objective.state > -1? objective.state : 0;
        stateSprite.sprite = sprites[state];
    }

}
