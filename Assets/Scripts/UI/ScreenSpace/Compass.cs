using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] RectTransform targetMarker;
    [SerializeField] RectTransform directions;
    private Camera cam;

    public static Compass instance;

    private void Start()
    {
        cam = Camera.main;
        instance = this;
    }

    public void Fill(Camera cam)
    {
        this.cam = cam;
    }

    private void Update()
    {
        if (cam == null) return;
        Vector3 dirPos = directions.localPosition;
        float rotation = cam.transform.eulerAngles.y;
        if (rotation <= 270)
        {
            dirPos.x = -10 * rotation;
        } else
        {
            dirPos.x = (-10 * rotation) + 3600;
        }
        directions.localPosition = dirPos;
        if (PlayerData.instance == null) return;
        Objective objective = PlayerData.instance.GetCurrentObjective();
        if (objective == null || objective.GetCoordinates() == new Vector3(-9999, -9999, -9999))
        {
            targetMarker.gameObject.SetActive(false);
            return;
        }
        targetMarker.gameObject.SetActive(true);
        Vector3 target = objective.GetCoordinates();
        target.y = 0;
        Vector3 player = cam.transform.position;
        player.y = 0;
        Vector3 forward = cam.transform.forward;
        Vector3 targetLine = (target - player).normalized;
        float angle = Vector3.SignedAngle(forward, targetLine, Vector3.up);
        float x = Mathf.Clamp(angle * 10, -300, 300);
        Vector3 pos = targetMarker.localPosition;
        pos.x = x;
        targetMarker.localPosition = pos;
    }
}
