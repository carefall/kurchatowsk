using UnityEngine;

public class QuestZone : MonoBehaviour
{
    private int questId;
    private int objectiveId;

    internal void SetIds(int qId, int objId)
    {
        questId = qId;
        objectiveId = objId;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerData>().CompleteObjective(questId, objectiveId);
            Destroy(gameObject);
        }
    }
}
