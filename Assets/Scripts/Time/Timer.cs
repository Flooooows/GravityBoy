using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    float delay = 0f;
    [SerializeField]
    float interval = 0f;
    public UnityEvent onTick;

    Coroutine timerCR;

    // Start is called before the first frame update
    private void OnEnable()
    {
        timerCR = StartCoroutine(TimerCoroutine());
    }

    // Start is called before the first frame update
    private void OnDisable()
    {
        try
        {
            StopCoroutine(timerCR);
        }
        catch (System.Exception)
        {
            //do something
        }
    }

    IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(delay);
        while (true)
        {
            Tick();
            yield return new WaitForSeconds(interval);
        }
    }

    void Tick()
    {
        onTick.Invoke();
    }


}
