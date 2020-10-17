using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    [SerializeField] private GameObject playerTruck;
    [SerializeField, Range(0, 15)] private float moveSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        playerTruck.transform.position += Vector3.forward * vertical * moveSpeed * Time.deltaTime;

    }
}
