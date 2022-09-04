using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAllOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Destroy(GameObject.Find("Player"));
        Destroy(GameObject.Find("UI Canvas"));
        Destroy(GameObject.Find("Respawn Controller"));
        Destroy(GameObject.Find("Player"));
    }

}
