using UnityEngine;

public class Countdown : MonoBehaviour
{
    public float timeRemaining;
    public bool isCountingDown = false;
    public bool isAcknowledged = true;


    private readonly float tickTime = 0.1f;

    public void wait(float duration)
    {
        if (!isCountingDown)
        {
            isAcknowledged = false;
            isCountingDown = true;
            timeRemaining = duration;
            Invoke("_tick", tickTime);
        }
    }

    private void _tick()
    {
        timeRemaining -= tickTime;
        if (timeRemaining > 0)
        {
            Invoke("_tick", tickTime);
        }
        else
        {
            isCountingDown = false;
        }
    }

    public void acknowlege()
    {
        this.isAcknowledged = true;
    }
}