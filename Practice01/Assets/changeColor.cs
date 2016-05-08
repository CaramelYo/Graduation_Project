using UnityEngine;
using System.Collections;

public class changeColor : MonoBehaviour
{
    private float counter;
    private MeshRenderer meshRenderer;
    Color origin;
    // Use this for initialization
    void Start()
    {
        counter = Random.Range(3, 5);
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            origin = meshRenderer.material.color;
            meshRenderer.material.color = Random.ColorHSV();
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;
        if (counter < 0 && meshRenderer!=null)
        {
            meshRenderer.material.color = origin;
            Destroy(this);
        }

    }
}