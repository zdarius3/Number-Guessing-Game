using System.Diagnostics;
using NumberGuessingGame.Helpers;

public class Game
{
    private int MaxAttempts;
    private int NumberToGuess;
    private int GuessedNumber;
    private int CurrentAttempt;
    private Stopwatch Timer = new Stopwatch();

    public Game() { }

    private void Welcome()
    {
        Console.WriteLine("Welcome to the Number Guessing Game!");
        Console.WriteLine("I'm thinking of a number between 1 and 100... " +
                        "Test your luck with me!\n");
    }

    private int AskInput(string message, int min, int max)
    {
        int number = 0;
        bool isValid = false;
        while (!isValid)
        {
            Console.Write(message);
            number = InputHelper.ParseInput(Console.ReadLine() ?? "0");
            if (number < min || number > max)
            {
                Console.WriteLine("Error. Please input only a number " +
                        $"between {min} and {max}.");
            }
            else
            {
                isValid = true;
            }
        }
        return number;
    }

    private int GetMaxAttempts(int option)
    {
        return option switch
        {
            1 => 10,
            2 => 5,
            3 => 3,
            _ => 0
        };
    }

    public void ExecuteGame()
    {
        Welcome();
        string loopInput = "";
        do
        {
            GuessedNumber = 0;
            CurrentAttempt = 0;
            NumberToGuess = Random.Shared.Next(1, 101);
            MaxAttempts = GetMaxAttempts(
                            AskInput("""
                        Please select the difficulty:
                        1 - Easy (10 attempts)
                        2 - Medium (5 attempts)
                        3 - Hard (3 attempts)
                        Option: 
                        """, 1, 3));
            Timer.Start();

            while (CurrentAttempt < MaxAttempts && GuessedNumber != NumberToGuess)
            {
                CurrentAttempt++;
                if (MaxAttempts == CurrentAttempt)
                    Console.WriteLine("\n--- Last attempt! ---");
                else
                    Console.WriteLine($"\n--- Attempt {CurrentAttempt} ---");

                GuessedNumber = AskInput("Enter your guess: ", 1, 100);

                if (MaxAttempts != CurrentAttempt)
                {
                    if (GuessedNumber > NumberToGuess)
                        Console.WriteLine("Too high!");
                    else if (GuessedNumber < NumberToGuess)
                        Console.WriteLine("Too low!");
                } 
            }

            Timer.Stop();
            Console.WriteLine();

            if (GuessedNumber == NumberToGuess)
            {
                string showAttempts = "attempt";
                if (CurrentAttempt > 1) showAttempts = "attempts";
                Console.WriteLine($"Congrats! You beat the game in " +
                        $"{Timer.Elapsed.Seconds} seconds " +
                        $"and {CurrentAttempt} {showAttempts}!");
            }
            else
            {
                Console.WriteLine($"You lost. The number was {NumberToGuess}. " +
                        "Good luck next time.\n");
            }

            Console.Write("Type 0 to exit or anything else to play again: ");
            loopInput = Console.ReadLine() ?? "";
        } while (!loopInput.Trim().Equals("0"));

        Console.WriteLine("\nThanks for playing!");
    }
}