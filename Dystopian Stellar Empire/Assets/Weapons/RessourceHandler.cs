using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RessourceHandler : MonoBehaviour
{

    [SerializeField] private float _regen;
    public float _regenAmp = 1f;
    public float _ressources;
    public float _ressourcesMax;

    public void UpdateRessource(float diff)
    {
        
        _ressources = Mathf.Min(_ressources + diff, _ressourcesMax);
    }

    public void SetRessource(float diff)
    {
        _ressources = diff;
    }

    void FixedUpdate()
    {
        UpdateRessource(_regen * _regenAmp * Time.deltaTime);
    }
}
