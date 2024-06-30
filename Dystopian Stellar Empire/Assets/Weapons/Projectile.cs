using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _lifetime = 5f;
    [SerializeField] private int _damage = 10;


    private Vector3 _velocity;
    private GameObject? _collisionParticlePrefab = null;
    private GameObject? _collisionParticle = null;
    protected void SetCollisionParticle(GameObject particle)
    {
        _collisionParticlePrefab = particle;
    }

    public float getRange()
    {
        return _lifetime * _speed;
    }
    public void SetDirection(Quaternion direction)
    {
        _velocity = direction * Vector3.forward;
    }

    void Start()
    {
        Destroy(gameObject, _lifetime); // Destroy the projectile after its lifetime
    }

    void OnCollisionEnter(Collision collision)
    {
        // Example of dealing damage to a target with a Health script
        HealthHandler target = collision.gameObject.GetComponent<HealthHandler>();
        if (target != null)
        {
            if(_collisionParticle != null)
            {
                _collisionParticle = Instantiate(_collisionParticlePrefab, collision.transform);
            }

            target.TakeDamage(_damage);
        }

        // Destroy the projectile on impact
        Destroy(gameObject);
    }


    private void OnCollisionExit(Collision collision)
    {
        if(_collisionParticle != null)
        {
            Destroy(_collisionParticle);
        }
    }

    //Using an extra Method to be able to speed up time in later Stages
    public void TickHandler(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            if (_lifetime > 0)
            {
                _lifetime -= Time.deltaTime;
                if (_lifetime <= 0)
                {
                    Destroy(gameObject);
                    return;
                }
            }
            Move();
        }
    }

    void FixedUpdate()
    {
        TickHandler(1);
    }

    private void Move()
    {
        transform.position += _velocity * Time.deltaTime * _speed;
    }
}
