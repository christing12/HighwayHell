using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingCar : MonoBehaviour
{
    public Rigidbody rb; //Object to rotate continuously
    public float speed; //Speed of rotation

    // Start is called before the first frame update
    void Start()
    {
        rb.angularVelocity = new Vector3(0, speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
