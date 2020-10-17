using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFlying : MonoBehaviour
{
    Rigidbody rbPlayer;
    public string floorName;//Wasn't sure how to avoid floor collisions, so I just take the name of the object it is on.
  

    Rigidbody rb;
    Vector3 shotDirection;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = this.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.collider.name != floorName)
        {
            rb = collision.rigidbody; //gets enemies rigid body
            Vector3 enemyPosition = rb.position;
            Vector3 playerPosition = rbPlayer.position;

            float shotDirection = calcSlope(playerPosition, enemyPosition);

            //based on moving in the positive x direction.
            if ((shotDirection < 0 || shotDirection > 0) && playerPosition.x > enemyPosition.x)
            {
                rb.AddForce(-5000, 5000, 0);
            }
            else
            {
                rb.AddForce(5000, 5000, 0);
            }

            
        }
    }

    private float calcSlope(Vector3 player, Vector3 enemy)
    {
        float playerx = player.x;
        float playerz = player.z;
        float enemyx = enemy.x;
        float enemyz = enemy.z;

        return (enemyz - playerz) / (enemyx - playerx);
    }
}
