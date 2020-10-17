using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EndlessRunner : MonoBehaviour
{
    enum Level {
        Level0 = 0,
        Level1,
        Level2
    };

    int currLevel;
    Vector3 currTrackDirection = Vector3.forward;


    [SerializeField] private Vector3 startPos;



    [SerializeField] private GameObject playerTruck;
    [SerializeField] private GameObject planePrefab;


    float totalDistanceTraveled = 0f; // for score calc?
    float totalTimeTraveled = 0f; // for score calc?
    Vector3 lastFramePosition;
    Vector3 lastPlaneSpawnPos; // the last position of the player when the plane was spawned (lastPlaneSpawnPos + zDistPlaneSpawn)
    // is the center of the latest plane

    // NOTE: right now theres a bug that will infinitely spawn planes if distToSpawnNewPlane > ZDistPlaneSpawn
    [SerializeField, Range(0, 50)] float distToSpawnNewPlane; // distance traveled before you spawn a new plane
    [SerializeField, Range(-5, 5)] float yHeightOfPlane; // What height the planes spawn at
    [SerializeField, Range(0, 0.5f)] float spawnBuffer;

    private void Awake()
    {
        currLevel = 0;
        totalDistanceTraveled = 0;
        totalDistanceTraveled = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject test = Instantiate(planePrefab);
        //  test.SetActive(false);
        Debug.Log(test.GetComponent<Collider>().bounds);
        distToSpawnNewPlane = test.GetComponent<Collider>().bounds.size.z - spawnBuffer;
        Destroy(test);
        startPos = playerTruck.transform.position;

        lastFramePosition = playerTruck.transform.position;
        lastPlaneSpawnPos = playerTruck.transform.position + Vector3.forward * (-distToSpawnNewPlane / 2f);

    }

    // Update is called once per frame
    void Update()
    {
        totalTimeTraveled += Time.deltaTime;
        Vector3 dirTraveledSinceLastFrame = (playerTruck.transform.position - lastFramePosition);
        float distanceInTrackDirection = Vector3.Dot(dirTraveledSinceLastFrame, currTrackDirection.normalized);
        totalDistanceTraveled += distanceInTrackDirection;

        CheckToSpawnNewPlane();
        lastFramePosition = playerTruck.transform.position;
    }


    private bool CheckToSpawnNewPlane()
    {
        float dist = Mathf.Abs(playerTruck.transform.position.z - lastPlaneSpawnPos.z);
     //   Debug.Log(dist + " LAST PALNE: " + distToSpawnNewPlane);
        
        if (dist >= distToSpawnNewPlane)
        {
            GameObject plane = Instantiate(planePrefab) as GameObject;
            lastPlaneSpawnPos = playerTruck.transform.position;

            Debug.Log(startPos.x + "  PLAYER POS : " + playerTruck.transform.position);

            float zPos = playerTruck.transform.position.z + distToSpawnNewPlane;
            plane.transform.position = new Vector3(startPos.x, 0f, zPos);

            



            //Vector3 spawnPoint = playerTruck.transform.position + playerTruck.transform.forward * ZDistPlaneSpawn;
            //spawnPoint.y = yHeightOfPlane;
            //plane.transform.position = spawnPoint;
            return true;
        }
        return false;
    }
}
