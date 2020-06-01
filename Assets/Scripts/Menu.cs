using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public Slider loadBarSlider;
    public TextMeshPro loadProgressText;
    // Start is called before the first frame update

    public void SetLoadProgress(float progress)
    {
        loadBarSlider.value = progress;
        loadProgressText.text = "Loading " + progress * 100 + "%";
    }
}
