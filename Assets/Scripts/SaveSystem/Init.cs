using UnityEngine;

public class Init : MonoBehaviour
{

    [SerializeField] private GameObject playerPrefab;
    private SaveData save;

    void Awake()
    {
        save = SaveSystem.GetSaveData();
    }

    private void Start()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(save.x, save.y, save.z), Quaternion.identity);
        PlayerData data = player.GetComponent<PlayerData>();
        data.Fill(save.hp, save.level, save.quests, save.currentQuest, save.completedQuests, save.failedQuests, save.deadUniqueNPCs, save.inventoryContent);
        player.transform.localEulerAngles = new Vector3(save.xRot, save.yRot, save.zRot);
        //
        EntitySystem.InitPopulateWorld(save);
        //
        Destroy(gameObject);
    }
}
