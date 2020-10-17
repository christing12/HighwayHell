using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //public Vector3 movement; // takes the direction vector the enemy will move at.
    public float movementSpeed;
    public float maxMoveSpeed;
    Rigidbody rb;
    float speed = 1.0f;
    public GameObject vfx;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        FuckingMoveForwardYes();
    }
    void FuckingMoveForwardYes()
    {
        if (rb.velocity.z < maxMoveSpeed)
        {
            rb.AddForce(transform.forward * movementSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Instantiate(vfx, transform.position, Quaternion.identity);
        }
    }
}
