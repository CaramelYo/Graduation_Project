using UnityEngine;
using System.Collections;

public class camFollow : MonoBehaviour {
    public GameObject target;
    public float damping = 1;
    Vector3 distance;

    void Start()
    {
        distance = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + distance;
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * damping);
        transform.position = position;

        transform.LookAt(target.transform.position);
    }
}
