using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    Rigidbody rb;
    public GameObject vfx;
    public float explosionPower = 500f;

    void start()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb = collision.rigidbody; //gets Players rigid body
            rb.AddRelativeForce(0, explosionPower, explosionPower / 10);
            Instantiate(vfx, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
          //  Destroy(this.gameObject);
        }
    }
}
