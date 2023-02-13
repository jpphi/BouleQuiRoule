using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proix : MonoBehaviour
{
    float _speed = 100f;
    [SerializeField]float Xmin = -10f, Xmax = 10f, Ymin = 0f, Ymax = 1f, Zmin = -10f, Zmax = 10f;
    GameObject _cetObjet;
    private void Update()
    {
        //transform.position += new Vector3(Random.Range(-0.1f, 0.1f), 0, Random.Range(-0.1f, 0.1f));
        _cetObjet = this.gameObject;
        _cetObjet.GetComponent<Rigidbody>().AddForce(Random.Range(Xmin, Xmax) * _speed * Time.fixedDeltaTime,
            Random.Range(Ymin, Ymax) * Time.fixedDeltaTime, Random.Range(Zmin,Zmax) * _speed * Time.fixedDeltaTime);

    }
}
