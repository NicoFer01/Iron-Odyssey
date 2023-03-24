using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piercer : MonoBehaviour
{
    private float _speed = 7.0f;

    void Update()
    {
        transform.position += Vector3.forward * (-1) * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerControllerCC>().Death();
    }
}
