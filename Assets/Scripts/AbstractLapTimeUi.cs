using UnityEngine;

public abstract class AbstractLapTimeUi : MonoBehaviour
{
    public abstract void SetCurrentLapTime(float time);
    public abstract void SetBestLapTime(float time);
    public abstract void SetLastLapTime(float time);
}