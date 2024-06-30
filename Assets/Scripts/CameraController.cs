using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField][Range(0, 1)] private float _lerpValue;
    [SerializeField] private Vector3 _offset = new Vector3(0f, -1.5f, 0f);
    [SerializeField] private float _cameraHeight = 4f;
    private Vector2 _cameraRotation = Vector2.zero;
    private float _cameraZoom = 1.0f;
    private int _maxCameraZoom = -3;
    private int _minCameraZoom = 0;
    private Vector2 _relativeDirection;
    private Vector3 _initialPosition;
    private Vector3 _cameraPosition = Vector3.zero;
    private Vector3 _amount = Vector3.zero;
    private float _sensitivity = 10.0f;
    private Vector3 _mouseDelta = Vector3.zero;

    private InputControls _input;

    private void Awake()
    {
        _input = new InputControls();
        _target = GameObject.Find("Noah").transform;
        UpdateCamera(transform, out _initialPosition);
    }

    private void Start()
    {
        InitializeInput();
    }

    private void InitializeInput()
    {
        //_input.Camera.View.performed += ctx => { OnRotate(ctx); };
        _input.Camera.Zoom.performed += ctx => { OnZoom(ctx); };
    }

    private void OnRotate(InputAction.CallbackContext ctx)
    {
        _cameraRotation = ctx.ReadValue<Vector2>();
    }

    private void OnZoom(InputAction.CallbackContext ctx)
    {

        if (ctx.ReadValue<Vector2>().y > 0)
        {
            if (_offset.z <= _maxCameraZoom && _offset.z >= _minCameraZoom) _offset.z -= 0.1f;
        }
        else
        {
            if (_offset.z >= _maxCameraZoom && _offset.z <= _minCameraZoom) _offset.z -= -0.1f;
        }

    }

    private void LateUpdate()
    {
        UpdateCamera(ref _cameraPosition);
        transform.position = _cameraPosition;
        transform.LookAt(_target.transform.position - _offset);
    }

    private void UpdateCamera(ref Vector3 camera)
    {
        camera.x = _initialPosition.x + _target.position.x + _offset.x;
        camera.z = _initialPosition.z + _target.position.z + _offset.z;
        camera.y = _offset.y + _cameraHeight;
    }

    private void UpdateCamera(in Transform camera, out Vector3 initialPosition)
    {
        initialPosition = camera.position;
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