using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Camera _camera = null;

    void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    void Start()
    {
        Debug.Log(transform.position);
    }

    void Update()
    {

    }
}

/*
Para obtener un componente del mismo objeto transform, debe ser creada la variable antes y obtener la instancia en el start
ej:
public BoxCollider collider;

void Start(){collider = GetComponent<BoxCollider>();}
*/