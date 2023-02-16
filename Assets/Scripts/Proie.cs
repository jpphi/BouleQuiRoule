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
    //[SerializeField] int conso = 1;

    [SerializeField] private ScriptableObjectTest _soProie;


    bool joyeuAnniversaire = false;

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

        joyeuAnniversaire = true;

        Vector3 scale= _soProie.decroissanceProie * transform.localScale;

        if ((scale.x < _soProie.tailleMiniXProie) || (scale.y < _soProie.tailleMiniYProie) || (scale.z < _soProie.tailleMiniZProie))
        {
            //Debug.Log("Proie à détruire = " + transform.position);

            Destroy(gameObject);
        }
        else
        {
            transform.localScale = scale;
        }

    }


    private void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("On collision dans Proie. Collision avec " + collision.gameObject.tag + " Scale: " + transform.localScale);
        if (collision.gameObject.CompareTag("Arbre"))
        {
            Vector3 croissance = _soProie.croissanceProie * transform.localScale;

            //Debug.Log("Arbre touché position = " + transform.position + " Taille de la proie sera ajusté à " + croissance);
            //Debug.Log("scale mini X = " + _soProie.tailleMiniXProie + " scale mini Z " + _soProie.tailleMiniZProie);
            transform.localScale = croissance;
        }

    }


}
