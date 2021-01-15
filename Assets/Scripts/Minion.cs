using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    [Header("Ragdoll data")]
    [SerializeField] private Animator _animator = null;
    [SerializeField] private Collider _mainCollider = null;
    [SerializeField] private Rigidbody _mainRigidbody = null;
    [SerializeField] private Collider[] _ragdollColliders = null;
    [SerializeField] private Rigidbody[] _ragdollRigidbodies = null;
    [SerializeField] private Joint[] _ragdollJoints = null;

    public bool IsRagdollActive
    {
        get => !_animator.enabled;
        set
        {
            _animator.enabled = !value;
            if (IsRagdollActive)
            {
                _mainRigidbody.Sleep();
                _mainCollider.enabled = false;

                foreach (var rigidbody in _ragdollRigidbodies)
                    rigidbody.WakeUp();

                foreach (var collider in _ragdollColliders)
                    collider.enabled = true;

                _renderer.material = _deathMaterial;

                _agent.enabled = false;
            }
            else
            {
                _mainRigidbody.WakeUp();
                _mainCollider.enabled = true;

                foreach (var rigidbody in _ragdollRigidbodies)
                    rigidbody.Sleep();

                foreach (var collider in _ragdollColliders)
                    collider.enabled = false;

                _renderer.material = _aliveMaterial;

                _agent.enabled = true;
            }
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
        transform.eulerAngles = Vector3.zero;
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

    }
}
