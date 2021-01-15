using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private Points _points = null;
    [SerializeField] private int _minionCount = 0;
    [SerializeField] private float _moveSpeed = 2f;

    private void Start()
    {
        _points.InstantiateMinions(_minionCount);
    }

    private void Update()
    {
        _points.transform.position += Vector3.forward * _moveSpeed * Time.deltaTime;
    }
}
