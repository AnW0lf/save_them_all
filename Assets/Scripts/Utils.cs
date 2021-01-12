using System;
using System.Collections;
using UnityEngine;

public static class Utils
{
    public static Process GetCrossFade<A>(A from, A to, float duration, Action<A> setter, Func<A, A, float, A> lerp)
    {
        Process process = new Process();
        IEnumerator crossFading = CrossFading(from, to, duration, setter, lerp, process.Coroutine);
        process.Enumerator = crossFading;
        return process;
    }

    public static IEnumerator CrossFading<A>(A from, A to, float duration, Action<A> setter, Func<A, A, float, A> lerp, Coroutine self)
    {
        float timer = 0f;

        while (timer <= duration)
        {
            timer += Time.deltaTime;
            setter(lerp(from, to, timer / duration));
            yield return null;
        }

        self = null;
    }
}

public class Process : MonoBehaviour
{
    public Coroutine Coroutine { get; private set; }
    public IEnumerator Enumerator { get; set; }

    public Process()
    {
        Coroutine = null;
        Enumerator = null;
    }

    public Process(IEnumerator enumerator)
    {
        Coroutine = null;
        Enumerator = enumerator;
    }

    public void Begin()
    {
        if (Coroutine != null) StopCoroutine(Coroutine);
        Coroutine = StartCoroutine(Enumerator);
    }

    public void Break()
    {
        if (Coroutine != null) StopCoroutine(Coroutine);
        Coroutine = null;
    }
}
