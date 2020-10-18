using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;
using UnityEngine.SceneManagement;

public class EndlessRunner : MonoBehaviour
{

    public static EndlessRunner instance;
    [SerializeField] private SpawnTable spawnTable;
    enum Level {
        Level0 = 0,
        Level1,
        Level2
    };

    int currLevel;
    Vector3 currTrackDirection = Vector3.forward;

    [SerializeField] private GameObject playerTruck;
    [SerializeField] private GameObject planePrefab;
    [SerializeField] private GameObject enemyPrefab;


    float totalDistanceTraveled = 0f; // for score calc?
    float totalTimeTraveled = 0f; // for score calc?


    // NOTE: right now theres a bug that will infinitely spawn planes if distToSpawnNewPlane > ZDistPlaneSpawn
    [SerializeField, Range(0, 50)] float distToSpawnNewPlane; // distance traveled before you spawn a new plane
   // [SerializeField, Range(-5, 5)] float yHeightOfPlane; // What height the planes spawn at
    [Tooltip("Percentage Length of how far back you want to start spawning")]
    [SerializeField, Range(0, 100)] float spawnBuffer;
    [SerializeField, Range(0, 10)] int numPlanesBuffer;
    [SerializeField, Range(0, 30)] float timePerThreshold;

    private Vector3 startPos;
    Vector3 lastFramePosition;
    Vector3 lastPlaneSpawnPos; // the last position of the player when the plane was spawned (lastPlaneSpawnPos + zDistPlaneSpawn)
    // is the center of the latest plane
    Vector3 lastPlanePositionSpawnedAt;

    [SerializeField, Range(0, 5)] float extraBuffer;

    float zLen;


    int numThresholdsPassed = 1;

    private void Awake()
    {
        currLevel = 0;
        totalDistanceTraveled = 0;
        totalDistanceTraveled = 0;
        numThresholdsPassed = 1;

        spawnTable.ValidateSpawnable();
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        zLen = planePrefab.GetComponent<Collider>().bounds.size.z;
        distToSpawnNewPlane = zLen - extraBuffer;
        if(!ValidateZDist(distToSpawnNewPlane, planePrefab.GetComponent<Collider>()))
        {
            Debug.LogError("Z Dist to spawn new plane is less than lenght of plane");
        }

        startPos = planePrefab.transform.position;
        for (int i = 0; i < numPlanesBuffer; i++)
        {
            GameObject extraPlane = ObjectPooler.SharedInstance.GetPooledObject("ground");
            extraPlane.SetActive(true);
            extraPlane.transform.position = startPos + Vector3.forward * (i + 1) * distToSpawnNewPlane;
            lastPlanePositionSpawnedAt = extraPlane.transform.position;
        }
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
        if (totalTimeTraveled >= numThresholdsPassed * timePerThreshold)
        {
            Debug.Log("Changing Thresholds");
            numThresholdsPassed++;
        }

        CheckToSpawnNewPlane();
        lastFramePosition = playerTruck.transform.position;
    }


    private bool CheckToSpawnNewPlane()
    {
        float dist = Mathf.Abs(playerTruck.transform.position.z - lastPlaneSpawnPos.z);
        if (dist >= distToSpawnNewPlane - extraBuffer)
        {
            GameObject plane = ObjectPooler.SharedInstance.GetPooledObject("ground");
            lastPlaneSpawnPos = playerTruck.transform.position;
            plane.SetActive(true);
            plane.transform.position = lastPlanePositionSpawnedAt + Vector3.forward * distToSpawnNewPlane - Vector3.up * 0.0001f;
            lastPlanePositionSpawnedAt = plane.transform.position;


            SpawnNewEnemies(lastPlanePositionSpawnedAt , plane.GetComponent<Collider>());
            DisableOldGround();
           
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

    private void DisableOldGround()
    {
        List<GameObject> activeGround = ObjectPooler.SharedInstance.GetActiveGameObjects("ground");
        
        foreach (GameObject ground in activeGround)
        {
            Vector3 forwardPos = ground.transform.position + Vector3.forward * (ground.GetComponent<Collider>().bounds.extents.z  + distToSpawnNewPlane);
            if (forwardPos.z - playerTruck.transform.position.z < 0)
            {
                ground.SetActive(false);

            }
        }
    }

    private void SpawnNewEnemies(Vector3 planePosition, Collider collider)
    {
        Bounds b = collider.bounds;
        float xPos = Random.Range(planePosition.x - b.extents.x, planePosition.x + b.extents.x);
        float yPos = Random.Range(planePosition.z - b.extents.z, planePosition.z + b.extents.z);


        Spawnable s = spawnTable.PickSpawnable();
        int numToSpawn = Random.Range(1, numThresholdsPassed);
        numToSpawn = numToSpawn <= 0 ? 1 : numToSpawn;
        for (int i = 0; i < numToSpawn; i++)
        {
            GameObject enemy = ObjectPooler.SharedInstance.GetPooledObjectByName(s.obj.name);
            if (enemy != null)
            {
                enemy.SetActive(true);
                enemy.transform.position = new Vector3(xPos, playerTruck.transform.position.y, yPos);
            }
        }
    }

    public bool isDeathPlaying = false;
    public IEnumerator PlayDeath(AudioSource sound)
    {
        Debug.Log("Dying");
        isDeathPlaying = true;
        sound.Play();
        float t = 0.0f;
        while (sound.isPlaying)
        {
            t += Time.deltaTime;
            yield return null;
        }
        isDeathPlaying = false;
        yield return new WaitForSeconds(5f - t);
        SceneManager.LoadScene("DeathScene");
    }
}
