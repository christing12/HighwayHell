using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sukk : MonoBehaviour
{   
    [SerializeField] public float gravityPull;
    public static float gravityRadius = 3f;
    public float chaosEnergy;
   void Awake()
   {
       gravityRadius = GetComponent<SphereCollider>().radius;
   }
    // Start is called before the first frame update
   
   
    void Start()
    {
        
    }

    // Update is called once per frame
  void OnTriggerStay(Collider other)
      {
         if(other.attachedRigidbody)
         {
            float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) /gravityRadius;
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * gravityPull * Time.smoothDeltaTime * chaosEnergy);
            other.attachedRigidbody.AddForce((transform.position + other.transform.position) * gravityIntensity);
            // other.attachedRigidbody.AddForce(Vector3.up * chaosEnergy);
            Debug.DrawRay(other.transform.position, transform.position - other.transform.position);
         }
      }
   }


