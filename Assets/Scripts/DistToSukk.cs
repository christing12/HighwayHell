using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

//Calculates the distance between Sukk and the player
//And manages the notification text/death based on that
public class DistToSukk : MonoBehaviour
{

    public Rigidbody enemy; //Sukk monster
    public Rigidbody player; //Player car
    public TextMeshProUGUI distText;
    public float deathThreshold; //distance before death is counted (behind the sukk)
    public float waitTime; //Number of seconds before killing the player after Sukk has caught up

    private float dist; //Z distance between sukk and player car

    // Update is called once per frame
    void Update()
    {
        //When dist is positive, the player is OK and when dist is negative the player has lost
        dist = player.position.z - enemy.position.z;
        //Update distance to sukk text
        distText.SetText(dist.ToString("F0"));

        //Check if Sukk has caught up, and if it has, start coroutine
        if (dist <= deathThreshold)
        {
            StartCoroutine(waitForDeathCoRoutine());
        }
    }

    IEnumerator waitForDeathCoRoutine()
    {
        yield return new WaitForSeconds(waitTime);// Wait for one second

        //After the timer has completed, load death scene
        SceneManager.LoadScene("DeathScene");
    }
}
