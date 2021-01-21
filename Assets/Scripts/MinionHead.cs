using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionHead : MonoBehaviour
{
    [SerializeField] private Minion _minion = null;

    public Minion Minion => _minion;

    private void OnCollisionEnter(Collision collision)
    {
        print("Head begin");
        if (collision.gameObject.TryGetComponent(out Interactive interactive))
        {
            print("Head");
            interactive.Interact();
            if (_minion.IsRagdollActive) ;
            else _minion.IsRagdollActive = true;
        }
        else if (collision.gameObject.tag == "KillZone")
        {
            print("Head");
            if (_minion.IsRagdollActive) ;
            else _minion.IsRagdollActive = true;
        }

        Minion.ChectToAddToParty(collision.gameObject);
    }
}
