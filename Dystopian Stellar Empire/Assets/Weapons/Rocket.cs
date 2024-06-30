using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Rocket : Projectile
{
    // Start is called before the first frame update
    [SerializeField] GameObject _ParticlePrefab;

    private void Awake()
    {
        SetCollisionParticle(_ParticlePrefab);
    }


}
