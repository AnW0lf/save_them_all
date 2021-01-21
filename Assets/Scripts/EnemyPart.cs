using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPart : Interactive
{
    [SerializeField] private Enemy _enemy = null;

    public override void Interact()
    {
        _enemy.Interact();
    }
}
