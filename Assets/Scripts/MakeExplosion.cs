using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeExplosion : MonoBehaviour
{
    public GameObject Explosion;
    public float leftBound;
    public float rightBound;
    Rigidbody rb;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(rb.position.x > rightBound || rb.position.x < leftBound)
        {
            GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);

            Destroy(this.gameObject, 0.01f);

            Destroy(explosion, 2.2f);
        }
    }
}
