using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadComputer : MonoBehaviour
{
   
    public TextMeshProUGUI displayText;
   
    int counter;
    bool isInBox;

    bool isInBox2;
    void Start()
    {
        displayText.enabled = false;
        counter = 0;
        isInBox = false;
      
    }

    // Update is called once per frame
    void Update()
    {       //Checks if player is in box and overrides what was in the collider
            if(Input.GetKeyDown(KeyCode.E) && isInBox == true && counter == 0)
            {
            displayText.text = "You check your phone and realize how much you've been ignoring work and friends. Not responding to anything and prolonging to finish assignments..";
            
            

            counter+=1;

           
            }
            
             else if(Input.GetKeyDown(KeyCode.E) && counter == 1 && isInBox == true)
                {
                    displayText.text = "But forget about that! Get a highscore on Highway Hell first!";
                    counter+=1;
                  
                    
                 }
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            displayText.enabled = true;
            displayText.text = "Press E to read.";
            isInBox = true;

        
        }

          

       
    }


    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            displayText.enabled = false;
            isInBox = false;
            counter = 0;
 
        }

        }
    }

