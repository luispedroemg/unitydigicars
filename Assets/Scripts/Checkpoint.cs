using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointIndex;
    
    private Action<Checkpoint> _triggerCallback;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Car")
            _triggerCallback(this);
    }

    public void SetTriggerCallback(Action<Checkpoint> callback)
    {
        _triggerCallback = callback;
    }
}
