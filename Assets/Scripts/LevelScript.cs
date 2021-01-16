using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private Points _points = null;
    [SerializeField] private int _minionCount = 0;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private GameObject _selector = null;

    public bool IsActiveGame { get; private set; } = false;

    private void Start()
    {
        _points.InstantiateMinions(_minionCount);
    }

    private void Update()
    {
        if (IsActiveGame)
            _points.transform.position += Vector3.forward * _moveSpeed * Time.deltaTime;
    }

    public void Begin()
    {
        IsActiveGame = true;
        _selector.SetActive(true);
    }

    public void Stop()
    {
        IsActiveGame = false;
        _selector.SetActive(false);
    }
}
