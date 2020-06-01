using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float motorTorque = 100f;
    public float brakeTorque = 100f;
    public float handBrakeTorque = 1000f;
    public float maxSpeed = 10f;
    public float maxWheelRpm = 2000f;
    public float maxSteerAngle = 45f;
    public float steerRate = 1f;
    public float enginePitchMultiplier = 0.5f;
    public bool autoReverse = true;
    public GameObject centerOfMass;
    public GameObject carUiGameObject;

    private AbstractCarController _carController;

    private IEnumerable<Wheel> _wheels;
    private Rigidbody _rigidbody;
    private AudioSource _carSound;
    private AbstractCarUI _carUi;
    private int _numPowerWheels;
    private int _numBreakingWheels;

    public float EngineRpm => _engineRpm;
    private float _engineRpm;

    public bool IsGrounded => _isGrounded;
    private bool _isGrounded;
    
    void Start()
    {
        _wheels = GetComponentsInChildren<Wheel>();
        foreach (Wheel wheel in _wheels)
        {
            wheel.MaxWheelRpm = maxWheelRpm;
            if (wheel.power)
            {
                _numPowerWheels++;
            }
            if (wheel.braking)
            {
                _numBreakingWheels++;
            }
        }
        GetComponent<Rigidbody>().centerOfMass = centerOfMass.transform.localPosition;
        SetController(gameObject.AddComponent<LocalCarController>());
        _rigidbody = GetComponent<Rigidbody>();
        _carSound = GetComponent<AudioSource>();
        _carUi = carUiGameObject.GetComponent<AbstractCarUI>();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(new Vector3(0,  - _rigidbody.velocity.magnitude * 200f, 0));
        if (!_isGrounded)
        {
            _rigidbody.AddForce(new Vector3(0, -8000f, 0));
        }
    }

    void Update()
    {
        float brake = _carController.Brake() * brakeTorque;
        float power = _carController.Throttle() * (motorTorque/_numPowerWheels);
        bool handbraking = _carController.HandBrake();

        if (transform.InverseTransformDirection(_rigidbody.velocity).z < 0.001f && _carController.Brake() > 0f && autoReverse)
        {
            power = -1 * _carController.Brake() * (motorTorque/_numBreakingWheels);
            brake = 0;
        }

        _engineRpm = 0;
        _isGrounded = false;
        foreach (Wheel wheel in _wheels)
        {
            if (_rigidbody.velocity.magnitude >= maxSpeed){
                wheel.Torque = 0;
            }else{
                wheel.Torque = power;
            }
            wheel.SteerAngle = Mathf.Lerp(wheel.SteerAngle,_carController.SteerAngle() * maxSteerAngle, steerRate * Time.deltaTime);;
            wheel.BrakeTorque = brake;
            wheel.HandBrakeTorque = handbraking? handBrakeTorque : 0;
            _engineRpm += wheel.power?Mathf.Abs(wheel.wheelCollider.rpm):0;
            _isGrounded |= wheel.IsGrounded;
        }
        _engineRpm /= _numPowerWheels;
        var soundPitch = Mathf.Clamp(_engineRpm * enginePitchMultiplier, 0.5f, 10f);
        _carSound.pitch = soundPitch;
        _carUi.SetSpeed(_rigidbody.velocity.magnitude, maxSpeed);
        _carUi.SetRpm(_engineRpm,maxWheelRpm);
        ResetFlip();
    }

    // TODO Improve flipping detection
    public bool IsFlipped()
    {
        return Mathf.Abs(transform.localEulerAngles.z) >= 50f && !_isGrounded;
    }

    public void ResetFlip()
    {
        if (IsFlipped() && _carController.FlipCar())
        {
            Transform currentTransform = transform;
            Quaternion uprightRotation = new Quaternion{eulerAngles = new Vector3(0, currentTransform.rotation.eulerAngles.y, 0)};
            currentTransform.rotation = uprightRotation;
        }
    }

    public void SetController(AbstractCarController controller)
    {
        _carController = controller;
    }
}
