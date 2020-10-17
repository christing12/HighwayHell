using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    Rigidbody rb;
    public float explosionPower = 500f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb = collision.rigidbody; //gets Players rigid body
            rb.AddForce(explosionPower/1.5f, explosionPower, 0);
        }
    }
}
