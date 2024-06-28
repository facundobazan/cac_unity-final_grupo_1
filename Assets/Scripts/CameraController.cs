using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    [Range(0, 1)]
    private float _lerpValue;
    [SerializeField]
    private Vector3 _offset = Vector3.zero;
    private Vector2 _cameraRotation = Vector2.zero;
    private float _cameraZoom = 0.0f;
    private int _maxCameraZoom = 5;
    private int _minCameraZoom = 0;
    private Vector2 _relativeDirection;
    private Vector3 _cameraPosition = Vector3.zero;
    private Vector3 _amount = Vector3.zero;
    private float _sensitivity = 10.0f;
    private Vector3 _mouseDelta = Vector3.zero;

    private InputControls _input;

    private void Awake()
    {
        _input = new InputControls();
        _target = GameObject.Find("Noah").transform;
    }

    private void Start()
    {
        InitializeInput();
    }

    private void InitializeInput()
    {
        _input.Camera.View.performed += ctx => { OnRotate(ctx); };
        _input.Camera.Zoom.performed += ctx => { OnZoom(ctx); };
    }

    private void OnRotate(InputAction.CallbackContext ctx)
    {
        _cameraRotation = ctx.ReadValue<Vector2>();
    }

    private void OnZoom(InputAction.CallbackContext ctx)
    {
        _cameraZoom = ctx.ReadValue<float>() > 0 ? 1 : -1;

        if (_cameraZoom > _maxCameraZoom) _cameraZoom = _maxCameraZoom;
        else if (_cameraZoom < 0) _cameraZoom = _minCameraZoom;
    }

    private void Update()
    { }

    private void LateUpdate()
    {
        //transform.position = Vector3.Lerp(transform.position, _target.position + _offset, _lerpValue);
        //transform.LookAt(_target.position);

        _mouseDelta.Set(_cameraRotation.x, _cameraRotation.y, 0f);
        _amount += _mouseDelta * _sensitivity;
        _amount.z = Mathf.Clamp(_amount.z, 50, 300);
        _amount.y = Mathf.Clamp(_amount.y, -89, 89);

        _cameraPosition = Quaternion.AngleAxis(_amount.x, Vector3.up) * Quaternion.AngleAxis(_amount.y, Vector3.right) * Vector3.forward;
        _cameraPosition *= _amount.z * 0.1f;
        _cameraPosition += _target.transform.position;
        transform.position = _cameraPosition;
        transform.LookAt(_target.transform.position);
    }

    private void OnEnable()
    {
        _input.Camera.View.Enable();
        _input.Camera.Zoom.Enable();
    }

    private void OnDisable()
    {
        _input.Camera.View.Disable();
        _input.Camera.Zoom.Disable();
    }
}

/*
Para obtener un componente del mismo objeto transform, debe ser creada la variable antes y obtener la instancia en el start
ej:
public BoxCollider collider;

void Start(){collider = GetComponent<BoxCollider>();}
*/