using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ResourceCardTag"))
        {
            Debug.Log("Trigger entered by object tagged 'ResourceCardTag'");
        }
    }
}

