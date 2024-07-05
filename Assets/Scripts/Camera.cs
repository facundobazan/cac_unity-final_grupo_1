using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _rotationSpeed = 100f;

    private InputControls _input;

    private bool _rotate = false;
    private Vector2 _mousePosition = Vector2.zero;
    private float _targetRotation = 0f;

    private void Awake()
    {
        _input = new InputControls();

        if (_target == null) Debug.Log("Objetivo no encontrado.");

        //_input.Camera.Rotate.canceled += ctx => _rotate = false;
        //_input.Camera.Rotate.performed += ctx => _rotate = true;
        _input.Camera.View.canceled += ctx => _mousePosition = Vector2.zero;
        _input.Camera.View.performed += ctx => _mousePosition = _input.Camera.View.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        _input.Camera.Rotate.Enable();
        _input.Camera.View.Enable();
    }

    private void OnDisable()
    {
        _input.Camera.Rotate.Disable();
        _input.Camera.View.Disable();
    }

    private void Update()
    {
        //Debug.DrawRay(transform.position, _target.position);

        if (_mousePosition != Vector2.zero)
        {
            //transform.position = _target.position - transform.position;
            //transform.RotateAround(_target.localPosition, _view, 100 * Time.deltaTime);
            //if (_view.y > 1)
            _targetRotation = _mousePosition.x * _rotationSpeed * Time.deltaTime;
            _target.Rotate(0, _targetRotation, 0);
            /*Quaternion rotation = Quaternion.Euler(new Vector3(_view.y, _view.x, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);*/
        }
    }
}
