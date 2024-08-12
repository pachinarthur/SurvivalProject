using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMA_Monster_IdleComponent : MonoBehaviour
{
    public event Action OnTimerElapsed = null;
    [SerializeField] float timeMin = 0, timeMax = 3, waitingTime = 0, currentTime = 0;
    [SerializeField] bool start = false;

    // Start is called before the first frame update
    void Start()
    {
        OnTimerElapsed += ResetTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
            currentTime = UpdateTime(currentTime, waitingTime);
    }

    public void InitTime()
    {

        waitingTime = UnityEngine.Random.Range(timeMin, timeMax);
    }

    float UpdateTime(float _time, float _timeMax)
    {
        _time += Time.deltaTime;
        if (_time >= _timeMax)
        {
            OnTimerElapsed?.Invoke();
            return 0;
        }
        return _time;
    }

    public void StartTime()
    {
        start = true;
    }

    void ResetTime()
    {
        start = false;
        currentTime = 0;
    }
}
