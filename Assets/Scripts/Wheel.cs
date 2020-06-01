using UnityEngine;

public class Wheel : MonoBehaviour
{
    public bool steer;
    public bool power;
    public bool braking;
    public bool isHandBrake;
    public float smokeEffectThreshold = 0.5f;
    public float trailEffectThreshold = 0.4f;
    public float screechSoundThreshold = 0.5f;
    
    public float MaxWheelRpm{ get; set; }
    public float HandBrakeTorque { get; set; }
    public float Torque { get; set; }
    public float SteerAngle { get; set; }
    public float BrakeTorque { get; set; }
    
    public WheelCollider wheelCollider;
    public ParticleSystem trailParticleEffect;
    public TrailRenderer trailEffect;

    private Transform _wheelTransform;
    private AudioSource _skidSound;
    private ITerrainFeature _currentTerrainFeature;
    private TrailParticleEffect _trailParticleEffectBehaviour;
    public bool IsGrounded => _isGrounded;
    private bool _isGrounded;
    

    private void Start()
    {
        _currentTerrainFeature = GameObject.Find("Track").GetComponent<ITerrainFeature>();
        _wheelTransform = GetComponentInChildren<MeshRenderer>().transform;
        _skidSound = GetComponent<AudioSource>();
        _trailParticleEffectBehaviour = trailParticleEffect.GetComponent<TrailParticleEffect>();
    }

    void FixedUpdate()
    {
        // Assign in condition
        if (_isGrounded = wheelCollider.isGrounded)
        {
            var sidewaysFriction = wheelCollider.sidewaysFriction;
            var forwardFriction = wheelCollider.forwardFriction;
            if (_currentTerrainFeature.name == "Track")
            {
                sidewaysFriction.stiffness = 1f;
                forwardFriction.stiffness = 1f;
            }
            else
            {
                sidewaysFriction.stiffness = 0.5f;
                forwardFriction.stiffness = 0.5f;
            }
            wheelCollider.sidewaysFriction = sidewaysFriction;
            wheelCollider.forwardFriction = forwardFriction;
        }

        if (steer)
        {
            wheelCollider.steerAngle = SteerAngle;
        }

        if (power && wheelCollider.isGrounded)
        {
            wheelCollider.motorTorque = Torque;
        }

        if (braking)
        {
            wheelCollider.brakeTorque = BrakeTorque;
        }

        if (isHandBrake)
        {
            wheelCollider.brakeTorque = HandBrakeTorque;
        }
        
        if (!wheelCollider.isGrounded)
        {
            wheelCollider.brakeTorque = 10000f;
        }

        if (wheelCollider.rpm >= MaxWheelRpm)
        {
            wheelCollider.motorTorque = 0;
        }
    }

    private void Update()
    {
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot );
        _wheelTransform.rotation = rot;
        _wheelTransform.position = pos;
        if (wheelCollider.isGrounded)
        {
            wheelCollider.GetGroundHit(out WheelHit hit);
            SetTerrain(hit);
            float slipSpeed = Mathf.Max(Mathf.Abs(hit.forwardSlip), Mathf.Abs(hit.sidewaysSlip));
            if (slipSpeed > smokeEffectThreshold)
            {
                trailParticleEffect.GetComponent<TrailParticleEffect>().EmitParticle(slipSpeed * 10f);
            }

            if (Mathf.Abs(hit.sidewaysSlip) > trailEffectThreshold || Mathf.Abs(hit.forwardSlip) > trailEffectThreshold)
            {
                trailEffect.emitting = true;
            }
            else
            {
                trailEffect.emitting = false;
            }

            if (Mathf.Abs(hit.sidewaysSlip) > screechSoundThreshold ||
                Mathf.Abs(hit.forwardSlip) > screechSoundThreshold)
            {
                _skidSound.pitch = Mathf.Clamp(0.35f + Mathf.Max(Mathf.Abs(hit.sidewaysSlip), Mathf.Abs(hit.forwardSlip)), 0.75f, 1.25f);
                if(!_skidSound.isPlaying)
                    _skidSound.Play();
            }
            else
            {
                _skidSound.Stop();
            }
        }
        else
        {
            trailEffect.emitting = false;
            _skidSound.Stop();
        }
    }

    private void SetTerrain(WheelHit hit)
    {
        ITerrainFeature terrainHit = hit.collider.GetComponent<ITerrainFeature>();

        if (!terrainHit.Equals(_currentTerrainFeature))
        {
            // Debug.Log("Terrain Feature: "+terrainHit.name);
            // Debug.Log("Previous Feature: "+_currentTerrainFeature.name);

            ParticleSystem newTrailParticleEffect = Instantiate(terrainHit.GetTrailParticleEffect(), trailParticleEffect.transform.parent, false);
            newTrailParticleEffect.transform.position = trailParticleEffect.transform.position;
            _trailParticleEffectBehaviour.ScheduleDestroy();
            trailParticleEffect = newTrailParticleEffect;
            _trailParticleEffectBehaviour = trailParticleEffect.GetComponent<TrailParticleEffect>();

            _skidSound.Stop();
            _skidSound.clip = terrainHit.GetTrailAudioClip();
            
            TrailRenderer newTrail = Instantiate(trailEffect, trailEffect.transform.parent, false).GetComponent<TrailRenderer>();
            newTrail.material = terrainHit.GetTrailMaterial();
            newTrail.transform.position = trailEffect.transform.position;
            trailEffect.autodestruct = true;
            trailEffect.emitting = false;
            trailEffect = newTrail;
            _currentTerrainFeature = terrainHit;
        }
    }
}
