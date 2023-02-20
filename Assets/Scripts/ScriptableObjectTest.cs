using System.Collections.Generic;
using UnityEngine;

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
    private int maxArbre = 100;
    public float tempsDePousse = 4f;
    private int NbArbre;

    //public int stockNouritureAuDepartArbre = 100;
    public float tailleMiniXArbre = 0.2f;
    public float tailleMiniYArbre = 0.2f;
    public float tailleMiniZArbre = 0.2f;
    public float tailleMaxiArbre = 5f;

    public float croissanceArbre = 1.05f;

    // Proies
    public List<CaracteristiqueProie> caracteristiqueProie;// = new List<CaracteristiqueProie>();
    private int NbProie;
    public float decroissanceProie = 0.9f;
    public float croissanceProie = 1.15f;
    //public int stockNouritureAuDepartProie = 10;

    public float tailleMiniXProie = 0.2f;
    public float tailleMiniYProie = 0.2f;
    public float tailleMiniZProie = 0.2f;
    public float tailleMaxiProie = 2f;

    private int maxProie = 200;

    // Prédateur et Player
    //public int stockNouritureAuDepartPredateur = 100;
    public Vector3 positionPlayer;

    public void creerObjet(GameObject obj, Vector3 pos, Quaternion rot, Vector3 scale)
    {
        GameObject objcree;

        Debug.Log("Créée l'objet " + obj.name + " position " + obj.transform.position + " scale " + obj.transform.localScale +
            " Tag= " + obj.tag + " NbArbre= " + NbArbre + " NbProie= " + NbProie);

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

    public void miseAZero()
    {
        NbProie= NbArbre= 0;
    }
}
