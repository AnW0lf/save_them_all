using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHead : MonoBehaviour
{
    [SerializeField] private Minion _minion = null;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "KillZone")
        {
            if (_minion.IsRagdollActive) ;
            else _minion.IsRagdollActive = true;
        }
    }
}
