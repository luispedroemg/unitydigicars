using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class CarAgent : Agent, ICarController
{
    private float _throttle;
    private float _brake;
    private float _steerAngle;
    private bool _handbrake;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        base.CollectObservations(sensor);
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        base.OnActionReceived(vectorAction);
    }

    public override void OnEpisodeBegin()
    {
        base.OnEpisodeBegin();
    }

    public float Throttle()
    {
        return _throttle;
    }

    public float Brake()
    {
        return _brake;
    }

    public float SteerAngle()
    {
        return _steerAngle;
    }

    public bool HandBrake()
    {
        return _handbrake;
    }
}
