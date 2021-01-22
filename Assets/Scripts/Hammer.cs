using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Interactive
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
    [SerializeField] private float _upAngle = 100f;
    [SerializeField] private float _upDuration = 3f;
    [SerializeField] private float _downAngle = 0f;
    [SerializeField] private float _downDuration = 0.4f;

    private Coroutine _moving = null;
    private bool _up = false;
    private bool Up
    {
        get => _up;
        set
        {
            _up = value;
            if (_moving != null) StopCoroutine(_moving);

            float from = _up ? _downAngle : _upAngle;
            float to = _up ? _upAngle : _downAngle;
            float duration = (_up ? _upDuration : _downDuration);

            print($"Up changed {_up}");

            _moving = StartCoroutine(Utils.CrossFading(from, to, duration,
                (a) =>
                {
                    Vector3 euler = _rotor.transform.localEulerAngles;
                    euler.x = a;
                    _rotor.transform.localEulerAngles = euler;
                    print($"{_rotor.transform.localEulerAngles}");
                },
                (a, b, c) => Mathf.Lerp(a, b, c)));
        }
    }

    public bool Active
    {
        get => _active;
        set
        {
            _active = value && !Interacted;
            if (_active) Up = Up;
            else if (_moving != null) StopCoroutine(_moving);
        }
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

    private void Start()
    {
        Up = Up;
    }

    private void Update()
    {
        if (Mathf.Abs(_rotor.transform.localEulerAngles.x -_upAngle) < 5f && Up ||
            Mathf.Abs(_rotor.transform.localEulerAngles.x - _downAngle) < 5f && !Up)
            Up = !Up;
    }
}
