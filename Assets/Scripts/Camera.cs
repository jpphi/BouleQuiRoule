using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] GameObject _player;

    Vector3 offset;
    Quaternion rot;
    void Start()
    {
        rot= Quaternion.Euler(90,0,0);
        offset= new Vector3(0,70,0);
        //rot= transform.rotation;
        transform.position = offset;
        transform.rotation = rot;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position= _player.transform.position + offset;
        
    }
}
