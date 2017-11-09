
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FPSController : NetworkBehaviour

{
    public Camera _camera;
    public float _speed;
    public float _mouseSens;

    private Vector3 _velocity = Vector3.zero;
    private Vector3 _rotation = Vector3.zero;
    private Vector3 _cameraRotation = Vector3.zero;
    private Vector3 _thrusterForce = Vector3.zero;

    private Rigidbody _rgb;
    public float _thrusterPower;
    private float _thrusterFuel;
    public float _maxFuel;

    public float _decreaseFuel;
    public float _increaseFuel;

    public Slider _fuelSlider;

    private void Start()
    {
        _rgb = GetComponent<Rigidbody>();        
        _thrusterFuel = _maxFuel;
    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _fuelSlider.value = _thrusterFuel / _maxFuel;
        if (_thrusterFuel < _maxFuel)
        {
            _thrusterFuel += _increaseFuel * Time.deltaTime;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 _horizontal = transform.right * x;
        Vector3 _vertical = transform.forward * z;

        _velocity = (_horizontal + _vertical).normalized * _speed;

        if (Input.GetButton("Jump") && _thrusterFuel > 0)
        {
            Vector3 up = new Vector3(0, 1f, 0);
            _thrusterForce = up * _thrusterPower;
            _thrusterFuel -= _decreaseFuel * Time.deltaTime;
        }
        else
        {
            _thrusterForce = Vector3.zero;
        }


        Move();

        float yR = Input.GetAxisRaw("Mouse X");
         _rotation = new Vector3(0f, yR , 0f) * _mouseSens * Time.deltaTime;
        Rotate();

        float xR = Input.GetAxisRaw("Mouse Y");
        _cameraRotation = new Vector3(xR, 0f, 0f) * _mouseSens * Time.deltaTime;

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

        if (_thrusterForce != Vector3.zero)
        {
            _rgb.AddForce(_thrusterForce, ForceMode.Acceleration);
        }
    }
}
