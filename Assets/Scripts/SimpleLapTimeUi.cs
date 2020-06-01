using System;
using UnityEngine;
using UnityEngine.UI;

public class SimpleLapTimeUi : AbstractLapTimeUi
{
    public Text lapTime;
    public Text bestLapTime;
    public Text lastLapTime;
    
    public override void SetCurrentLapTime(float time)
    {
        lapTime.text = parseTime(time);
    }

    public override void SetBestLapTime(float time)
    {
        bestLapTime.text = parseTime(time);
    }

    public override void SetLastLapTime(float time)
    {
        lastLapTime.text = parseTime(time);
    }

    private String parseTime(float time)
    {
        return Mathf.Floor(time / 60) + ":" + Mathf.Floor(time%60) + ":" + Mathf.Floor((time*100)%100);
    }
}
