using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sukk_Movement : MonoBehaviour
{
    public float maxSpeed;
    public float startingSpeed;
    Rigidbody rb_movement;
    public float acceleration = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        rb_movement = GetComponent<Rigidbody>();
        InvokeRepeating("IncreaseSpeed", 3f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        rb_movement.velocity = new Vector3(0, 0, startingSpeed);
    }

    void IncreaseSpeed()
    {
        if (startingSpeed < maxSpeed)
        {
            startingSpeed *= acceleration;
        }
       // Debug.Log("SPEED: " + startingSpeed);

    }
}

