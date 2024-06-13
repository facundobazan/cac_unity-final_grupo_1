using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed = 5f;
    [SerializeField]
    private float _walkingSpeed = 5f;
    [SerializeField]
    private float _runningSpeed = 7f;
    [SerializeField]
    private float _crouchingSpeed = 3f;

    private InputControls _inputSystem;
    private CharacterController _characterController;
    private Animator _animator;

    private Vector2 _move = Vector2.zero;
    private float _currentSpeed = 0f;
    private bool _isRunning = false;
    private bool _isDucking = false;
    private bool _illuminate = false;
    private bool _isJumping = false;

    public void Awake()
    {
        _inputSystem = new InputControls();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        if (_inputSystem == null)
        {
            return;
        }

        //Player
        _inputSystem.Player.Action.performed += ctx => { OnAction(ctx); };

        //_inputSystem.Player.Duck.canceled += ctx => _isDucking = false;
        _inputSystem.Player.Duck.performed += ctx => { OnDuck(ctx); };

        //_inputSystem.Player.Jump.canceled += ctx => _isJumping = false;
        _inputSystem.Player.Jump.performed += ctx => { OnJump(ctx); };

        //_inputSystem.Player.Lamp.canceled += ctx => _useLamp = false;
        _inputSystem.Player.Lamp.performed += ctx => { OnLamp(ctx); };

        _inputSystem.Player.Move.canceled += ctx => _move = Vector2.zero;
        _inputSystem.Player.Move.performed += ctx => { OnMove(ctx); };

        _inputSystem.Player.Run.canceled += ctx => _isRunning = false;
        _inputSystem.Player.Run.performed += ctx => { OnRun(ctx); };
        //UI
        _inputSystem.UI.Inventary.performed += ctx => { Inventary(ctx); };
        _inputSystem.UI.Map.performed += ctx => { Map(ctx); };
        _inputSystem.UI.Pause.performed += ctx => { Pause(ctx); };
    }

    // Update is called once per frame
    void Update()
    {
        //OnMove();
        //_move = _inputSystem.Player.Move.ReadValue<Vector2>();
        //Debug.Log(_move);





        SetAnimator();
        /*_animator.SetBool("Duck", _inputSystem.Player.Duck.ReadValue<float>() == 1 ? true : false);
        _animator.SetBool("Run", _inputSystem.Player.Run.ReadValue<float>() == 1 ? true : false);*/
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _move = _inputSystem.Player.Move.ReadValue<Vector2>();

        Rotate();

        Move();
    }

    private void Rotate()
    {
        transform.Rotate(0, _move.x * _rotateSpeed, 0);
    }

    private void Move()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        //float speed = _currentSpeed * _move.y;

        _characterController.SimpleMove(forward * _currentSpeed * _move.y);
    }

    private void SetAnimator()
    {
        _animator.SetFloat("VelX", _move.x);
        _animator.SetFloat("VelY", _move.y);
        _animator.SetBool("Run", _isRunning);
        _animator.SetBool("Duck", _isDucking);

        //_animator.SetBool("Duck", _isRunning ? false : _isDucking);
        //_animator.SetBool("useLamp", _illuminate);
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        //Debug.Log("Action");
        _animator.Play("Action");
    }

    private void OnDuck(InputAction.CallbackContext context)
    {
        //Debug.Log("Duck");
        _isDucking = !_isDucking;

        if (_isDucking)
        {
            if (_isRunning) _isRunning = false;

            _animator.Play("Blend Down");
            _currentSpeed = _crouchingSpeed;
        }
        else
            _animator.Play("Get Up");

    }

    private void OnJump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        //TODO: generar salto
        _animator.Play("Jump");
    }

    private void OnLamp(InputAction.CallbackContext context)
    {
        //Debug.Log("Lamp");
        _illuminate = !_illuminate;
        _animator.Play(_illuminate ? "Illuminate" : "Deillumine");
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        //Debug.Log("Run");
        if (_isDucking) return;

        _isRunning = true;
        _currentSpeed = _runningSpeed;
    }

    private void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Pause");
    }

    private void Inventary(InputAction.CallbackContext context)
    {
        Debug.Log("Inventary");
    }

    private void Map(InputAction.CallbackContext context)
    {
        Debug.Log("Map");
    }


    private void OnEnable()
    {
        _inputSystem.Player.Action.Enable();
        _inputSystem.Player.Duck.Enable();
        _inputSystem.Player.Jump.Enable();
        _inputSystem.Player.Lamp.Enable();
        _inputSystem.Player.Move.Enable();
        _inputSystem.Player.Run.Enable();
        _inputSystem.UI.Pause.Enable();
        _inputSystem.UI.Inventary.Enable();
        _inputSystem.UI.Map.Enable();
    }

    private void OnDisable()
    {
        _inputSystem.Player.Action.Disable();
        _inputSystem.Player.Duck.Disable();
        _inputSystem.Player.Jump.Disable();
        _inputSystem.Player.Lamp.Disable();
        _inputSystem.Player.Move.Disable();
        _inputSystem.Player.Run.Disable();
        _inputSystem.UI.Pause.Disable();
        _inputSystem.UI.Inventary.Disable();
        _inputSystem.UI.Map.Disable();
    }

    private void LateUpdate()
    {
        ResetVariables();
    }

    private void ResetVariables()
    {
        _move = Vector2.zero;
        _currentSpeed = _walkingSpeed;
        //_isRunning = false;
    }
}

