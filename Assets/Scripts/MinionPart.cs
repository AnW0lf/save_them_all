using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPart : MonoBehaviour
{
    [SerializeField] private Minion _minion = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Interactive interactive))
        {
            print("Part");
            interactive.Interact();
            if (_minion.IsRagdollActive) ;
            else _minion.IsRagdollActive = true;
        }
    }
}
