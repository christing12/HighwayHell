using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    Rigidbody rb_movement;  
    // Start is called before the first frame update
    void Start()
    {
        rb_movement = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb_movement.velocity = new Vector3(speed,0,0);
    }
}

