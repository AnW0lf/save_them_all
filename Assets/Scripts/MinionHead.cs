using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHead : MonoBehaviour
{
    [SerializeField] private Minion _minion = null;

    public Minion Minion => _minion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Interactive interactive))
        {
            interactive.Interact();

            if (!_minion.IsRagdollActive) _minion.IsRagdollActive = true;
            if (!_minion.IsDeath) _minion.IsDeath = true;
        }
        else if (collision.gameObject.tag == "KillZone")
        {
            if (!_minion.IsRagdollActive) _minion.IsRagdollActive = true;
            if (!_minion.IsDeath) _minion.IsDeath = true;
        }

        Minion.ChectToAddToParty(collision.gameObject);
    }
}
