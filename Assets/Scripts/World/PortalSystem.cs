using UnityEngine;

public class PortalSystem : MonoBehaviour
{
    public static PortalSystem instance;

    void Awake()
    {
        instance = this;
    }

    public Transform GetPortal(string sceneName)
    {
        foreach (Portal portal in FindObjectsByType<Portal>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (portal.targetScene == sceneName) return portal.transform;
        }
        return null;
    }
}
