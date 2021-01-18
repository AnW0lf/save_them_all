using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Interactive
{
    [SerializeField] private float _range = 2f;
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private GameObject _effectPrefab = null;

    public override void Interact()
    {
        foreach (var hit in Physics.SphereCastAll(transform.position, _range, Vector3.one))
        {
            if (hit.transform.TryGetComponent(out Interactive interactive))
                interactive.Interact();

            if (hit.transform.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _range);
        }

        Instantiate(_effectPrefab).transform.position = transform.position;

        Destroy(gameObject);
    }
}
