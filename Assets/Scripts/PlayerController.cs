using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputControls _input;
    private CharacterController _characterController;

    [SerializeField]
    private float _rotateSpeed = 5.0f;
    [SerializeField]
    private float _duckingSpeed = 3.0f;
    [SerializeField]
    private float _walkingSpeed = 5.0f;
    [SerializeField]
    private float _runningSpeed = 7.0f;

    private float _currentSpeed = 0.0f;
    private Vector3 _move = Vector3.zero;
    private Vector3 _chRotation = Vector3.zero;

    private bool _isCrouched = false;
    private bool _isRunning = false;
    private bool _isJumping = false;

    public void Awake()
    {
        _input = _input ?? new InputControls();
        _characterController = GetComponent<CharacterController>();

        //Player
        _input.Player.Action.performed += ctx => OnAction(ctx);
        _input.Player.Duck.performed += ctx => _isCrouched = !_isCrouched;
        _input.Player.Jump.canceled += ctx => _isJumping = false;
        _input.Player.Jump.performed += ctx => OnJump(ctx);
        _input.Player.Move.canceled += ctx => _move = Vector3.zero;
        _input.Player.Move.performed += ctx => OnMove(ref _move, _input.Player.Move.ReadValue<Vector2>());
        _input.Player.Run.canceled += ctx => _isRunning = false;
        _input.Player.Run.performed += ctx => _isRunning = true;
        //UI
        _input.UI.Inventary.performed += ctx => Inventary(ctx);
        _input.UI.Map.performed += ctx => Map(ctx);
        _input.UI.Pause.performed += ctx => Pause(ctx);
    }

    void Update()
    {
        //Debug.Log(_move);
        //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _move.z < 0 ? -transform.localScale.z : transform.localScale.z);
        //if (_move.z > 0) transform.LookAt();
        //if (_move.z < 0) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,  -transform.localScale.z);

        //_isJumping = false;
        if (_move.magnitude >= 0.1f)
        {
            transform.rotation = Quaternion.Euler(0,
             Mathf.Atan2(_move.x, _move.z) * Mathf.Rad2Deg,
            0);

            if (_move.z < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (_move.z > 0)
                transform.localScale = new Vector3(1, 1, 1);
        }

        if (_isCrouched) _currentSpeed = _duckingSpeed;
        else _currentSpeed = _isRunning ? _runningSpeed : _walkingSpeed;
        _characterController.SimpleMove(_move.normalized * _currentSpeed);
    }

    private void OnMove(ref Vector3 move, Vector2 input)
    {
        move.x = input.x;
        move.z = input.y;
    }

    private void OnAction(InputAction.CallbackContext context)
    {
        //TODO: falta implementar Action
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        //TODO: falta implementar Jump
    }

    private void Pause(InputAction.CallbackContext context)
    {
        //TODO: falta implementar Pause
    }

    private void Inventary(InputAction.CallbackContext context)
    {
        //TODO: falta implementar Inventary
    }

    private void Map(InputAction.CallbackContext context)
    {
        //TODO: falta implementar Map
    }

    private void OnEnable()
    {
        _input.Player.Action.Enable();
        _input.Player.Duck.Enable();
        _input.Player.Jump.Enable();
        _input.Player.Lamp.Enable();
        _input.Player.Move.Enable();
        _input.Player.Run.Enable();
        _input.UI.Pause.Enable();
        _input.UI.Inventary.Enable();
        _input.UI.Map.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Action.Disable();
        _input.Player.Duck.Disable();
        _input.Player.Jump.Disable();
        _input.Player.Lamp.Disable();
        _input.Player.Move.Disable();
        _input.Player.Run.Disable();
        _input.UI.Pause.Disable();
        _input.UI.Inventary.Disable();
        _input.UI.Map.Disable();
    }
}