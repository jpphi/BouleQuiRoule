using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
//public class Coordonnees
//{
//    public Vector3 position;
//    public Quaternion rotation;
//}

[CreateAssetMenu(menuName = "Scenario 1")]
public class ScriptableObjectTest : ScriptableObject
{
    float test;
    //    public Coordonnees[] _coord;
    public List<Vector3> position;
    public List<Quaternion> rotation;
    public Vector3 positionPlayer;

    public int taille = 8;

    public float tailleMiniX = 0.2f;
    public float tailleMiniY = 0.2f;
    public float tailleMiniZ = 0.2f;

    public int stockNouritureAuDepartArbre = 100;
    public int stockNouritureAuDepartProie = 100;
    public int stockNouritureAuDepartPredateur = 100;


    public void affichePositionRotation()
    {
        for(int i= 0; i < position.Count; i++)
        {
            Debug.Log("A l'indice i= " + i + " position= " + position[i] + " position.Count= " + position.Count);
        }
    }

    public void chargementObjet(GameObject obj, Vector3 pos, Quaternion rot)
    {
        //Debug.Log("Objet charg� en  " + pos + " rotation " + rot);
        Instantiate(obj, pos, rot);
    }


}
