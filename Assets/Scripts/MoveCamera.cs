using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform _target = null;
    [SerializeField] private float _height = 3f;
    [SerializeField] private float _distance = 6f;

    private void Update()
    {
        transform.position = _target.transform.position
            + Vector3.back * _distance
            + Vector3.up * _height;
    }
}
