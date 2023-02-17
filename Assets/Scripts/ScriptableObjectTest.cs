using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;


//[System.Serializable]
//public class Coordonnees
//{
//    public Vector3 position;
//    public Quaternion rotation;
//}

[System.Serializable]
public class CaracteristiqueProie
{
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;
    public int numeroProie;

}

[System.Serializable]
public class CaracteristiqueArbre
{
    public Vector3 position;
    public Vector3 scale;
    public Quaternion rotation;
    public int numero;

}


[CreateAssetMenu(menuName = "Scenario 1")]

public class ScriptableObjectTest : ScriptableObject
{
    //float test;
    //    public Coordonnees[] _coord;

    public List<CaracteristiqueProie> caracteristiqueProie;// = new List<CaracteristiqueProie>();
    public List<CaracteristiqueArbre> caracteristiqueArbre; //= new List<CaracteristiqueArbre>();

    public Vector3 positionPlayer;

    public int taille = 8;

    public float tailleMiniXArbre = 0.2f;
    public float tailleMiniYArbre = 0.2f;
    public float tailleMiniZArbre = 0.2f;
    public float tailleMaxiArbre = 5f;

    public float croissanceArbre = 1.05f;

    public float tailleMiniXProie = 0.2f;
    public float tailleMiniYProie = 0.2f;
    public float tailleMiniZProie = 0.2f;
    public float tailleMaxiProie = 2f;
    public int maxProie = 200;

    public float decroissanceProie = 0.9f;
    public float croissanceProie =1.15f;

    public int stockNouritureAuDepartArbre = 100;
    public int stockNouritureAuDepartProie = 10;
    public int stockNouritureAuDepartPredateur = 100;

    public int maxArbre = 100;
    private int indexArbre = 0;


    //public void affichePositionRotation()
    //{
    //    for(int i= 0; i < position.Count; i++)
    //    {
    //        Debug.Log("A l'indice i= " + i + " position= " + position[i] + " position.Count= " + position.Count);
    //    }
    //}

    public void chargementObjet(GameObject obj, Vector3 pos, Quaternion rot)
    {
        //Debug.Log("Objet chargé en  " + pos + " rotation " + rot);
        Instantiate(obj, pos, rot);
        if (obj.CompareTag("Proie"))
        {
            caracteristiqueProie.Add(new CaracteristiqueProie {
                position = pos,
                rotation = rot,
            });
        }
        if (obj.CompareTag("Arbre"))
        {
            //Debug.Log("On créer un arbre à la position pos= " + pos);
            caracteristiqueArbre.Add(new CaracteristiqueArbre { 
            position= pos,
            rotation= rot,
            numero= indexArbre++
            });

        }

    }
    public void detruitObjet(GameObject obj, int numero)
    {
        //Debug.Log("Objet chargé en  " + pos + " rotation " + rot);



        if (obj.CompareTag("Proie"))
        {
            //numeroProie.Remove(numero);
        }

        Destroy(obj);


    }

    public void retabliForet(GameObject arbre)
    {
        //int nbarbre = 0;

        //Debug.Log("On rentre dans retabliForet caracteristiqueArbre.Count= " + caracteristiqueArbre.Count);

        for (int i= 0; (i< caracteristiqueArbre.Count) && (i < maxArbre); i++)
        {
            //chargementObjet(arbre, caracteristiqueArbre[i].position, caracteristiqueArbre[i].rotation);
            Instantiate(arbre, caracteristiqueArbre[i].position, caracteristiqueArbre[i].rotation);

        }

        //Debug.Log("retabliForest : arbre= " + nbarbre + " caracteristiqueArbre.Count= " + caracteristiqueArbre.Count);
    }

    public void coupeArbre(GameObject arbre)
    {
        Debug.Log("Couper un arbre à la position " + arbre.transform.position);

        for(int i= 0; i< caracteristiqueArbre.Count; i++)
        {
            if (caracteristiqueArbre[i].position == arbre.transform.position)
            {
                Debug.Log("arbre coupé !");
                caracteristiqueArbre.RemoveAt(i);
                break;
            }
        }

    }

    public void lancerLesProies(GameObject proie)
    {
        for (int i = 0; (i < caracteristiqueProie.Count) && (i < maxProie); i++)
        {
            Instantiate(proie, caracteristiqueProie[i].position, caracteristiqueProie[i].rotation);
        }

    }


}
