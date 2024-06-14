using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationStateController : MonoBehaviour
{
    private Animator _animator;
    private InputControls _inputSystem;

    private Vector2 _move = Vector2.zero;
    private float _currentMaxSpeed = 0.0f;
    [SerializeField] private float _maxWalkingSpeed = 1.0f;
    [SerializeField] private float _maxRunningSpeed = 1.5f;
    private bool _isDucking = false;
    private bool _illuminate = false;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _inputSystem = new InputControls();

        InitializeControls();
    }

    void Start()
    {
        _currentMaxSpeed = _maxWalkingSpeed;
    }

    // Update is called once per frame
    void Update()
    {


        _animator.SetFloat("VecX", _move.x * _currentMaxSpeed);
        _animator.SetFloat("VecZ", _move.y * _currentMaxSpeed);
        _animator.SetBool("Duck", _isDucking);
        _animator.SetBool("Illuminate", _illuminate);
    }

    private void InitializeControls()
    {
        // Player
        _inputSystem.Player.Action.performed += ctx => { OnAction(ctx); };

        _inputSystem.Player.Duck.performed += ctx => _isDucking = !_isDucking;

        _inputSystem.Player.Jump.performed += ctx => { OnJump(ctx); };

        _inputSystem.Player.Lamp.performed += ctx => _illuminate = !_illuminate;

        _inputSystem.Player.Move.canceled += ctx => _move = Vector2.zero;
        _inputSystem.Player.Move.performed += ctx => _move = _inputSystem.Player.Move.ReadValue<Vector2>();

        _inputSystem.Player.Run.canceled += ctx => _currentMaxSpeed = _maxWalkingSpeed;
        _inputSystem.Player.Run.performed += ctx => _currentMaxSpeed = _maxRunningSpeed;
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

    private void OnAction(InputAction.CallbackContext context)
    {
        _animator.Play("Action");
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _animator.Play("Jump");
    }
}
