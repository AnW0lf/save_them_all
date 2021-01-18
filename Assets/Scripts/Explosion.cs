using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Interactive
{
    [SerializeField] private float _range = 2f;
    [SerializeField] private float _explosionForce = 100f;
    [SerializeField] private GameObject _effectPrefab = null;

    private bool _deactive = false;

    public override void Interact()
    {
        if (_deactive) return;
        _deactive = true;

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _range, Vector3.one);

        foreach (var hit in hits)
        {
            if (hit.transform == transform) continue;

            if (hit.transform.TryGetComponent(out Interactive interactive))
            {
                interactive.Interact();
            }

            Transform hitTransform = hit.transform;
            while (hitTransform != null)
            {
                if (hitTransform.TryGetComponent(out Minion minion))
                {
                    if (!minion.IsRagdollActive)
                        minion.IsRagdollActive = true;
                    break;
                }
                hitTransform = hitTransform.parent;
            }
        }

        Instantiate(_effectPrefab).transform.position = transform.position;

        StartCoroutine(DelayedExplosion());
    }

    private IEnumerator DelayedExplosion()
    {
        yield return new WaitForSecondsRealtime(0.05f);

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _range, Vector3.one);

        foreach (var hit in hits)
        {
            if (hit.transform == transform) continue;

            if (hit.transform.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(_explosionForce, transform.position, _range);
            }
        }

        Destroy(gameObject);
    }
}
