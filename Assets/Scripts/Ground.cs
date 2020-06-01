using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : ITerrainFeature
{
    public Material trailMaterial;
    public ParticleSystem trailParticleSystem;
    public AudioClip trailAudioClip;
    
    void Start()
    {
        TerrainFeatureName = gameObject.name;
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
        return trailAudioClip;
    }
}
