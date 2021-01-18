using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private LineRenderer _line = null;
    [SerializeField] private float _castVelocity = 150f;
    [SerializeField] private float _duration = 10f;
    [SerializeField] private float _step = 0.1f;

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
            _direction = (_to - _from) * _castVelocity; 

            if (Target != null)
            DrawLine(_from, _direction);
        }
    }

    private void UpdatePositions()
    {
        if (Target != null)
        {
            Vector2 screenDifference = (Vector2)Input.mousePosition - _down;
            _from = Target.transform.position + Vector3.up * 0.5f;
            float horizontalOffset = Mathf.Clamp(-3f * screenDifference.x / Screen.width, -1.2f, 1.2f);
            float forceOffset = Mathf.Clamp(-3f * screenDifference.y / Screen.height, 0.1f, 1f);
            Vector3 offset = Vector3.RotateTowards(Target.transform.forward, Mathf.Sign(horizontalOffset) * Target.transform.right, Mathf.Abs(horizontalOffset), 0f);
            offset = Vector3.RotateTowards(offset, Vector3.up, 0.6f, 0f);
            offset *= forceOffset;
            print(offset);
            _to = _from + offset;
        }
    }

    private void DrawLine(Vector3 origin, Vector3 direction)
    {
        int count = (int)(_duration / _step);
        Vector3[] positions = new Vector3[count];
        _line.positionCount = count;
        Vector3 g = Physics.gravity;

        for(int i = 0; i < count; i++)
        {
            float t = _step * i;
            Vector3 position = origin + direction * t + g * Mathf.Pow(t, 2f) / 2f;
            //print($"{position} = {origin} + {direction} * {t} ({direction * t}) + {g} * {t}^2 ({Mathf.Pow(t, 2f)}) / 2 ({g * Mathf.Pow(t, 2f) / 2f})");
            positions[i] = position;
        }

        _line.SetPositions(positions);
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
            Target.Cast(_direction * 50f);
    }

    Coroutine _changeTimeScale = null;

    private void CrossFadeTimeScale(float timeScale)
    {
        if (_changeTimeScale != null) StopCoroutine(_changeTimeScale);

        _changeTimeScale = StartCoroutine(Utils.CrossFading(Time.timeScale, timeScale, 0.5f, (t) => Time.timeScale = t, (a, b, c) => Mathf.Lerp(a, b, c)));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _holded = true;
        _line.enabled = true;

        CrossFadeTimeScale(0.1f);

        _down = Input.mousePosition;

        Select();

        UpdatePositions();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CrossFadeTimeScale(1f);

        UpdatePositions();

        _direction = (_to - _from) * _castVelocity;

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
