using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arbre : MonoBehaviour
{
    [SerializeField] private ScriptableObjectTest _soArbre;
    [SerializeField] float tempsDePousse = 4f;

    bool joyeuAnniversaire = false;

    void Start()
    {
        // On va g�rer le vieillissement des arbres avec cette variable
        StartCoroutine(OnPousse());

        //Debug.Log("L'arbre numero ");
    }

    // Update is called once per frame
    void Update()
    {
        if (joyeuAnniversaire)
        {
            joyeuAnniversaire = false;
            StartCoroutine(OnPousse());
            //Debug.Log("Joyeux Anniv! ");
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 scale = new Vector3(transform.localScale.x * 0.9f, transform.localScale.y * 0.9f, transform.localScale.z * 0.9f);

  
        Debug.Log("On collision enter tag (GameObject arbre): " + collision.gameObject.tag + " Scale: " + transform.localScale);
        if (collision.gameObject.CompareTag("Proie")) // || collision.gameObject.CompareTag("Predateur")
        {
            //Debug.Log("Arbre touch� position = " + transform.position + " sera ajust� � " + scale + 
            //    " scale mini X = " + _soArbre.tailleMiniX + " scale mini Z " + _soArbre.tailleMiniZ);
            transform.localScale = scale;
        }

        if( (scale.x < _soArbre.tailleMiniXArbre) || (scale.y < _soArbre.tailleMiniYArbre) || (scale.z < _soArbre.tailleMiniZArbre))
        {
            //Debug.Log("Arbre � d�truire = " + transform.position);

            _soArbre.coupeArbre(gameObject);
        }
    }


    private IEnumerator OnPousse()
    {
        yield return new WaitForSeconds(tempsDePousse);

        joyeuAnniversaire = true;

        Vector3 scale = _soArbre.croissanceArbre * transform.localScale;

        scale = (scale.x > _soArbre.tailleMaxiArbre) ?
              new Vector3(_soArbre.tailleMaxiArbre, _soArbre.tailleMaxiArbre, _soArbre.tailleMaxiArbre) : scale;

        transform.localScale = scale;

    }
}
