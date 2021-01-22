using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    private Minion _minion = null;

    public Minion Minion => _minion;

    public void AttachMinion(Minion minion)
    {
        _minion = minion;
        minion.Target = transform;
    }

    public bool IsEmpty => _minion == null;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

    public void Clear()
    {
        _minion = null;
    }
}
