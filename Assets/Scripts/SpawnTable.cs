using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Spawnable
{
    public GameObject obj;
    public float weight;

    [HideInInspector]
    public Vector2 probabilityRange;
}


[CreateAssetMenu]
public class SpawnTable : ScriptableObject
{
    public Spawnable[] spawnable;

    public float totalProbabilityWeight;


    public void ValidateSpawnable()
    {
        float currMaxProbability = 0f;
        foreach (Spawnable spawn in spawnable)
        {
            spawn.probabilityRange.x = currMaxProbability;
            currMaxProbability += spawn.weight;
            spawn.probabilityRange.y = currMaxProbability;
        }
        totalProbabilityWeight = currMaxProbability;
    }

    public Spawnable PickSpawnable()
    {
        float num = Random.Range(0, totalProbabilityWeight);
        foreach(Spawnable spawn in spawnable)
        {
            if (num > spawn.probabilityRange.x && num < spawn.probabilityRange.y)
            {
                return spawn;
            }
        }
        Debug.Log("ERRRRROR");
        return spawnable[0];
    }
}
