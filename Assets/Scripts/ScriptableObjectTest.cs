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
    //public int indiceArbre;
    //public Vector3[] positionArbre;
    //public Quaternion[] rotationArbre;

    public int taille = 8;
    //public Quaternion rotation;

    
    public void affichePositionRotation()
    {
        for(int i= 0; i < position.Count; i++)
        {
            Debug.Log("A l'indice i= " + i + " position= " + position[i] + " position.Count= " + position.Count);
        }
    }

    public void chargementObjet(GameObject obj, Vector3 pos, Quaternion rot)
    {
        Debug.Log("Objet chargé en  " + pos + " rotation " + rot);
        Instantiate(obj, pos, rot);
    }


}
