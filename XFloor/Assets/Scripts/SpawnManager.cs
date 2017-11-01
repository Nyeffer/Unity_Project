using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject platform;
    public Transform platform_0;
    public Transform platform_N1;
    public Transform platform_P1;
    public Transform platform_N2;
    public Transform platform_P2;
    public Transform platform_N3;
    public Transform platform_P3;
    public Transform platform_N4;
    public Transform platform_P4;

    // Use this for initialization
    void Start()
    {
        Spawn();
    }
    void Spawn()
    {
            Instantiate(platform, platform_0.transform.position, Quaternion.identity);
            Instantiate(platform, platform_N1.transform.position, Quaternion.identity);
            Instantiate(platform, platform_N2.transform.position, Quaternion.identity);
            Instantiate(platform, platform_N3.transform.position, Quaternion.identity);
            Instantiate(platform, platform_N4.transform.position, Quaternion.identity);
            Instantiate(platform, platform_P1.transform.position, Quaternion.identity);
            Instantiate(platform, platform_P2.transform.position, Quaternion.identity);
            Instantiate(platform, platform_P3.transform.position, Quaternion.identity);
            Instantiate(platform, platform_P4.transform.position, Quaternion.identity);
    }
}