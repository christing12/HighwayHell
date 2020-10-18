using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Calculates the distance between Sukk and the player
//And manages the notification text/death based on that
public class DistToSukk : MonoBehaviour
{

    public Rigidbody enemy; //Sukk monster
    public Rigidbody player; //Player car
    public TextMeshProUGUI distText;

    private float dist; //Z distance between sukk and player car

    // Update is called once per frame
    void Update()
    {
        //When dist is positive, the player is OK and when dist is negative the player has lost
        dist = enemy.position.z - player.position.z;
        //Update distance to sukk text
        distText.SetText(dist.ToString("F0"));
    }
}
