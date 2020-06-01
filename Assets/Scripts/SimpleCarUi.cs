using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SimpleCarUi : AbstractCarUI
{
    public Text speedText;
    [FormerlySerializedAs("needle")] public Image speedNeedle;
    public Image rpmNeedle;
    
    [FormerlySerializedAs("needleZeroAngle")] public float speedNeedleZeroAngle = 120;
    public float rpmNeedleZeroAngle = 120;
    public override void SetSpeed(float speed)
    {
        var textSpeed = ((speed * 3600) / 1000) *1.2f;
        speedText.text = textSpeed - textSpeed%1 + " Km/h";
    }

    public override void SetSpeed(float speed, float topSpeed)
    {
        SetSpeed(speed);
        speedNeedle.rectTransform.rotation = Quaternion.Euler(new Vector3(0,0, speedNeedleZeroAngle - speed * (speedNeedleZeroAngle*2/topSpeed)));
    }

    public override void SetRpm(float rpm, float topRpm)
    {
        rpmNeedle.rectTransform.rotation = Quaternion.Euler(new Vector3(0,0, speedNeedleZeroAngle - rpm * (speedNeedleZeroAngle*2/topRpm)));
    }
}
