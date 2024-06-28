using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationStateController : MonoBehaviour
{
    private Animator _animator;
    private InputControls _input;
    private GameObject _phone;

    private Vector2 _move = Vector2.zero;
    private float _currentMaxSpeed = 0.0f;
    [SerializeField] private float _maxWalkingSpeed = 1.0f;
    [SerializeField] private float _maxRunningSpeed = 1.5f;
    private bool _isDucking = false;
    private bool _illuminate = false;

    void Awake()
    {
        _animator = _animator ?? GetComponent<Animator>();
        _animator.applyRootMotion = false;
        _input = _input ?? new InputControls();
        _phone = GameObject.Find("Phone");

        InitializeControls();
    }

    void Start()
    {
        _phone.SetActive(false);
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
        _input.Player.Action.performed += ctx => { OnAction(ctx); };

        _input.Player.Duck.performed += ctx => _isDucking = !_isDucking;

        _input.Player.Jump.performed += ctx => { OnJump(ctx); };

        _input.Player.Lamp.performed += ctx => { OnLamp(ctx); };

        _input.Player.Move.canceled += ctx => _move = Vector2.zero;
        _input.Player.Move.performed += ctx => _move = _input.Player.Move.ReadValue<Vector2>();

        _input.Player.Run.canceled += ctx => _currentMaxSpeed = _maxWalkingSpeed;
        _input.Player.Run.performed += ctx => _currentMaxSpeed = _maxRunningSpeed;
    }

    private void OnLamp(InputAction.CallbackContext ctx)
    {
        _illuminate = !_illuminate;
        _phone.SetActive(_illuminate);
    }

    private void OnEnable()
    {
        _input.Player.Action.Enable();
        _input.Player.Duck.Enable();
        _input.Player.Jump.Enable();
        _input.Player.Lamp.Enable();
        _input.Player.Move.Enable();
        _input.Player.Run.Enable();
        /*_input.UI.Pause.Enable();
        _input.UI.Inventary.Enable();
        _input.UI.Map.Enable();
        _input.Camera.View.Enable();
        _input.Camera.Zoom.Enable();*/
    }

    private void OnDisable()
    {
        _input.Player.Action.Disable();
        _input.Player.Duck.Disable();
        _input.Player.Jump.Disable();
        _input.Player.Lamp.Disable();
        _input.Player.Move.Disable();
        _input.Player.Run.Disable();
        /*_input.UI.Pause.Disable();
        _input.UI.Inventary.Disable();
        _input.UI.Map.Disable();
        _input.Camera.View.Disable();
        _input.Camera.Zoom.Disable();*/
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
