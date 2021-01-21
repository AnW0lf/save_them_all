using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Interactive
{
    [SerializeField] private MeshDestroy _meshDestroy = null;

    private bool _deactive = false;

    public override void Interact()
    {
        if (_deactive) return;
        _deactive = true;

        _meshDestroy.DestroyMesh();
    }
}
