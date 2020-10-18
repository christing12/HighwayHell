using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

using TMPro;

public class CantEscape : MonoBehaviour
{   
    public TextMeshProUGUI cantEscape;
    

    
    // Start is called before the first frame update

    float count;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ShowText());
            count+=1;
          
        }

        if(count == 2)
        {
            cantEscape.fontSize = 25;
        }

          if(count == 3)
        {
            cantEscape.fontSize = 30;
        }


          if(count == 4)
        {
            cantEscape.fontSize = 50;
        }

          if(count == 5)
        {
            SceneManager.LoadScene("Room");
        }
    }

    IEnumerator ShowText()
    {
        cantEscape.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        cantEscape.gameObject.SetActive(false);
    }
}
