using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        collision.gameObject.SendMessage("Bye");
    }

    // Update is called once per frame
    void Update () {
	
	}
}
