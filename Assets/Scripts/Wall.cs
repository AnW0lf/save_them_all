using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] private GameObject _destroyedWallPrefab = null;

    public void DestroyWall()
    {
        Transform destroyedWall = Instantiate(_destroyedWallPrefab, transform.parent).transform;
        destroyedWall.position = transform.position;
        destroyedWall.rotation = transform.rotation;

        Destroy(gameObject);
    }
}
