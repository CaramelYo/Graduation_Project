using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {
    public float speed;
    public GameObject bullet;
	void Update () {
        if (Input.GetKey(KeyCode.W)){
            transform.position += speed * Vector3.forward * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += speed * Vector3.left * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += speed * Vector3.back * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += speed * Vector3.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            if (gameObject.GetComponent<changeColor>() == null)
            {
                gameObject.AddComponent<changeColor>();
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = transform.position;
            Destroy(newBullet, 4);
        }
    }
        
}
