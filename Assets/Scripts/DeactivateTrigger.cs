using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (ObjectPooler.SharedInstance.GetPooledObjectByName(other.name) != null && other.gameObject.tag != "ground")
        {
          //  Debug.Log(other.gameObject.name);
            other.gameObject.SetActive(false);
        }
    }
}
