using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("YO");
        gameObject.SetActive(false);
    }
}
