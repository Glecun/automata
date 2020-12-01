using System;
using UnityEngine;
using Random = System.Random;

public class Utils
{
    public static void doAndWait(Action todo, Countdown countdown, float duration)
    {
        if (!countdown.isCountingDown)
        {
            todo();
            countdown.wait(duration);
        }
    }

    public static void waitAndDo(Action todo, Countdown countdown, float duration, bool startCountDown)
    {
        if (!countdown.isCountingDown && !countdown.isAcknowledged)
        {
            todo();
            countdown.acknowlege();
        }
        else if (!countdown.isCountingDown && startCountDown)
        {
            countdown.wait(duration);
        }
    }

    public static int random(int min, int max)
    {
        return random(min, max, DateTime.Now.Millisecond);
    }

    public static int random(int min, int max, int seed)
    {
        var random = new Random(seed);
        return random.Next(min, max + 1);
    }

    public static Vector3 getTopPosition(Transform transform, int height, float offset)
    {
        var vector = transform.position;
        vector.y += height + offset;
        return vector;
    }

    public static T initWhenFound<T>(T objToInit, Func<T> fun)
    {
        if (objToInit != null) return objToInit;
        return fun();
    }
}