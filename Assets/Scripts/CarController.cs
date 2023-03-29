using UnityEngine;
using System;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    public Joystic joystic;
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public ParticleSystem smokeParticle;
        public Axel axel;
    }

    public ControlMode control;

    public float maxAcceleration = 5.0f;
    public float brakeAcceleration = 8.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public Transform _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;
    [HideInInspector]
    public Rigidbody carRb;
    

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass.localPosition;
    }

    void Update()
    {
        //Debug.Log(carRb.velocity.magnitude);
        GetInputs();
        AnimateWheels();
        WheelEffects();
    }
    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
        
    }
    public void MoveInput(float input)
    {
        moveInput = input;
    }
    public void SteerInput(float input)
    {
        steerInput = input;
    }
    void GetInputs()
    {
        if(control == ControlMode.Keyboard)
        {
            moveInput = joystic.curentJoysticPosX;
            steerInput = joystic.curentJoysticPosZ;
        }
    }

    void Move()
    {
        if (moveInput != 0)
        {
            carRb.velocity = gameObject.transform.forward * moveInput * 10;
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = moveInput * maxAcceleration * Time.deltaTime;
            }
        }
    }

    void Steer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = brakeAcceleration * Time.deltaTime;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    void AnimateWheels()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }

    void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            //var dirtParticleMainSettings = wheel.smokeParticle.main;

            if (wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.velocity.magnitude >= 5.0f)
            {
                wheel.wheelEffectObj.GetComponent<TrailRenderer>().emitting = true;
                wheel.smokeParticle.Emit(1);
            }
            else
            {
                wheel.wheelEffectObj.GetComponent<TrailRenderer>().emitting = false;
                wheel.smokeParticle.Clear();
            }
        }
    }
}
