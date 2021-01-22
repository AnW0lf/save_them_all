using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private GameObject _minionPrefab = null;
    [SerializeField] private Point[] _points = null;

    public void InstantiateMinions(int count)
    {
        count = Mathf.Clamp(count, 0, _points.Length);

        for (int i = 0; i < count; i++)
        {
            Minion minion = Instantiate(_minionPrefab).GetComponent<Minion>();
            minion.transform.position = _points[i].transform.position + Vector3.back * 0.9f;
            _points[i].AttachMinion(minion);
        }
    }

    public void AttachMinion(Minion minion)
    {
        foreach (var point in _points)
        {
            if (point.IsEmpty)
            {
                point.AttachMinion(minion);
                return;
            }
        }
    }

    private void Update()
    {
        foreach (var point in _points)
        {
            if (!point.IsEmpty && point.Minion.IsRagdollActive)
            {
                point.Clear();
            }
        }
    }
}
