using System;
using UnityEngine;

public class Zone : MonoBehaviour
{
    private int questId, objectiveId;
    public static Action<Zone,int, int> completeObjective;
    public void SetQuest(int qId, int oId)
    {
        questId = qId;
        objectiveId = oId;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        completeObjective.Invoke(this,questId, objectiveId);
    }
}
