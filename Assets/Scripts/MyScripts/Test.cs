using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour
{
    bool changing = false;
    Color origin;

    public bool Changing
    {
        get
        {
            return changing;
        }
        set
        {
            changing = value;
        }
    }
    
    public Color Change()
    {
        origin = GetComponent<MeshRenderer>().material.color;
        changing = true;
        StartCoroutine("cd");
        return new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    IEnumerator cd()
    {
        yield return new WaitForSeconds(5f);
        changing = false;
        GetComponent<MeshRenderer>().material.color = origin;
        yield break;
    }
}

public class Test : MonoBehaviour {

    float speed = 5f;
    GameObject newbullet;
    Transform followcamera, bullet;
    MeshRenderer plane;

	// Use this for initialization
	void Start ()
    {
        followcamera = GameObject.Find("Main Camera").transform;
        bullet = Resources.Load<Transform>("Bullet");
        plane = GameObject.Find("Plane").GetComponent<MeshRenderer>();
        plane.material.color = Color.black;
        if(!GetComponent<ChangeColor>())
            gameObject.AddComponent<ChangeColor>();

        return;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.localPosition += (Input.GetKey(KeyCode.UpArrow)) ? speed * Time.deltaTime * Vector3.up : Vector3.zero;
        transform.localPosition += (Input.GetKey(KeyCode.DownArrow)) ? speed * Time.deltaTime * Vector3.down : Vector3.zero;
        transform.localPosition += (Input.GetKey(KeyCode.LeftArrow)) ? speed * Time.deltaTime * Vector3.left : Vector3.zero;
        transform.localPosition += (Input.GetKey(KeyCode.RightArrow)) ? speed * Time.deltaTime * Vector3.right : Vector3.zero;

        followcamera.localPosition = transform.localPosition;
        followcamera.localPosition += 10 * Vector3.back;

        if(Input.GetKey(KeyCode.Space))
        {
            //shooting
            newbullet = Instantiate(bullet).gameObject;
            newbullet.transform.parent = transform;
            newbullet.transform.localPosition = Vector3.zero;
            Destroy(newbullet, 2);
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(!GetComponent<ChangeColor>().Changing)
            {
                GetComponent<MeshRenderer>().material.color = GetComponent<ChangeColor>().Change();
            }
        }
    }
}
