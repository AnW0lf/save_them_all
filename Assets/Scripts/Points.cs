using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private Color _sphereColor = Color.cyan;
    [SerializeField] private GameObject _minionPrefab = null;
    [SerializeField] private Transform[] _points = null;

    private void OnDrawGizmos()
    {
        if (_points == null) return;
        for (int i = 0; i < _points.Length; i++)
        {
            Gizmos.color = _sphereColor;
            Gizmos.DrawSphere(_points[i].position, 0.1f);
        }
    }

    public void InstantiateMinions(int count)
    {
        count = Mathf.Clamp(count, 0, _points.Length);

        for(int i = 0; i < count; i++)
        {
            GameObject minion = Instantiate(_minionPrefab);
            minion.transform.position = _points[i].position;
        }
    }
}
