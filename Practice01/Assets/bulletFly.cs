using UnityEngine;
using System.Collections;

public class bulletFly : MonoBehaviour {
    public float speed=1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += speed * Time.deltaTime * Vector3.back;
	}
}
