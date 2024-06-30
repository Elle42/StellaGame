using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] private float _fireRate = 1f;
    public GameObject _projectilePrefab;
    [SerializeField] private float _fireCost = 1f;


    private Transform _firePoint;
    private RessourceHandler resourcesHandler;
    private float nextFireTime = 0f;



    public void Init(Transform firePoint)
    {
        _firePoint = firePoint;
        resourcesHandler = _firePoint.GetComponent<RessourceHandler>();
    }

    public void TryFire()
    {
        if (Time.time >= nextFireTime && _fireCost < resourcesHandler._ressources)
        {
            
            Fire();
            
        }
    }


    private void Fire()
    {
        resourcesHandler.UpdateRessource(-_fireCost);
        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
        projectile.GetComponent<Projectile>().SetDirection(gameObject.transform.rotation);
        nextFireTime = Time.time + 1f / _fireRate;
    }
}