/*
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _move = Vector2.zero;
    private Vector3 _v3 = Vector3.zero;
    [SerializeField]
    private float _rotateSpeed = 5f;
    [SerializeField]
    private float[] _speeds = { 5f, 7f, 3f };
    private float _speed = 0f;
    private InputControls _inputSystem;
    private CharacterController _characterController;
    private Animator _animator;
    private bool _isRunning = false;
    private bool _isDucking = false;


    public void Awake()
    {
        _inputSystem = new InputControls();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        if (_inputSystem == null)
        {
            return;
        }

        _inputSystem.Player.Action.performed += ctx => { Action(ctx); };
        _inputSystem.Player.Duck.performed += ctx => { Duck(ctx); };
        _inputSystem.Player.Jump.performed += ctx => { Jump(ctx); };
        _inputSystem.Player.Lamp.performed += ctx => { Lamp(ctx); };
        _inputSystem.Player.Run.performed += ctx => { Run(ctx); };
        _inputSystem.UI.Pause.performed += ctx => { Pause(ctx); };
        _inputSystem.UI.Inventary.performed += ctx => { Inventary(ctx); };
        _inputSystem.UI.Map.performed += ctx => { Map(ctx); };
    }

    // Update is called once per frame
    void Update()
    {
        _move = _inputSystem.Player.Move.ReadValue<Vector2>();
        //Debug.Log(_move);

        if (_move != Vector2.zero) Move();

        //if (_move != Vector2.zero) Move();
        _animator.SetFloat("VelX", _move.x);
        _animator.SetFloat("VelY", _move.y);

        SetAnimator();


        //_animator.SetBool("Duck", ReadAction(PlayerState.Duck));
        //_animator.SetBool("Run", ReadAction(PlayerState.Run));

        //_characterController.Move(_move.x * _speed * Time.deltaTime, 0, _move.y * _speed * Time.deltaTime);
    }

    private bool ReadAction(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Run:
                return _inputSystem.Player.Duck.ReadValue<float>() == 1 ? true : false;
                break;
            case PlayerState.Duck:
                return _inputSystem.Player.Duck.ReadValue<float>() == 1 ? true : false;
                break;
        }

        return false;
    }

    private void SetAnimator(){

        if (ReadAction(PlayerState.Duck))
        {
            _animator.SetBool("Duck", true);
            _animator.SetBool("Run", false);
            return;
        }

        if (ReadAction(PlayerState.Run))
        {
            _animator.SetBool("Duck", false);
            _animator.SetBool("Run", true);
            return;
        }

        _animator.SetBool("Duck", false);
        _animator.SetBool("Run", false);
    }

    private void Move()
    {

        Rotate();

        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //float currentSpeed = _speed * _move.y;
        //_characterController.SimpleMove(forward * currentSpeed);

        _characterController.SimpleMove(Forward() * CurrentSpeed());

        //Vector2 currentPosition = transform.position;
        //currentPosition.x += _move.x * _speed * Time.deltaTime;
        //transform.position = currentPosition;
    }

    private void Rotate()
    {
        transform.Rotate(0, _move.x * _rotateSpeed, 0);
    }

    private float CurrentSpeed()
    {
        return ReadSpeed() * _move.y;
    }

    private float ReadSpeed()
    {
        if (ReadAction(PlayerState.Run)) return _speeds[(int)PlayerState.Run];
        if (ReadAction(PlayerState.Duck)) return _speeds[(int)PlayerState.Duck];
        return _speeds[(int)PlayerState.Walk];
    }

    private Vector3 Forward()
    {
        return transform.TransformDirection(Vector3.forward);
    }

    private void Action(InputAction.CallbackContext context)
    {
        Debug.Log("Action");
    }

    private void Duck(InputAction.CallbackContext context)
    {
        Debug.Log("Duck");


    }

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
    }

    private void Lamp(InputAction.CallbackContext context)
    {
        Debug.Log("Lamp");
    }

    private void Run(InputAction.CallbackContext context)
    {
        Debug.Log("Run");
        //_animator.SetBool("Run", true);
    }

    private void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Pause");
    }

    private void Inventary(InputAction.CallbackContext context)
    {
        Debug.Log("Inventary");
    }

    private void Map(InputAction.CallbackContext context)
    {
        Debug.Log("Map");
    }


    private void OnEnable()
    {
        _inputSystem.Player.Action.Enable();
        _inputSystem.Player.Duck.Enable();
        _inputSystem.Player.Jump.Enable();
        _inputSystem.Player.Lamp.Enable();
        _inputSystem.Player.Move.Enable();
        _inputSystem.Player.Run.Enable();
        _inputSystem.UI.Pause.Enable();
        _inputSystem.UI.Inventary.Enable();
        _inputSystem.UI.Map.Enable();
    }

    private void OnDisable()
    {
        _inputSystem.Player.Action.Disable();
        _inputSystem.Player.Duck.Disable();
        _inputSystem.Player.Jump.Disable();
        _inputSystem.Player.Lamp.Disable();
        _inputSystem.Player.Move.Disable();
        _inputSystem.Player.Run.Disable();
        _inputSystem.UI.Pause.Disable();
        _inputSystem.UI.Inventary.Disable();
        _inputSystem.UI.Map.Disable();
    }
}

enum PlayerState
{
    Walk,
    Run,
    Duck
}

*/