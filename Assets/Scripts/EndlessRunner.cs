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
    Vector3 lastFramePosition;
    Vector3 lastPlaneSpawnPos; // the last position of the player when the plane was spawned (lastPlaneSpawnPos + zDistPlaneSpawn)
    // is the center of the latest plane

    // NOTE: right now theres a bug that will infinitely spawn planes if distToSpawnNewPlane > ZDistPlaneSpawn
    [SerializeField, Range(0, 50)] float distToSpawnNewPlane; // distance traveled before you spawn a new plane
    [SerializeField, Range(0, 15)] float ZDistPlaneSpawn; // how far out do the planes spawn in front of you
    [SerializeField, Range(-5, 5)] float yHeightOfPlane; // What height the planes spawn at
    [SerializeField, Range(0, 5)] float spawnDist; //

    private void Awake()
    {
        currLevel = 0;
        totalDistanceTraveled = 0;
        totalDistanceTraveled = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log(dist);
        if (dist >= distToSpawnNewPlane)
        {
           // Debug.Log("Spawning New Plane");
            GameObject plane = Instantiate(planePrefab) as GameObject;
           // Debug.Log(plane.GetComponent<Collider>().bounds);
            lastPlaneSpawnPos = playerTruck.transform.position;
            Vector3 spawnPoint = playerTruck.transform.position + playerTruck.transform.forward * ZDistPlaneSpawn;
            spawnPoint.y = yHeightOfPlane;
            plane.transform.position = spawnPoint;
            return true;
        }
        return false;
    }
}
