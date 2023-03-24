using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlatform : MonoBehaviour
{
    [SerializeField] private GameObject[] platforms;

    public void PlacePlatform()
    {
        Instantiate(platforms[Random.Range(0, platforms.Length)], transform.position, Quaternion.identity);
    }
}
