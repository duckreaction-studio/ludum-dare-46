using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FishImpulse : MonoBehaviour
{
    [SerializeField]
    private float explosionForce = 1f;
    [SerializeField]
    private float explosionRadius = 0.1f;
    [SerializeField]
    private float upwardsModifier = 0f;
    [SerializeField]
    private string[] targetFilters;

    private Rigidbody _rigidbody;
    public new Rigidbody rigidbody
    {
        get
        {
            if(_rigidbody == null)
            {
                _rigidbody = GetComponent<Rigidbody>();
            }
            return _rigidbody;
        }
    }

    [ContextMenu("Impact")]
    public void FishImpact(string target)
    {
        if(rigidbody != null && targetFilters.Contains(target))
        {
            Debug.Log("Explode");
            rigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);
        }
    }
}
