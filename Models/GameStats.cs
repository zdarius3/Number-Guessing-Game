using System.Diagnostics;

namespace NumberGuessingGame.Models;

public class GameStats
{
    public int MaxAttempts { get; set; }
    public int CurrentAttempt { get; set; }
    public int GuessedNumber { get; set; }
    public int NumberToGuess { get; set; }

    public Stopwatch Timer { get; set; } = new Stopwatch();

    public GameStats()
    {
        Reset();
    }

    public void Reset()
    {
        CurrentAttempt = 0;
        GuessedNumber = 0;
        NumberToGuess = Random.Shared.Next(1, 101);
        Timer.Reset();
    }

    public void StartTimer()
    {
        Timer.Start();
    }

    public void StopTimer()
    {
        Timer.Stop();
    }

    public int GetTimeInSeconds()
    {
        return Timer.Elapsed.Seconds;
    }
}