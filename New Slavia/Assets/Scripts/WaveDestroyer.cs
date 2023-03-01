using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveDestroyer : MonoBehaviour
{
    private GameObject enem;
    void Start()
    {
        
    }
    void Update()
    {
        enem = GameObject.FindGameObjectWithTag("Enemy");
        if (enem == null)
        {
            Destroy(gameObject);
        }
    }
}
