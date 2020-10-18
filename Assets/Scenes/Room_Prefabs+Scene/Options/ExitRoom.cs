using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class ExitRoom : MonoBehaviour
{   public TextMeshProUGUI displayText;
   
    
    int counter;
    bool isInBox;
    
    public bool isInBox2;
    // Start is called before the first frame update
    void Awake()
    {
        displayText.enabled = false;
        isInBox = false;
        isInBox2 = false;
        counter = 0;
    }

    // Update is called once per frame
       void Update()
    {       //Checks if player is in box and overrides what was in the collider
            if(Input.GetKeyDown(KeyCode.E) && isInBox == true && counter == 0)
            {
            displayText.text = "Don't leave yet!";
            
            

            counter+=1;

           
            }
            
             else if(Input.GetKeyDown(KeyCode.E) && counter == 1 && isInBox == true)
                {
                    displayText.text = "No really just keep playing the game!";
                    counter+=1;
                  
                    
                 }

                  else if(Input.GetKeyDown(KeyCode.E) && counter == 2 && isInBox == true)
                {
                    displayText.text = "Come on just one more!";
                    counter+=1;
                  
                 
                 }

                     else if(Input.GetKeyDown(KeyCode.E) && counter == 3 && isInBox == true)
                {
                    displayText.text = "Serouisly don't leave!";
                    counter+=1;
                  
                 
                 }

                     else if(Input.GetKeyDown(KeyCode.E) && counter == 4 && isInBox == true)
                {
                    displayText.text = "Think about all that work you need to catch up on...";
                    counter+=1;
                  
                 
                 }

                       else if(Input.GetKeyDown(KeyCode.E) && counter == 5 && isInBox == true)
                {
                    displayText.text = "Leaving is not escaping.";
                    counter+=1;
                  
                 
                 }

                       else if(Input.GetKeyDown(KeyCode.E) && counter == 6 && isInBox == true)
                {
                    displayText.text = "This IS your escape...";
                    counter+=1;
                  
                 
                 }

                        else if(Input.GetKeyDown(KeyCode.E) && counter == 7 && isInBox == true)
                {
                    displayText.text = "Please don't move on...";
                    counter+=1;
                  
                 
                 }

                        else if(Input.GetKeyDown(KeyCode.E) && counter == 8 && isInBox == true)
                {
                    displayText.text = "Please...";
                    counter+=1;
                  
                 
                 }

                        else if(Input.GetKeyDown(KeyCode.E) && counter == 9 && isInBox == true)
                {
                    displayText.text = "don't leave... comfort.";
                    counter+=1;
                  
                 
                 }

                          else if(Input.GetKeyDown(KeyCode.E) && counter == 10 && isInBox == true)
                {
                   Application.Quit();
                  
                 
                 }

                

    }


    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {   
            displayText.enabled = true;
            displayText.text = "E to leave...";
            isInBox = true;

        
        }
        


}


    void OnTriggerExit(Collider cold)
    {
        if(cold.gameObject.tag == "Player")
        {
            displayText.enabled = false;
            isInBox = false;
            counter = 0;
        
        }

        }
}
