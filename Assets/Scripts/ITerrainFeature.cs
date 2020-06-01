using UnityEngine;

public abstract class ITerrainFeature : MonoBehaviour
{
   protected string TerrainFeatureName;

   public abstract Material GetTrailMaterial();
   public abstract ParticleSystem GetTrailParticleEffect();

   public abstract AudioClip GetTrailAudioClip();

   public bool Equals(ITerrainFeature other)
   {
      return other.TerrainFeatureName == TerrainFeatureName;
   }
}