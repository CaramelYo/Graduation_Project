using UnityEngine;
using System.Collections;

public class Model : MonoBehaviour {

    Animator animator;
    AnimatorStateInfo info;
    BoxCollider mcollider;
    SkinnedMeshRenderer s1, s2;
    float speed = 2f, turnspeed = 60f;
    Vector3 origin_collidersize, shieldposition = new Vector3 (-3f, 2f, 0f);
    WaitForSeconds twoseconds = new WaitForSeconds(2f);
    GameObject shield;
    //Vector3 movedirection = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        mcollider = GetComponent<BoxCollider>();
        s1 = transform.FindChild("Cube/Cube_MeshPart0").GetComponent<SkinnedMeshRenderer>();
        s2 = transform.FindChild("Cube/Cube_MeshPart1").GetComponent<SkinnedMeshRenderer>();
        origin_collidersize = mcollider.size;
    }

    IEnumerator timer()
    {
        yield return twoseconds;
        animator.speed = 3f;
        mcollider.size = origin_collidersize;
        yield break;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.name);
        if(collision.transform.name == "Wall")
        {
            s1.material.color = Color.red;
            s2.material.color = Color.red;
        }
    }

    public void slow()
    {
        s1.material.color = Color.yellow;
        s2.material.color = Color.yellow;
        animator.speed = .1f;
    }

    public void normal()
    {
        animator.speed = 2f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(animator)
        {
            animator.SetInteger("Mode", Input.GetKey(KeyCode.UpArrow) ? 1 : 0);

            //movedirection = Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
            transform.Rotate(0, Input.GetAxis("Horizontal") * turnspeed * Time.deltaTime, 0);
            transform.localPosition += Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime; ;
            
            //info = animator.GetCurrentAnimatorStateInfo(0);

            if(Input.GetKeyDown(KeyCode.D))
            {
                shield = (GameObject)Instantiate(Resources.Load("Shield"), transform.position + shieldposition, Quaternion.identity);
                //shield.transform.parent = transform;

                Destroy(shield, 3f);
            }

            
            if (Input.GetKey(KeyCode.Space))
            {
                animator.speed = 1.5f;
                mcollider.size = new Vector3(.5f, .5f, .5f);
                animator.SetBool("Space", true);
                StartCoroutine("timer");
            }
            else
            {
                animator.SetBool("Space", false);
            }
        }
    }
}
