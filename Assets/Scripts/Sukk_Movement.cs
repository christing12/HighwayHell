using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sukk_Movement : MonoBehaviour
{
    public float speed;
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
        rb_movement.velocity = new Vector3(0, 0, speed);
    }

    void IncreaseSpeed()
    {
        speed *= acceleration;
        Debug.Log("SPEED: " + speed);

    }
}

