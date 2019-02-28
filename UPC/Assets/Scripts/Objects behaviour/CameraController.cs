using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Transform target;
    public Transform border;
    private float speed = 2.0f;
    private Rect rect;
    Vector2 cameraPointMin;
    Vector2 cameraPointMax;
    Vector3 positionTarget;
    public float verticalDelta;
    public float horizontalDelta;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
        rect = border.GetComponent<RectTransform>().rect;
        positionTarget = new Vector3();
        verticalDelta = transform.GetComponent<Camera>().orthographicSize;
        //Resolution resolution = Screen.currentResolution;
        
        //horizontalDelta = Screen.width / (Screen.height / verticalDelta);


    }

    void Update()
    {
        horizontalDelta = Screen.width / (Screen.height / verticalDelta);
        if (target != null)
        {
            positionTarget = target.position;
            positionTarget.z = -10f;
        }
        //range control
        if (positionTarget.x < rect.xMin + horizontalDelta)
        {
            positionTarget.x = rect.xMin + horizontalDelta;
        }
        if (positionTarget.y < rect.yMin + verticalDelta)
        {
            positionTarget.y = rect.yMin + verticalDelta;
        }

        if (positionTarget.x > rect.xMax - horizontalDelta)
        {
            positionTarget.x = rect.xMax - horizontalDelta;
        }
        if (positionTarget.y > rect.yMax - verticalDelta)
        {
            positionTarget.y = rect.yMax - verticalDelta;
        }

        transform.position = Vector3.Lerp(transform.position, positionTarget, speed * Time.deltaTime);
    }
}
