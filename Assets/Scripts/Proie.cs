using System.Collections;
using UnityEngine;

public class Proie : MonoBehaviour
{
    float _speed = 100f;
    [SerializeField] float Xmin = -10f, Xmax = 10f, Ymin = 0f, Ymax = 1f, Zmin = -10f, Zmax = 10f;
    [SerializeField] GameObject _arbre;

    [SerializeField] float tictac = 3f;
    //[SerializeField] int conso = 1;

    [SerializeField] private ScriptableObjectTest _soProie;

    bool joyeuAnniversaire = false;

    public int IndiceProix;

    private void Start()
    {
        StartCoroutine(OhVieillir());
    }
    private void Update()
    {
        if (joyeuAnniversaire)
        {
            joyeuAnniversaire = false;
            StartCoroutine(OhVieillir());
            //Debug.Log("Joyeux Anniv! ");
        }

        if(transform.position.y <= -2)
        {
            transform.position = new Vector3(0, 5, 0);
        }

        this.GetComponent<Rigidbody>().AddForce(Random.Range(Xmin, Xmax) * _speed * Time.fixedDeltaTime,
            Random.Range(Ymin, Ymax) * Time.fixedDeltaTime, Random.Range(Zmin,Zmax) * _speed * Time.fixedDeltaTime);
    }

    private IEnumerator OhVieillir()
    {
        //Debug.Log("Vieillir lancer : attente ");
        yield return new WaitForSeconds(tictac);

        //Debug.Log("Vieillir tictac atteint : " + tictac);

        joyeuAnniversaire = true;

        Vector3 scale= _soProie.decroissanceProie * transform.localScale;

        if ((scale.x < _soProie.tailleMiniXProie) || (scale.y < _soProie.tailleMiniYProie) || (scale.z < _soProie.tailleMiniZProie))
        { // La proie est trop faible et meurt, un arbre prend sa place
            //Debug.Log("Proie à détruire = " + transform.position);

            _soProie.creerObjet(_arbre, transform.position, transform.rotation, new Vector3(1, 1, 1));

            _soProie.detruitObjet(gameObject);
        }
        else
        {
            transform.localScale = scale;
            //changeMasse(scale.x);
        }

    }


    private void OnCollisionEnter(Collision collision)
    {

        //Debug.Log("On collision dans Proie. Collision avec " + collision.gameObject.tag + " Scale: " + transform.localScale);
        if (collision.gameObject.CompareTag("Arbre"))
        {
            Vector3 croissance = _soProie.croissanceProie * transform.localScale;

            if(croissance.x > _soProie.tailleMaxiProie)
            {
                transform.localScale = new Vector3(1, 1, 1);
                _soProie.creerObjet(gameObject, transform.position, transform.rotation, new Vector3(1,1,1));
            }
            else
            {
                //Debug.Log("Arbre touché position = " + transform.position + " Taille de la proie sera ajusté à " + croissance);
                //Debug.Log("scale mini X = " + _soProie.tailleMiniXProie + " scale mini Z " + _soProie.tailleMiniZProie);
                transform.localScale = croissance;
                //changeMasse(croissance.x);

            }

        }

    }

    private void changeMasse(float echelle)
    {
        float masse = transform.GetComponent<Rigidbody>().mass;
        masse*= echelle;

        Debug.Log("Changement de la masse= " + masse + " Echelle= " + echelle);
        transform.GetComponent<Rigidbody>().mass = masse;
    }


}
