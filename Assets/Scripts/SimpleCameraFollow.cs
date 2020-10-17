using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    private Transform playerTruck;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTruck = GameObject.Find("PlayerTruck").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerTruck);
    }
}
