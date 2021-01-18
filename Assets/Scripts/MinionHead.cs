using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHead : MonoBehaviour
{
    [SerializeField] private Minion _minion = null;

    private void OnCollisionEnter(Collision collision)
    {
        print($"Head detected collision");
        if(collision.gameObject.TryGetComponent(out Wall wall) || collision.gameObject.tag == "Wall")
        {
            if (_minion.IsRagdollActive) ;
            else _minion.IsRagdollActive = true;

            wall.DestroyWall();
        }
    }
}
