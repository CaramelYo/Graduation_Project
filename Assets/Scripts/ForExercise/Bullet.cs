using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    float shootspeed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	//test
	// Update is called once per frame
	void Update () {
        transform.localPosition += shootspeed * Time.deltaTime * Vector3.right;
	}
}
