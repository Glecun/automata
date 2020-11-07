using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown: MonoBehaviour {
    public int timeRemaining;
    public bool isCountingDown = false;

    public void of(int duration)
    {
        if (!isCountingDown) {
            isCountingDown = true;
            timeRemaining = duration;
            Invoke ( "_tick", 1f );
        }
    }
 
    private void _tick() {
        timeRemaining--;
        if(timeRemaining > 0) {
            Invoke ( "_tick", 1f );
        } else {
            isCountingDown = false;
        }
    }
}