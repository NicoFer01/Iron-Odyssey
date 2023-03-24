using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;

public class PlayerControllerCC : MonoBehaviour
{
    //movimiento de lado
    [SerializeField] private float _sidewaySpeed;

    //correr
    [SerializeField] public float _runningSpeed;

    //saltar
    [SerializeField] private float _jumpForce;

    //gravedad
    [SerializeField] private float _gravity;

    //animator
    [SerializeField] private Animator _animator;

    //stateMachine
    [SerializeField] private StateMachine _state;

    //metros recorridos
    [SerializeField] private Text _metersOutput;

    private int _metersCounter;

    //input
    public string _road;

    public string _goToRoad;

    [SerializeField] private CharacterController _player;

    public Vector3 _movementDir;
    private Vector3 _prevPlayerPos;
    public bool _isJumping;
    private bool _destiny;
    public bool _isWallrunning;
    private float _wallrunInitialTime;
    private float _wallrunTime;

    [SerializeField] private float incrementoVelocidad;
    [SerializeField] private float tiempoIncrementoVelocidad;
    [SerializeField] private float lapsoNivel;

    void Start()
    {
        this._player = this.gameObject.GetComponent<CharacterController>();

        this._sidewaySpeed = 0f;
        this._runningSpeed = 20f;
        this._jumpForce = 10f;
        this._gravity = 25f;

        this._metersCounter = 0;
        this._road = "Middle";

        this._destiny = true;
        this._isWallrunning = false;
        this._wallrunTime = 2f;
        
        this.incrementoVelocidad = 0.1f;
        this.lapsoNivel = 5f;
}

    void Update()
    {
        if (this._player.isGrounded) //Cuando el personaje toca el suelo
        {
            this._movementDir = this._player.transform.TransformDirection(new Vector3(_sidewaySpeed, 0f, _runningSpeed));
            this._isJumping = false;
            this._isWallrunning = false;

            GetInput();
        }

        if (this._isWallrunning) //Cuando el personaje toca las paredes (hasta que cae al suelo)
        {
            this._movementDir.x = this._sidewaySpeed;
            if (Time.time - this._wallrunInitialTime >= this._wallrunTime)
            {
                ExitWallrun();
            }

            GetInputWallrun();
        }

        SidewayMove();

        this._movementDir -= new Vector3(0f, this._gravity * Time.deltaTime, 0f);
        this._player.Move(this._movementDir * Time.deltaTime);        

        MetersTraveled();

        if (Input.GetKeyDown(KeyCode.Escape)) //Boton PAUSA
        {
            Pause();
        }

        if (Time.time - tiempoIncrementoVelocidad > lapsoNivel)
        {
            Debug.Log("Acelerar");
            _runningSpeed += incrementoVelocidad;
            tiempoIncrementoVelocidad = Time.time;
        }
    }

    private void GetInput() //Input en el suelo
    {
        if (Input.GetKeyDown(KeyCode.A) && _road != "LeftWall")
        {
            this._sidewaySpeed = -15f;

            _goToRoad = "ToLeft";
            _destiny = false;
        }

        else if (Input.GetKeyDown(KeyCode.D) && _road != "RightWall")
        {
            this._sidewaySpeed = 15f;

            _goToRoad = "ToRight";
            _destiny = false;
        }

        else if (Input.GetKeyDown(KeyCode.Space) && this._destiny) Jump();
    }

    private void GetInputWallrun() //Input en las paredes 
    {
        if (Input.GetKeyDown(KeyCode.D) && _road == "LeftWall" || Input.GetKeyDown(KeyCode.A) && _road == "RightWall") ExitWallrun();

        if (Input.GetKeyDown(KeyCode.Space)) ExitWallrun();
    }

    private void SidewayMove() //Movimientos laterales
    {
        if (_goToRoad == "ToLeft" && !_destiny) //Hacia la izquierda
        {
            if (_road == "Left")
            {
                if (transform.position.x <= -2)
                {
                    Jump();
                    _road = "LeftWall";
                }
            }

            else if (_road == "Middle")
            {
                if (this.transform.position.x >= -2.2f && this.transform.position.x <= -1.8f)
                {
                    _road = "Left";
                    _destiny = true;
                }
            }

            else if (_road == "Right")
            {
                if (this.transform.position.x <= 0.2f && this.transform.position.x >= -0.2f)
                {
                    _road = "Middle";
                    _destiny = true;
                }
            }

            else if (_road == "RightWall")
            {
                if (this.transform.position.x <= 2.2f && this.transform.position.x >= 1.8f)
                {
                    _road = "Right";
                    _destiny = true;
                }
            }
        }

        if (_goToRoad == "ToRight" && !_destiny) //Hacia la derecha
        {
            if (_road == "Right")
            {
                if (transform.position.x >= 2)
                {
                    Jump();
                    _road = "RightWall";
                }
            }

            else if (_road == "Middle")
            {
                if (this.transform.position.x <= 2.2f && this.transform.position.x >= 1.8f)
                {
                    _road = "Right";
                    _destiny = true;
                }
            }

            else if (_road == "Left")
            {
                if (this.transform.position.x <= 0.2f && this.transform.position.x >= -0.2f)
                {
                    _road = "Middle";
                    _destiny = true;
                }
            }

            else if (_road == "LeftWall")
            {
                if (this.transform.position.x >= -2.2f && this.transform.position.x <= -1.8f)
                {
                    _road = "Left";
                    _destiny = true;
                }
            }
        }

        if (this._destiny) this._sidewaySpeed = 0f;

        else this._movementDir.x = this._sidewaySpeed;
    }

    private void Jump() //Salto
    {
        _animator.SetTrigger("jumpTrigger");
        _movementDir.y += _jumpForce;
        _isJumping = true;
    }

    private void OnTriggerEnter(Collider other) //Colision con paredes
    {
        if (other.gameObject.tag == "Wallrun")
        {
            Wallrun();
        }
    }

    public void Death() //Muerte
    {
        this._runningSpeed = 20f;
        Destroy(this.gameObject);
        _state.STATE_MACHINE = "GameOver";
    }

    private void Wallrun() //Carrera por la pared
    {
        if (!this._isWallrunning) this._wallrunInitialTime = Time.time;
        this._isJumping = false;
        this._isWallrunning = true;
        this._movementDir.y = 0f;
        this._gravity = 0f;
        this._movementDir.x = 0f;
        _destiny = true;
    }

    private void ExitWallrun() //Salida de la carrera por la pared
    {
        this._gravity = 25f;
        if (this._road == "LeftWall")
        {
            this._sidewaySpeed = 15f;
            this._goToRoad = "ToRight";
        }
        else if (this._road == "RightWall")
        {
            this._sidewaySpeed = -15f;
            this._goToRoad = "ToLeft";
        }
        this._destiny = false;
        this._movementDir.x = this._sidewaySpeed;
    }

    private void Pause() //Pausa
    {
        Time.timeScale = 0f;
        _state.STATE_MACHINE = "Pause";
    }

    private void MetersTraveled() //Metros recorridos por el personaje
    {
        float _actualPlayerPos = transform.position.z - _prevPlayerPos.z;
        if (_actualPlayerPos >= 4)
        {
            _metersCounter++;
            _metersOutput.text = _metersCounter + " m";
            _prevPlayerPos = transform.position;
        }
    }
}
