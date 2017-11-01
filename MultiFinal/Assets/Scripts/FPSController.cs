
using UnityEngine;
using UnityEngine.Networking;

public class FPSController : NetworkBehaviour

{
    public Camera _camera;
    public float _speed;
    public float _mouseSens;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private Vector3 _cameraRotation = Vector3.zero;
    private Rigidbody _rgb;

    private void Start()
    {
        _rgb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 _horizontal = transform.right * x;
        Vector3 _vertical = transform.forward * z;

        _velocity = (_horizontal + _vertical).normalized * _speed;

        Move();

        float yR = Input.GetAxisRaw("Mouse X");
         _rotation = new Vector3(0f, yR , 0f) * _mouseSens;
        Rotate();

        float xR = Input.GetAxisRaw("Mouse Y");
        _cameraRotation = new Vector3(xR, 0f, 0f) * _mouseSens;

        Rotate();

    }

    public override void OnStartLocalPlayer()
    {
       GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
    }

    void Rotate()
    {
        _rgb.MoveRotation(_rgb.rotation * Quaternion.Euler(_rotation));
        if (_camera != null)
        {
            _camera.transform.Rotate(-_cameraRotation);
        }
    }
    

    void Move()
    {
        if (_velocity != Vector3.zero)
        {
            _rgb.MovePosition(_rgb.position + _velocity * Time.fixedDeltaTime);
        }
    }
}
