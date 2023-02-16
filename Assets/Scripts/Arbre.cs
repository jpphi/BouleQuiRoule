using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbre : MonoBehaviour
{
    [SerializeField] private ScriptableObjectTest _soArbre;

    private int stockNouriture;
  
    void Start()
    {
        // On va g�rer le vieillissement des arbres avec cette variable
        //stockNouriture = _soArbre.stockNouritureAuDepartArbre;
    
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 scale = new Vector3(transform.localScale.x * 0.9f, transform.localScale.y * 0.9f, transform.localScale.z * 0.9f);

        //Debug.Log("On collision enter tag (GameObject arbre): " + collision.gameObject.tag + " Scale: " + transform.localScale);
        if (collision.gameObject.CompareTag("Proie")) // || collision.gameObject.CompareTag("Predateur")
        {
            //Debug.Log("Arbre touch� position = " + transform.position + " sera ajust� � " + scale + 
            //    " scale mini X = " + _soArbre.tailleMiniX + " scale mini Z " + _soArbre.tailleMiniZ);
            transform.localScale = scale;
        }

        if( (scale.x < _soArbre.tailleMiniX) || (scale.y < _soArbre.tailleMiniY) || (scale.z < _soArbre.tailleMiniZ))
        {
            //Debug.Log("Arbre � d�truire = " + transform.position);
            _soArbre.position.Remove(transform.position);
            _soArbre.rotation.Remove(transform.rotation);
            //Debug.Log("Longueur list position = " + _soArbre.position.Count);

            Destroy(gameObject);
        }
    }

    
}
