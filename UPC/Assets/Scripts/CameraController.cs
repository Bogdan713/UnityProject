using UnityEngine;

public class CameraController : MonoBehaviour {

    private Transform target;
    private float speed = 2.0f;

    private void Awake()
    {
        if (!target) target = FindObjectOfType<Character>().transform;
    }

    // Update is called once per frame
    void Update () {
        Vector3 position = target.position;
        position.z = -10.1f;
        transform.position = Vector3.Lerp(transform.position, position, speed*Time.deltaTime);
        Canvas canvas = new Canvas();
	}
}
