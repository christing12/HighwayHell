using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class PlayGame : MonoBehaviour
{
   public TextMeshProUGUI displayText;
   
    bool isInBox;

    void Awake()
    {
        displayText.enabled = false;
    }
  
  void Update()
  {
      if(Input.GetKeyDown(KeyCode.E) && isInBox == true)
            {
            
                SceneManager.LoadScene("LarrySampleScene_2");

            }
  }
  
  void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            displayText.enabled = true;
            isInBox = true;

        
        }
        


}


    void OnTriggerExit(Collider cold)
    {
        if(cold.gameObject.tag == "Player")
        {
            displayText.enabled = false;
            isInBox = false;
            
        
        }

        }
}


