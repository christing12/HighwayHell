using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendFlying : MonoBehaviour
{
    Rigidbody rbPlayer;
    public float force = 5000;

    Rigidbody rb;
    Vector3 shotDirection;

    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = this.GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().PlaySound("Impact");

            //Debug.Log("ENEMY COLLIDED");
            rb = collision.rigidbody; //gets enemies rigid body
            Vector3 enemyPosition = rb.position;
            Vector3 playerPosition = rbPlayer.position;

            float shotDirection = calcSlope(playerPosition, enemyPosition);

            //based on moving in the positive x direction.
            if ((shotDirection < 0 || shotDirection > 0) && playerPosition.x > enemyPosition.x)
            {
                rb.AddForce(-force, force, 0);
            }
            else
            {
                rb.AddForce(force, force, 0);
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
