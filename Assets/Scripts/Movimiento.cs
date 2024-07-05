using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    private InputControls _input;
    private CharacterController _controller;

    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 5f;
    [SerializeField] private float gravityValue = -30f;//-9.81f;

    private Vector3 _move = Vector3.zero;
    private Vector2 _mousePosition = Vector2.zero;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool _jumping = false;

    private void Awake()
    {
        _input = new InputControls();

        _input.Player.Move.canceled += ctx => _move = Vector2.zero;
        _input.Player.Move.performed += ctx => ReadVector3(ref _move, _input.Player.Move.ReadValue<Vector2>());
        _input.Player.Jump.canceled += ctx => _jumping = false;
        _input.Player.Jump.performed += ctx => _jumping = true;

        _input.Camera.View.canceled += ctx => _mousePosition = Vector2.zero;
        _input.Camera.View.performed += ctx => _mousePosition = _input.Camera.View.ReadValue<Vector2>();
    }

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void ReadVector3(ref Vector3 vector, Vector2 input)
    {
        vector.x = input.x;
        vector.y = Vector2.zero.y;
        vector.z = input.y;
    }

    void Update()
    {
        groundedPlayer = _controller.isGrounded;
        Debug.Log(_controller.isGrounded);

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        if (_mousePosition != Vector2.zero)
        {
            transform.Rotate(0, _mousePosition.x * _rotationSpeed * Time.deltaTime, 0);
        }

        if (_move != Vector3.zero)
        {
            Vector3 moveDirection = transform.TransformDirection(_move);
            _controller.Move(moveDirection * Time.deltaTime * playerSpeed);
        }

        if (_jumping && groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        _controller.Move(playerVelocity * Time.deltaTime);
    }

    private void OnEnable()
    {
        _input.Player.Move.Enable();
        _input.Player.Jump.Enable();
        _input.Camera.View.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Move.Disable();
        _input.Player.Jump.Disable();
        _input.Camera.View.Disable();
    }
}
