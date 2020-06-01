using UnityEngine;

public class TrailParticleEffect : MonoBehaviour
{
    public float emitDelay = 0.2f;
    public bool randomizeInitSpeed;
    private bool _destroy;
    private float _emitElapsed;
    private ParticleSystem _particleSystem;
    private bool _initialized;
    

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _initialized = true;
    }

    public void ScheduleDestroy()
    {
        _destroy = true;
    }

    public void EmitParticle(float speed)
    {
        if (_initialized)
        {
            if (randomizeInitSpeed)
            {
                speed = Random.value * speed;
            }
            var particlesMain = _particleSystem.main;
            particlesMain.startSpeedMultiplier = speed;
            
            if (_emitElapsed > emitDelay)
            {
                _particleSystem.Emit(1);
                _emitElapsed = 0f;
            }
        }
    }
    
    // Update is called once per framex    
    void Update()
    {
        _emitElapsed += Time.deltaTime;
        if (_destroy && !_particleSystem.IsAlive())
        {
            Destroy(this);
        }
    }
}
