using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    [Header("Ragdoll data")]
    [SerializeField] private Animator _animator = null;
    [SerializeField] private Rigidbody _body = null;

    public Vector3 BodyOrigin => _body.transform.position;

    public bool IsRagdollActive
    {
        get => !_animator.enabled;
        set
        {
            _animator.enabled = !value;

            foreach (var rigidbody in GetComponentsInChildren<Rigidbody>())
                rigidbody.isKinematic = _animator.enabled;
            _agent.enabled = _animator.enabled;

            _renderer.material = IsRagdollActive ? _deathMaterial : _aliveMaterial;
        }
    }

    private bool _selected = false;
    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            _renderer.material = _selected ? _deathMaterial : _aliveMaterial;
        }
    }

    [Header("Move data")]
    [SerializeField] private NavMeshAgent _agent = null;
    [SerializeField] private Renderer _renderer = null;
    [SerializeField] private Material _aliveMaterial = null;
    [SerializeField] private Material _deathMaterial = null;

    public void SetDestination(Vector3 target) => _agent.SetDestination(target);

    public Transform Target { get; set; } = null;

    private void Start()
    {
        IsRagdollActive = false;
        _agent.Warp(transform.position);
        _animator.SetFloat("Offset", Random.Range(0f, 1f));

        StartCoroutine(Utils.CrossFading(Vector3.zero, Vector3.zero, 1f, (vector) => transform.eulerAngles = vector, (a, b, c) => Vector3.Lerp(a, b, c)));
    }

    private void Update()
    {
        if (!IsRagdollActive && Target != null)
        {
            SetDestination(Target.position);
        }

        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    public void Cast(Vector3 direction)
    {
        IsRagdollActive = true;
        foreach (var rb in GetComponentsInChildren<Rigidbody>())
            rb.AddForce(direction, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        print($"Minion detected collision");
        if (collision.gameObject.TryGetComponent(out Interactive interactive))
        {
            interactive.Interact();

            if (IsRagdollActive) ;
            else IsRagdollActive = true;
        }
    }
}
