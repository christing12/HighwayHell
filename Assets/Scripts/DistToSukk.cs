using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public GameObject mainCamera; //Get the main camera so we can change the target to follow the enemy once we are sukked

    public Slider scoreSlider;
    public float sliderScaleFactor;

    private float dist; //Z distance between sukk and player car
    public AudioSource succSound;

    private void Start()
    {
        succSound.Play();
        

    }
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
            //Start death coroutine
            StartCoroutine(waitForDeathCoRoutine());
            //Also change camera to focus on sukk
            CameraFollow cf = mainCamera.GetComponent<CameraFollow>();
            cf.target = enemy.transform;
            //Also stop the score from increasing further
            ScoreManager sm = this.gameObject.GetComponent<ScoreManager>();
            sm.enabled = false;
        }

        //Update score slider display
        float displaySlider = 1 - (dist / sliderScaleFactor);
        if (displaySlider < 0) displaySlider = 0; //Minimum value is 0, Max value is 1
        succSound.volume = displaySlider;

        scoreSlider.value = displaySlider;
    }

    IEnumerator waitForDeathCoRoutine()
    {
        //Play sukk sound effect when the player gets close
        FindObjectOfType<AudioManager>().PlaySound("Sukk");

        yield return new WaitForSeconds(waitTime);// Wait for one second

        //After the timer has completed, load death scene
        SceneManager.LoadScene("DeathScene");
    }
}
