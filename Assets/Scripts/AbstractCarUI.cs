using UnityEngine;

public abstract class AbstractCarUI : MonoBehaviour
{
    public abstract void SetSpeed(float speed);
    public abstract void SetSpeed(float speed, float topSpeed);

    public abstract void SetRpm(float rpm, float topRpm);
}