using UnityEngine;

class LocalCarController : AbstractCarController
{
    public override float Throttle()
    {
        return Mathf.Clamp(Input.GetAxisRaw("Vertical"), 0, 1);
    }

    public override float Brake()
    {
        return Mathf.Clamp(Input.GetAxisRaw("Vertical"), -1, 0) * -1;
    }

    public override float SteerAngle()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool HandBrake()
    {
        return Input.GetAxisRaw("Handbrake") > 0.1f;
    }

    public override bool FlipCar()
    {
        return Input.GetAxisRaw("FlipCar") > 0.1f;
    }
}