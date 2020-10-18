using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sukk : MonoBehaviour
{   
    [SerializeField] public float gravityPull;
    public static float gravityRadius = 3f;
    float resetPull;

    bool carIsMax;
    float carCounter;
    public float chaosEnergy;
    public float counter;
   void Awake()
   {
       gravityRadius = GetComponent<SphereCollider>().radius;
       carIsMax = false;
       resetPull = gravityPull;
   }
    // Start is called before the first frame update
   


    void Start()
    {
        
    }

    void Update ()
    {
       if (carCounter >= counter)
       {
         gravityPull *= -3;
         carCounter = 0;
         StartCoroutine(ResetCounter());

       }
    }

   IEnumerator ResetCounter()
   {
      yield return new WaitForSeconds(1);
      gravityPull = resetPull;
      
   }

    // Update is called once per frame
  void OnTriggerStay(Collider other)
      { 
         if(other.attachedRigidbody && other.gameObject.tag != "triggerable")
         {
            float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) /gravityRadius;
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * gravityPull * Time.smoothDeltaTime * chaosEnergy);
            other.attachedRigidbody.AddForce((transform.position + other.transform.position) * gravityIntensity);
            // other.attachedRigidbody.AddForce(Vector3.up * chaosEnergy);
            Debug.DrawRay(other.transform.position, transform.position - other.transform.position);

            

         }

         if (other.tag == "Enemy")
            {
                carCounter+=1;
               // Debug.Log("Car Counter: " + carCounter); 

            }
        


         }
      }


         
      
   
   
   
   


