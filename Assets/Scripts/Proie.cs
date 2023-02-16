using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Proie : MonoBehaviour
{
    float _speed = 100f;
    [SerializeField]float Xmin = -10f, Xmax = 10f, Ymin = 0f, Ymax = 1f, Zmin = -10f, Zmax = 10f;
    GameObject _cetObjet;

    [SerializeField] float tictac = 1f;
    [SerializeField] int conso = 1;

    [SerializeField] private ScriptableObjectTest _soProie;


    bool joyeuAnniversaire = false;

    int stockNouriture;

    private void Start()
    {
        StartCoroutine(OhVieillir());

        _cetObjet = this.gameObject;

    }
    private void Update()
    {
        if (joyeuAnniversaire)
        {
            joyeuAnniversaire = false;
            StartCoroutine(OhVieillir());
            //Debug.Log("Joyeux Anniv! ");
        }

        //transform.position += new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
        _cetObjet.GetComponent<Rigidbody>().AddForce(Random.Range(Xmin, Xmax) * _speed * Time.fixedDeltaTime,
            Random.Range(Ymin, Ymax) * Time.fixedDeltaTime, Random.Range(Zmin,Zmax) * _speed * Time.fixedDeltaTime);

    }

    private IEnumerator OhVieillir()
    {
        yield return new WaitForSeconds(tictac);

        //stockNouriture -= conso;
        joyeuAnniversaire = true;

        //if(stockNouriture<=0)
        //{
        //    Debug.Log("Nouriture épuisée ! Objet détruit !" + stockNouriture);
        //    Destroy(gameObject);
        //}

        Vector3 scale= _soProie.decroissanceProie * transform.localScale;

        if ((scale.x < _soProie.tailleMiniX) || (scale.y < _soProie.tailleMiniY) || (scale.z < _soProie.tailleMiniZ))
        {
            //Debug.Log("Proie à détruire = " + transform.position);
            //_soProie.position.Remove(transform.position);
            //_soProie.rotation.Remove(transform.rotation);
            //Debug.Log("Longueur list position = " + _soArbre.position.Count);

            Destroy(gameObject);
        }
        else
        {
            transform.localScale = scale;
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        //Vector3 decroissance = new Vector3(transform.localScale.x * 0.9f, transform.localScale.y * 0.9f, transform.localScale.z * 0.9f);
        //Vector3  = new Vector3(transform.localScale.x * 1.1f, transform.localScale.y * 1.1f, transform.localScale.z * 1.1f);
        Vector3 croissance = _soProie.croissanceProie * transform.localScale;

        Debug.Log("On collision dans Proie " + collision.gameObject.tag);

        //Debug.Log("On collision enter tag (GameObject arbre): " + collision.gameObject.tag + " Scale: " + transform.localScale);
        if (collision.gameObject.CompareTag("Arbre"))
        {
            Debug.Log("Arbre touché position = " + transform.position + " Taille de la proie sera ajusté à " + croissance);
            Debug.Log("scale mini X = " + _soProie.tailleMiniXProie + " scale mini Z " + _soProie.tailleMiniZProie);
            transform.localScale = croissance;
        }

    }


}
