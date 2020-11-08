using System;

public class Utils
{
    public static void waitOrDo(Action todo, Countdown countdown, float duration)
    {
        if (!countdown.isCountingDown)
        {
            todo();
            countdown.wait(duration);
        }
    }

    public static int random(int min, int max)
    {
        var random = new Random(System.DateTime.Now.Millisecond);
        return random.Next(min, max + 1);
    }
}