using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] platform;
    private int num_platform;

    public Transform[] spawnPoints;

    private int num_SpawnPoints;


    

    // Use this for initialization
    void Start()
    {
        Spawn();
    }
    void Spawn()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int rand = Random.Range(1, 4);
            PickAPlatform(i, rand);
        }
    }

    private void PickAPlatform(int i, int rand)
    {
        switch (rand)
        {
            case 1:
                Instantiate(platform[0], spawnPoints[i].position, Quaternion.identity);
                break;
            case 2:
                Instantiate(platform[1], spawnPoints[i].position, Quaternion.identity);
                break;
            case 3:
                Instantiate(platform[2], spawnPoints[i].position, Quaternion.identity);
                break;
        }
    }
}