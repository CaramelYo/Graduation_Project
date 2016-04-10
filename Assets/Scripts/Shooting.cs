using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    float speed = 5f, brakedistance = 7f, brakescale = .2f;
    bool shoot = false, braked = false;
    RaycastHit rayhit;
    Transform mcamera, test;
    Vector3 origin_position;
    Quaternion origin_rotation;

	// Use this for initialization
	void Start ()
    {
        test = GameObject.Find("test").transform;
        mcamera = test.transform.Find("Camera");
        origin_position = mcamera.localPosition;
        origin_rotation = mcamera.localRotation;

        return;
	}

    public void Bye()
    {
        mcamera.localPosition = origin_position;
        mcamera.localRotation = origin_rotation;
        test.SendMessage("normal");
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.A))
        {
            shoot = shoot ? false : true;
        }

        if(shoot)
            transform.localPosition += speed * Time.deltaTime * Vector3.right;

        if (braked)
        {
            if(!Physics.Raycast(transform.position, transform.right, out rayhit))
            {
                speed /= brakescale;
                braked = false;
                Destroy(gameObject, 3);
                mcamera.localPosition = origin_position;
                mcamera.localRotation = origin_rotation;
                test.SendMessage("normal");
            }
            return;
        }


        if(Physics.Raycast(transform.position, transform.right, out rayhit))
        {
            if(Vector3.Distance(transform.position, rayhit.collider.transform.position) <= brakedistance && rayhit.collider.transform.name == "test")
            {
                speed *= brakescale;
                mcamera.localPosition = new Vector3(5.29f, 0.07f, 0.44f);
                mcamera.localRotation = new Quaternion(0.06846185f, 0.8426361f, -0.03833173f, -0.5327364f);
                test.SendMessage("slow");
                braked = true;
            }
        }
    }
}
