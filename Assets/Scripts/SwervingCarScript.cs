using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwervingCarScript : MonoBehaviour
{
    //Rigidbody Reference
    public Rigidbody rb;

    //Speed
    public float movementSpeed;
    public float maxMoveSpeed;

    //Shimmy
    private bool canShimmy = true;

    public float timeToShimmyDefault;
    private float timeToShimmy;

    private bool moveRight = true;

    //ShimmyCD
    public float shimmyCD;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timeToShimmy = timeToShimmyDefault;
    }


    private void FixedUpdate()
    {
        FuckingMoveForwardYes();
        RandomShimmy();
    }

    void FuckingMoveForwardYes()
    {
        if(rb.velocity.z < maxMoveSpeed)
        {
            rb.AddForce(transform.forward * movementSpeed * Time.deltaTime);
        }
    }

    void RandomShimmy()
    {
        if (canShimmy == true && moveRight == true)
        {
            if (timeToShimmy > 0)
            {
                rb.velocity = new Vector3(10, rb.velocity.y, rb.velocity.z);
                timeToShimmy -= Time.deltaTime;
            }
            else if (timeToShimmy < 0)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                canShimmy = false;
                shimmyCD = Random.Range(2, 4);
                moveRight = false;
            }
        }
        if (canShimmy == true && moveRight == false)
        {
            if (timeToShimmy > 0)
            {
                rb.velocity = new Vector3(-10, rb.velocity.y, rb.velocity.z);
                timeToShimmy -= Time.deltaTime;
            }
            else if (timeToShimmy < 0)
            {
                rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
                canShimmy = false;
                shimmyCD = Random.Range(2, 4);
                moveRight = true;
            }
        }
        if (canShimmy == false)
        {
            ShimmyCD();
        }
    }

    void ShimmyCD()
    {
        shimmyCD -= Time.deltaTime;
        if (shimmyCD <= 0)
        {
            canShimmy = true;
            timeToShimmy = timeToShimmyDefault;
        }
    }
}
