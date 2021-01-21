using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : Interactive
{
    [Header("Components")]
    [SerializeField] private bool _active = true;
    [SerializeField] private Collider _rotor = null;
    [SerializeField] private GameObject _log = null;
    [SerializeField] private GameObject[] _logParts = null;
    [SerializeField] private Rigidbody _blade = null;
    [SerializeField] private Rigidbody _pendulum = null;

    [Space(20)]
    [Header("Ostillation data")]
    [SerializeField] private float _omega = 1f;
    [SerializeField] private float _phi = 1f;
    [SerializeField] private float _maxOffset = 60f;

    private float _counter = 0f;

    public bool Active
    {
        get => _active;
        set { _active = value && !Interacted; }
    }

    public bool Interacted { get; private set; } = false;

    public override void Interact()
    {
        Interacted = true;
        Active = false;
        _log.SetActive(false);
        foreach (var part in _logParts)
            part.SetActive(true);
        _blade.isKinematic = false;
        _rotor.enabled = false;
        _pendulum.Sleep();
    }

    private void FixedUpdate()
    {
        if (Active)
        {
            _counter += Time.fixedDeltaTime;
            Vector3 eulerAngles = _rotor.transform.localEulerAngles;
            eulerAngles.z = Mathf.Sin(_omega * _counter + _phi) * _maxOffset;
            _rotor.transform.localEulerAngles = eulerAngles;
        }
    }
}
