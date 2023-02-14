using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbre : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("On collision enter tag (GameObject arbre): " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Proie"))
        {
            Debug.Log("Arbre touché position = " + transform.position);
            transform.localScale = new Vector3(transform.localScale.x * 0.9f,
                transform.localScale.y * 0.9f, transform.localScale.z * 0.9f);
        }

    }
}
