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
    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
        rect = border.GetComponent<RectTransform>().rect;
        positionTarget = new Vector3();
    }

    void Update()
    {

        if (target != null)
        {
            positionTarget = target.position;
            positionTarget.z = -10f;
        }
        //range control
        if (positionTarget.x < rect.xMin)
        {
            positionTarget.x = rect.xMin;
        }
        if (positionTarget.y < rect.yMin)
        {
            positionTarget.y = rect.yMin;
        }

        if (positionTarget.x > rect.xMax)
        {
            positionTarget.x = rect.xMax;
        }
        if (positionTarget.y > rect.yMax)
        {
            positionTarget.y = rect.yMax;
        }

        transform.position = Vector3.Lerp(transform.position, positionTarget, speed * Time.deltaTime);
    }
}
