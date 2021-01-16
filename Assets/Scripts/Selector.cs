using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LineRenderer _line = null;

    public Minion Target { get; private set; } = null;

    private Vector2 _down = Vector2.zero;
    private Vector3 _from = Vector2.zero;
    private Vector3 _to = Vector2.zero;
    private Vector3 _direction = Vector2.zero;

    private bool _holded = false;

    private void Update()
    {
        if (_holded)
        {
            UpdatePositions();

            Vector3[] positions = { _from, _to };
            _line.SetPositions(positions);
        }
    }

    private void UpdatePositions()
    {
        if (Target != null)
        {
            Vector2 screenDifference = (Vector2)Input.mousePosition - _down;
            _from = Target.transform.position + Vector3.up * 0.5f;
            float horizontalOffset = Mathf.Clamp(-3f * screenDifference.x / Screen.width, -1f, 1f);
            float forceOffset = Mathf.Clamp(-3f * screenDifference.y / Screen.height, 0.2f, 1f);
            _to = _from
                + Vector3.RotateTowards(Target.transform.forward, Mathf.Sign(horizontalOffset) * Target.transform.right, Mathf.Abs(horizontalOffset), 0f)
                + Vector3.RotateTowards(Target.transform.forward, Vector3.up, 0.5f, 0f) * 10f * forceOffset;
        }
    }

    private void Select()
    {
        Target = null;
        foreach (var minion in FindObjectsOfType<Minion>())
        {
            if (minion.IsRagdollActive) continue;
            if (Target == null || minion.transform.position.z > Target.transform.position.z)
                Target = minion;
        }

        if (Target != null)
            Target.Selected = true;
    }

    private void Cast()
    {
        if (Target != null)
            Target.Cast(_direction);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _holded = true;
        _line.enabled = true;

        Time.timeScale = 0.1f;

        _down = Input.mousePosition;

        Select();

        UpdatePositions();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Time.timeScale = 1f;

        UpdatePositions();

        _direction = _to - _from;

        if (Target != null)
        {
            Target.Selected = false;

            if (Vector2.Distance(_down, Input.mousePosition) > 10f)
                Cast();
        }

        _line.enabled = false;
        _holded = false;
    }
}
