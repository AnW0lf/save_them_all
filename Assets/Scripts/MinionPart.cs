using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionPart : MonoBehaviour
{
    [SerializeField] private Minion _minion = null;

    public Minion Minion => _minion;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Interactive interactive))
        {
            print("Part");
            interactive.Interact();

            if (!_minion.IsRagdollActive) _minion.IsRagdollActive = true;
            if (!_minion.IsDeath) _minion.IsDeath = true;
        }

        Minion.ChectToAddToParty(collision.gameObject);
    }
}
