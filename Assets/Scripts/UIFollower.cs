using UnityEngine;

public class UIFollower : MonoBehaviour
{
    public GameObject targetObject;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (targetObject == null)
        {
            rectTransform.position = new Vector3(-10000, -10000, 0);
            return;
        }
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);
        rectTransform.position = screenPosition;
    }
}
