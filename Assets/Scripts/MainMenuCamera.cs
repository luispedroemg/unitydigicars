using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    private MSCameraController _controller;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<MSCameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        _controller._horizontalInputMSACC = 1;
    }
}
