using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private StateMachine _state;
    
    // Start is called before the first frame update
    void Start()
    {
        this._player = GameObject.Find("Character_99 (CC)").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_state.STATE_MACHINE == "Play")
        {
            transform.position = new Vector3(Mathf.Clamp(_player.position.x, -3.0f, 3.0f), Mathf.Clamp(_player.position.y + (4.2f), 4.0f, 6.5f), _player.position.z + (-3.1f));
            
            //Solo para revisión
            //transform.position = new Vector3(-3f, Mathf.Clamp(_player.position.y, 0.0f, 6.5f), _player.position.z);
        }
    }
}
