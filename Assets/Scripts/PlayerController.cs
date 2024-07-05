using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerController : MonoBehaviour
{
    private InputControls _input;
    private CharacterController _characterController;
    private Animator _animator;
    [SerializeField] private GameObject _phone;

    [SerializeField] private float _duckingSpeed = 3.0f;
    [SerializeField] private float _walkingSpeed = 5.0f;
    [SerializeField] private float _runningSpeed = 7.0f;
    [SerializeField] private float _rotationSpeed = 50f;
    [SerializeField] private float _jumpForce = 0.2f;
    [SerializeField] private float _gravityValue = -30f;

    private float _currentSpeed = 0.0f;
    private Vector3 _move = Vector3.zero;
    private Vector2 _mousePosition = Vector2.zero;

    private bool _isCrouched = false;
    private bool _isRunning = false;
    private bool _isJumping = false;
    private bool _groundedPlayer = false;


    private float _currentAngle = 0.0f;
    private float _currentAngleVelocity;

    public void Awake()
    {
        _input = _input ?? new InputControls();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _phone.SetActive(false);

        //Player
        _input.Player.Action.performed += ctx => OnAction(ctx);
        _input.Player.Duck.performed += ctx => _isCrouched = !_isCrouched;
        _input.Player.Jump.canceled += ctx => OnJumpCanceled(ctx);
        _input.Player.Jump.performed += ctx => OnJump(ctx);
        _input.Player.Move.canceled += ctx => _move = Vector3.zero;
        _input.Player.Move.performed += ctx => OnMove(ref _move, _input.Player.Move.ReadValue<Vector2>());
        _input.Player.Run.canceled += ctx => _isRunning = false;
        _input.Player.Run.performed += ctx => _isRunning = true;
        _input.Player.Lamp.performed += ctx => _phone.SetActive(!_phone.activeSelf);
        //UI
        _input.UI.Inventary.performed += ctx => Inventary(ctx);
        _input.UI.Map.performed += ctx => Map(ctx);
        _input.UI.Pause.performed += ctx => Pause(ctx);
        //Camera
        _input.Camera.View.canceled += ctx => _mousePosition = Vector2.zero;
        _input.Camera.View.performed += ctx => _mousePosition = _input.Camera.View.ReadValue<Vector2>();
    }

    void Update()
    {
        //Debug.Log(_characterController.isGrounded);
        //transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _move.z < 0 ? -transform.localScale.z : transform.localScale.z);
        //if (_move.z > 0) transform.LookAt();
        //if (_move.z < 0) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,  -transform.localScale.z);

        //_isJumping = false;
        _groundedPlayer = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, _characterController.height / 2 + 0.1f);

        if (_groundedPlayer && _move.y < 0)
        {
            _move.y = 0f;
        }

        if (_isCrouched) _currentSpeed = _duckingSpeed;
        else _currentSpeed = _isRunning ? _runningSpeed : _walkingSpeed;

        if (_mousePosition != Vector2.zero)
        {
            transform.Rotate(0, _mousePosition.x * _rotationSpeed * Time.deltaTime, 0);
            //_characterController.Move(transform.TransformDirection() * _rotationSpeed * Time.deltaTime);
        }

        if (_move.magnitude >= 0.1f)
        {
            /*transform.rotation = Quaternion.Euler(0,
             Mathf.Atan2(_move.x, _move.z) * Mathf.Rad2Deg,
            0);

            if (_move.z < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (_move.z > 0)
                transform.localScale = new Vector3(1, 1, 1);*/

            _characterController.Move(_currentSpeed * Time.deltaTime * transform.TransformDirection(_move));






            /*float targetAngle = Mathf.Atan2(_move.x, _move.z) * Mathf.Rad2Deg;// + _camera.transform.eulerAngles.y;
            _currentAngle = Mathf.SmoothDampAngle(_currentAngle,
            targetAngle, ref _currentAngleVelocity, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0, _currentAngle, 0);
            Vector3 rotatedMovement = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;*/

            //Vector3 moveDirection = transform.TransformDirection(_move);

        }

        _move.y += _gravityValue * Time.deltaTime;
        _characterController.Move(transform.TransformDirection(_move) * Time.deltaTime);
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
        if (_groundedPlayer)
        {
            _move.y = Mathf.Sqrt(_jumpForce * -2.0f * _gravityValue);
            _animator.SetBool("IsGrounded", true);
            _animator.SetBool("Jump", true);
        }
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        _isJumping = false;
        _animator.SetBool("IsGrounded", false);
        _animator.SetBool("Jump", false);
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
        _input.Camera.View.Enable();
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
        _input.Camera.View.Disable();
    }
}