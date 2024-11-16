using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player") && collider.gameObject.activeSelf)
        {
            Shine();
        }
    }
    
    public void Shine()
    {
        this.gameObject.GetComponent<Animator>().SetTrigger("Interacted");
    }
}
