using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParticleScaler:MonoBehaviour
{
    public float Scale = 1;
    public void ScaleParticleSystem()
    {
        var scale = Scale;
        var gameObj = this;
        var hasParticleObj = false;
        var particles = gameObj.GetComponentsInChildren<ParticleSystem>(true);
        var max = particles.Length;
        for (int idx = 0; idx < max; idx++)
        {
            var particle = particles[idx];
            if (particle == null) continue;
            hasParticleObj = true;
            particle.startSize *= scale;
            particle.startSpeed *= scale;
            particle.startRotation *= scale;
            particle.transform.localScale *= scale;
        }
        if (hasParticleObj)
        {
            gameObj.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
