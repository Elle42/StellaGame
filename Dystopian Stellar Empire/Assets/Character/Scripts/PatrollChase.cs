using System;
using UnityEngine;
using UnityEngine.AI;

public class PatrollChase : MonoBehaviour
{
    public Transform[] waypoints;
    public float detectionRange = 10f;
    public float chaseSpeed = 5f;
    public float patrolSpeed = 2f;
    public Transform player;

    [SerializeField] private Transform _holdPosition;
    [SerializeField] private GameObject? _pickedObject;

    private bool _holdingObject = false;
    private NavMeshAgent _agent;
    private int _currentWaypointIndex = 0;
    private bool _isChasing = false;
    private WeaponBase? _currentWeapon;

    public void SetWeapon(WeaponBase weapon)
    {
        _currentWeapon = weapon;
    }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = patrolSpeed;
        GoToNextWaypoint();
        PickUpObject(_pickedObject);
    }
    void HoldObject()
    {
        
        _pickedObject.transform.position = _holdPosition.position;
        _pickedObject.transform.rotation = _holdPosition.rotation;
    }
    void PickUpObject(GameObject obj)
    {
        _holdingObject = true;
        _pickedObject = obj;

        if( _pickedObject.GetComponent<WeaponBase>() != null ) 
        { 
            _currentWeapon = _pickedObject.GetComponent<WeaponBase>();
            _currentWeapon.Init(_holdPosition);
        }
    }
    void DropObject()
    {
        _holdingObject = false;
        _pickedObject = null;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if(_holdingObject)
        {
            HoldObject();
        }
        if (distanceToPlayer < detectionRange)
        {
            StartChasing();
            
        }
        else if (_isChasing)
        {
            if(distanceToPlayer > detectionRange * 2) 
            { 
            StopChasing();
            }
            if(_currentWeapon._projectilePrefab.GetComponent<Projectile>().getRange() > distanceToPlayer)
            {
                _currentWeapon.TryFire();
            }
           
            
        }

        if (!_isChasing && _agent.remainingDistance < 1.5f)
        {
            GoToNextWaypoint();
        }
    }

    private void SearchWeapon()
    {
        throw new NotImplementedException();
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0)
            return;

        _agent.destination = waypoints[_currentWaypointIndex].position;
        _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
    }

    void StartChasing()
    {
        _isChasing = true;
        _agent.speed = chaseSpeed;
        _agent.destination = player.position;
    }

    void StopChasing()
    {
        _isChasing = false;
        _agent.speed = patrolSpeed;
        GoToNextWaypoint();
    }
}
