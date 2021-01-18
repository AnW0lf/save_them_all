using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Interactive
{
    [SerializeField] private GameObject _destroyedWallPrefab = null;
    [SerializeField] private Renderer _renderer = null;

    public override void Interact()
    {
        Transform destroyedWall = Instantiate(_destroyedWallPrefab, transform.parent).transform;
        destroyedWall.position = transform.position;
        destroyedWall.rotation = transform.rotation;

        foreach (var renderer in destroyedWall.GetComponentsInChildren<Renderer>())
            renderer.material = _renderer.material;

        Destroy(gameObject);
    }
}
