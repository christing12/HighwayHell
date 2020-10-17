using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    public Vector3 movement; // takes the direction vector the enemy will move at.

    Rigidbody rb;
    float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + (movement * speed * Time.deltaTime));
    }
}
