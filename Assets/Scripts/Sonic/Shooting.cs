using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    //in the Wall
    RaycastHit rayhit;
    Transform mcamera, Sonic;
    Vector3 camera_original_position;
    Quaternion camera_original_rotation;
    [SerializeField]
    float speed = 5f, brakedistance = 7f, brakescale = .2f;
    bool shoot = false, braked = false;

    // Use this for initialization
    void Start ()
    {
        Sonic = GameObject.Find("Sonic").transform;
        mcamera = Sonic.transform.Find("Camera");
        camera_original_position = mcamera.localPosition;
        camera_original_rotation = mcamera.localRotation;
	}

    public void DestroyWall()
    {
        mcamera.localPosition = camera_original_position;
        mcamera.localRotation = camera_original_rotation;
        Sonic.SendMessage("NormalMode");
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.A))
        {
            //to start the Wall
            shoot = shoot ? false : true;
        }

        //to move Wall
        if(shoot)
            transform.localPosition += speed * Time.deltaTime * Vector3.right;

        if (braked)
        {
            if(!Physics.Raycast(transform.position, transform.right, out rayhit))
            {
                speed /= brakescale;
                braked = false;
                Destroy(gameObject, 3);
                mcamera.localPosition = camera_original_position;
                mcamera.localRotation = camera_original_rotation;
                Sonic.SendMessage("NormalMode");
            }
            return;
        }


        if(Physics.Raycast(transform.position, transform.right, out rayhit))
        {
            if(Vector3.Distance(transform.position, rayhit.collider.transform.position) <= brakedistance && rayhit.collider.transform.name == "Sonic")
            {
                speed *= brakescale;
                mcamera.localPosition = new Vector3(5.29f, 0.07f, 0.44f);
                mcamera.localRotation = new Quaternion(0.06846185f, 0.8426361f, -0.03833173f, -0.5327364f);
                Sonic.SendMessage("SuperMode");
                braked = true;
            }
        }
    }
}
