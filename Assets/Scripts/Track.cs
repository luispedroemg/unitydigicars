using UnityEngine;

public class Track : ITerrainFeature
{
    public GameObject lapTimeUiObject;

    public Material trailMaterial;
    public ParticleSystem trailParticleSystem;
    public AudioClip trailAudio;
    
    private AbstractLapTimeUi _abstractLapTimeUi;
    private Checkpoint[] _checkpoints;
    
    public int CurrentSequence => _currentSequence;
    private int _currentSequence;
    public float LapTime => _lapTime;
    public float BestLap => _bestLap;
    public float LastLap => _lastLap;
    private float _lapTime;
    private float _bestLap = Mathf.Infinity;
    private float _lastLap;

    void Start()
    {
        TerrainFeatureName = gameObject.name;
        _checkpoints = GetComponentsInChildren<Checkpoint>();
        foreach (Checkpoint checkpoint in _checkpoints)
        {
            checkpoint.SetTriggerCallback(CheckpointTrigger);
        }
        _abstractLapTimeUi = lapTimeUiObject.GetComponent<AbstractLapTimeUi>();
    }
    
    void Update()
    {
        _lapTime += Time.deltaTime;
        _abstractLapTimeUi.SetCurrentLapTime(_lapTime);
    }

    private void CheckpointTrigger(Checkpoint checkpoint)
    {
        if (_currentSequence == checkpoint.checkpointIndex)
            _currentSequence++;
        if (_currentSequence == _checkpoints.Length)
        {
            _lastLap = _lapTime;
            _abstractLapTimeUi.SetLastLapTime(_lastLap);
            if (_lastLap < _bestLap)
            {
                _bestLap = _lastLap;
                _abstractLapTimeUi.SetBestLapTime(_lastLap);
            }
            _lapTime = 0;
            _currentSequence = 0;
        }
    }

    public override Material GetTrailMaterial()
    {
        return trailMaterial;
    }

    public override ParticleSystem GetTrailParticleEffect()
    {
        return trailParticleSystem;
    }

    public override AudioClip GetTrailAudioClip()
    {
        return trailAudio;
    }
}
 