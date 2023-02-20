//using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
//using UnityEngine.UIElements;
//using static UnityEditor.PlayerSettings;


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
    //public int taille = 8;

    // Arbres
    public List<CaracteristiqueArbre> caracteristiqueArbre; //= new List<CaracteristiqueArbre>();
    public int maxArbre = 100;
    public float tempsDePousse = 4f;
    public int NbArbre;

    //public int stockNouritureAuDepartArbre = 100;
    public float tailleMiniXArbre = 0.2f;
    public float tailleMiniYArbre = 0.2f;
    public float tailleMiniZArbre = 0.2f;
    public float tailleMaxiArbre = 5f;

    public float croissanceArbre = 1.05f;

    // Proies
    public List<CaracteristiqueProie> caracteristiqueProie;// = new List<CaracteristiqueProie>();
    public int NbProie;
    public float decroissanceProie = 0.9f;
    public float croissanceProie = 1.15f;
    public int stockNouritureAuDepartProie = 10;

    public float tailleMiniXProie = 0.2f;
    public float tailleMiniYProie = 0.2f;
    public float tailleMiniZProie = 0.2f;
    public float tailleMaxiProie = 2f;

    public int maxProie = 200;
    //public int nombreDeProie = 0;
    //public int IndiceProix = 0;

    // Prédateur et Player
    public int stockNouritureAuDepartPredateur = 100;
    public Vector3 positionPlayer;

    public void creerObjet(GameObject obj, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        GameObject objcree;

        //Debug.Log("Créée l'objet " + obj.name + " position " + obj.transform.position + " scale " + obj.transform.localScale);


        if (obj.CompareTag("Arbre") && (NbArbre < maxArbre))
        {
            NbArbre++;

            objcree = Instantiate(obj, pos, rot);
            objcree.transform.localScale = scale;
        }
        else if (obj.CompareTag("Proie") && (NbProie < maxProie))
        {
            NbProie++;

            objcree = Instantiate(obj, pos, rot);
            objcree.transform.localScale = scale;
        }
 
    }
    public void detruitObjet(GameObject obj)
    {
        //Debug.Log("Détruit Objet " + obj.name + " position " + obj.transform.position + " scale " + obj.transform.localScale);

        if (obj.CompareTag("Arbre"))
        {
            NbArbre--;
        }
        else if (obj.CompareTag("Proie") && (NbProie < maxProie))
        {
            NbProie--;
        }

        Destroy(obj);

    }

    //[SerializeField] Proie _soproie;
    //public void affichePositionRotation()
    //{
    //    for(int i= 0; i < position.Count; i++)
    //    {
    //        Debug.Log("A l'indice i= " + i + " position= " + position[i] + " position.Count= " + position.Count);
    //    }
    //}

    //public void detruitObjet(GameObject obj, int numero)
    //{
    //    //Debug.Log("Objet chargé en  " + pos + " rotation " + rot);



    //    if (obj.CompareTag("Proie"))
    //    {
    //        //numeroProie.Remove(numero);
    //    }

    //    Destroy(obj);


    //}

    //public void retabliForet(GameObject arbre)
    //{
    //    //int nbarbre = 0;

    //    //Debug.Log("On rentre dans retabliForet caracteristiqueArbre.Count= " + caracteristiqueArbre.Count);

    //    for (int i= 0; (i< caracteristiqueArbre.Count) && (i < maxArbre); i++)
    //    {
    //        //chargementObjet(arbre, caracteristiqueArbre[i].position, caracteristiqueArbre[i].rotation);
    //        Instantiate(arbre, caracteristiqueArbre[i].position, caracteristiqueArbre[i].rotation);

    //    }

    //    //Debug.Log("retabliForest : arbre= " + nbarbre + " caracteristiqueArbre.Count= " + caracteristiqueArbre.Count);
    ////}

    //public void coupeArbre(GameObject arbre)
    //{
    //    Debug.Log("Couper un arbre à la position " + arbre.transform.position);

    //    for(int i= 0; i< caracteristiqueArbre.Count; i++)
    //    {
    //        if (caracteristiqueArbre[i].position == arbre.transform.position)
    //        {
    //            Debug.Log("arbre coupé !");
    //            caracteristiqueArbre.RemoveAt(i);
    //            break;
    //        }
    //    }
    //}

    //public void uneProieMeurt()
    //{
    //    nombreDeProie--;
    //}

    //public void lancerLesProies(GameObject proie)
    //{
    //    //  caracteristiqueProie.Count
    //    for (int i = 0; (i < nombreDeProie) && (i < maxProie); i++)
    //    {
    //        Instantiate(proie, caracteristiqueProie[i].position, caracteristiqueProie[i].rotation);
    //    }

    //}


}
