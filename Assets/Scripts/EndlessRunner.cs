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

    [SerializeField] private GameObject playerTruck;
    [SerializeField] private GameObject planePrefab;


    float totalDistanceTraveled = 0f; // for score calc?
    float totalTimeTraveled = 0f; // for score calc?


    // NOTE: right now theres a bug that will infinitely spawn planes if distToSpawnNewPlane > ZDistPlaneSpawn
    [SerializeField, Range(0, 50)] float distToSpawnNewPlane; // distance traveled before you spawn a new plane
   // [SerializeField, Range(-5, 5)] float yHeightOfPlane; // What height the planes spawn at
    [Tooltip("Percentage Length of how far back you want to start spawning")]
    [SerializeField, Range(0, 100)] float spawnBuffer;
    [SerializeField, Range(0, 10)] int numPlanesBuffer;

    private Vector3 startPos;
    Vector3 lastFramePosition;
    Vector3 lastPlaneSpawnPos; // the last position of the player when the plane was spawned (lastPlaneSpawnPos + zDistPlaneSpawn)
    // is the center of the latest plane
    Vector3 lastPlanePositionSpawnedAt;

    private void Awake()
    {
        currLevel = 0;
        totalDistanceTraveled = 0;
        totalDistanceTraveled = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        distToSpawnNewPlane = planePrefab.GetComponent<Collider>().bounds.size.z;
        if(!ValidateZDist(distToSpawnNewPlane, planePrefab.GetComponent<Collider>()))
        {
            Debug.LogError("Z Dist to spawn new plane is less than lenght of plane");
        }

        startPos = planePrefab.transform.position;
        for (int i = 0; i < numPlanesBuffer; i++)
        {
            GameObject extraPlane = Instantiate(planePrefab);
            extraPlane.transform.position = startPos + Vector3.forward * (i + 1) * planePrefab.GetComponent<Collider>().bounds.size.z;
        }
        lastPlanePositionSpawnedAt = startPos + Vector3.forward * numPlanesBuffer * distToSpawnNewPlane;
        Debug.Log(lastPlanePositionSpawnedAt);
        lastFramePosition = playerTruck.transform.position;
        lastPlaneSpawnPos = playerTruck.transform.position + Vector3.forward * (-distToSpawnNewPlane * (spawnBuffer / 100f));

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

            //float zPos = playerTruck.transform.position.z + distToSpawnNewPlane;
            //plane.transform.position = new Vector3(startPos.x, startPos.y, zPos);

            plane.transform.position = lastPlanePositionSpawnedAt + Vector3.forward * distToSpawnNewPlane;
            lastPlanePositionSpawnedAt = plane.transform.position;



            //Vector3 spawnPoint = playerTruck.transform.position + playerTruck.transform.forward * ZDistPlaneSpawn;
            //spawnPoint.y = yHeightOfPlane;
            //plane.transform.position = spawnPoint;
            return true;
        }
        return false;
    }

    private bool ValidateZDist(float ZDist, Collider collider)
    {
        if (ZDist > collider.bounds.size.z)
            return false;
        else return true;
    }
}
