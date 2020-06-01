using UnityEngine;

public interface ICarController
{
    float Throttle();
    float Brake();
    float SteerAngle();
    bool HandBrake();
}

public abstract class AbstractCarController : MonoBehaviour, ICarController
{
    public abstract float Throttle();
    public abstract float Brake();
    public abstract float SteerAngle();
    public abstract bool HandBrake();
    public abstract bool FlipCar();
}
