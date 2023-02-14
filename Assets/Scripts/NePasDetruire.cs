using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NePasDetruire : MonoBehaviour
{
    // Start is called before the first frame update
    public static NePasDetruire instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
