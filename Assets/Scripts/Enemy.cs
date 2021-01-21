using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Interactive
{
    [Header("Ragdoll data")]
    [SerializeField] private Animator _animator = null;
    [SerializeField] private Renderer _renderer = null;
    [SerializeField] private Material _aliveMaterial = null;
    [SerializeField] private Material _deathMaterial = null;

    public bool IsRagdollActive
    {
        get => !_animator.enabled;
        set
        {
            _animator.enabled = !value;

            foreach (var rigidbody in GetComponentsInChildren<Rigidbody>())
                rigidbody.isKinematic = _animator.enabled;
            _agent.enabled = _animator.enabled;

            if (IsRagdollActive)
                _renderer.material = _deathMaterial;
            else
                _renderer.material = _aliveMaterial;
        }
    }

    [Header("Move data")]
    [SerializeField] private NavMeshAgent _agent = null;
    [SerializeField] private float _castDistance = 5f;
    [SerializeField] private float _moveDistance = 15f;

    public void SetDestination(Vector3 target) => _agent.SetDestination(target);

    public Transform Target { get; set; } = null;

    public override void Interact()
    {
        IsRagdollActive = true;
        foreach (var enemyPart in GetComponentsInChildren<EnemyPart>())
            Destroy(enemyPart, 0.1f);
        Destroy(this);
    }

    private void Start()
    {
        IsRagdollActive = false;
        _agent.Warp(transform.position);
        _animator.SetFloat("Offset", Random.Range(0f, 1f));
    }

    private void Update()
    {
        if (!IsRagdollActive)
        {
            if (Target == null)
            {
                foreach (var minion in FindObjectsOfType<Minion>()
                    .Where((m) => m.Target != null && !m.IsRagdollActive
                    && Vector3.Distance(transform.position, m.transform.position) < _moveDistance))
                {
                    Target = minion.transform;
                    break;
                }
            }
            else
            {
                SetDestination(Target.position);
            }

            if (Vector3.Distance(transform.position, Target.position) < _castDistance)
            {
                Vector3 direction = (Target.position - transform.position).normalized + Vector3.up * 0.5f;
                direction *= 15f;
                Cast(direction);
            }
        }

        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    public void Cast(Vector3 direction)
    {
        IsRagdollActive = true;

        foreach (var rb in GetComponentsInChildren<Rigidbody>())
            rb.AddForce(direction, ForceMode.Acceleration);
    }
}
