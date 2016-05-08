using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class Model : MonoBehaviour {

    //in the Sonic
    Animator animator;
    AnimatorStateInfo first_info;
    BoxCollider mcollider;
    SkinnedMeshRenderer skin1, skin2;
    Rigidbody rb;
    Vector3 original_collidersize, shieldposition = new Vector3 (-3f, 2f, 0f), original_position, faceright, faceleft, dead;
    GameObject shield;
    WaitForSeconds delay = new WaitForSeconds(1.7f), jumpdelay = new WaitForSeconds(.1f);
    [SerializeField]
    float speed, turnspeed, jumpforce;
    bool isJump = false;
    //Vector3 movedirection = Vector3.zero;

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        mcollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        skin1 = transform.FindChild("Cube/Cube_MeshPart0").GetComponent<SkinnedMeshRenderer>();
        skin2 = transform.FindChild("Cube/Cube_MeshPart1").GetComponent<SkinnedMeshRenderer>();
        original_collidersize = mcollider.size;
        original_position = transform.localPosition;
        //faceleft = new Quaternion(0f, Quaternion.Angle, 0f, 0f);
        //faceright = new Quaternion(0f, 180f, 0f, 0f);
        faceright = new Vector3(0f, 90f, 0f);
        faceleft = new Vector3(0f, 270f, 0f);
        dead = new Vector3(0.01f, 0.01f, 0.01f);
    }

    IEnumerator timer()
    {
        //two seconds delay, and then speed up to 3f
        yield return delay;
        animator.speed = 3f;
        mcollider.size = original_collidersize;
        yield break;
    }

    IEnumerator revive()
    {
        transform.localScale = dead;
        Debug.Log("???");
        //two seconds delay, and then speed up to 3f
        yield return delay;
        Debug.Log("!!!");
        transform.localScale = Vector3.one;
        yield break;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.relativeVelocity.y);
        if (collision.relativeVelocity.y > 1f)
            isJump = false;

        switch (collision.transform.name)
        {
            case "Wall":
                skin1.material.color = Color.red;
                skin2.material.color = Color.red;
                break;
            case "Spring":
                if(collision.relativeVelocity.y > 1f)
                    Jump(600f);
                break;
        }

        if(collision.transform.name == "Enemy1")
        {
            if (collision.relativeVelocity.y > 1f)
                Destroy(collision.gameObject);
            else
                GameOver();
        }
    }

    public void SuperMode()
    {
        //to slow everything
        skin1.material.color = Color.yellow;
        skin2.material.color = Color.yellow;
        animator.speed = .1f;
    }

    public void NormalMode()
    {
        animator.speed = 2f;
    }

    void Jump()
    {
        isJump = true;
        animator.SetBool("Jump", true);
        rb.AddForce(transform.up * jumpforce);
    }

    void Jump(float jumpforce1)
    {
        isJump = true;
        animator.SetBool("Jump", true);
        rb.AddForce(transform.up * jumpforce1);
    }

    public void GameOver()
    {
        Debug.Log("GG");
        transform.localPosition = original_position;
        StartCoroutine("revive");
    }

    // Update is called once per frame
    void Update ()
    {
        if (transform.localPosition.y < -20.0f)
            GameOver();

        if(animator)
        {
            //first_info = animator.GetCurrentAnimatorStateInfo(1);

            //to move
            //going forward action 

            if(Input.GetAxis("Horizontal") > 0f)
            {
                //to turn right
                transform.localRotation = Quaternion.Euler(faceright);
            }

            if (Input.GetAxis("Horizontal") < 0f)
            {
                //to turn left
                transform.localRotation = Quaternion.Euler(faceleft);
            }

            animator.SetInteger("Mode", Input.GetAxis("Horizontal")!= 0f ? 1 : 0);

            //movedirection = Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime;
            //for 3D
            //transform.Rotate(0, Input.GetAxis("Horizontal") * turnspeed * Time.deltaTime, 0);
            transform.localPosition += Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime; ;

            //to slide
            if (Input.GetKey(KeyCode.Space))
            {
                //sliding action
                animator.speed = 1.5f;
                mcollider.size = new Vector3(.5f, .5f, .5f);
                animator.SetBool("Space", true);
                StartCoroutine("timer");
            }
            else
            {
                animator.SetBool("Space", false);
            }
            

            if (Input.GetKeyDown(KeyCode.W) && !isJump)
            {
                Jump();
                //transform.localPosition += transform.up * jumpforce * speed * Time.deltaTime; ;
                //StartCoroutine("jumping");
            }
            else
            {
                animator.SetBool("Jump", false);
            }

            //to create a shield
            if (Input.GetKeyDown(KeyCode.D))
            {
                shield = (GameObject)Instantiate(Resources.Load("Shield"), transform.position + shieldposition, Quaternion.identity);
                //shield.transform.parent = transform;

                Destroy(shield, 3f);
            }

        }
    }
}
