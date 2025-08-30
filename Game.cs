using NumberGuessingGame.Helpers;
using NumberGuessingGame.Models;

public class Game
{
    private GameStats GameStats = null!;

    public Game() { }

    public void ExecuteGame()
    {
        Welcome();
        GameStats = new GameStats();
        do
        {
            SelectDifficulty();
            GameStats.StartTimer();
            LoopGuessing();
            GameStats.StopTimer();
            GetResult();
            GameStats.Reset();
        } while (AskToRepeat());

        Console.WriteLine("Thanks for playing!");
    }

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
                        $"between {min} and {max}.\n");
            }
            else
            {
                isValid = true;
            }
        }
        return number;
    }

    private void SelectDifficulty()
    {
        GameStats.MaxAttempts = GetMaxAttempts(
                            AskInput("""
                        Please select the difficulty:
                        1 - Easy (10 attempts)
                        2 - Medium (5 attempts)
                        3 - Hard (3 attempts)
                        Option: 
                        """, 1, 3));
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

    private void LoopGuessing()
    {
        while (GameStats.CurrentAttempt < GameStats.MaxAttempts &&
                GameStats.GuessedNumber != GameStats.NumberToGuess)
        {
            if (GameStats.MaxAttempts == GameStats.CurrentAttempt + 1)
                Console.WriteLine("\n--- Last attempt! ---");
            else
                Console.WriteLine($"\n--- Attempt {GameStats.CurrentAttempt + 1} ---");

            GameStats.GuessedNumber = AskInput("Enter your guess: ", 1, 100);

            if (GameStats.AlreadyGuessedNumber())
            {
                Console.WriteLine($"You've already tried with {GameStats.GuessedNumber}. " +
                            "Please try another number.");
                continue;
            }

            GameStats.SavePlayerGuess(GameStats.GuessedNumber);
            GameStats.CurrentAttempt++;

            if (GameStats.MaxAttempts != GameStats.CurrentAttempt)
            {
                if (GameStats.GuessedNumber - 25 > GameStats.NumberToGuess)
                    Console.WriteLine("Too high!");
                else if (GameStats.GuessedNumber + 25 < GameStats.NumberToGuess)
                    Console.WriteLine("Too low!");
                else if (GameStats.GuessedNumber > GameStats.NumberToGuess)
                    Console.WriteLine("A bit high!");
                else if (GameStats.GuessedNumber < GameStats.NumberToGuess)
                    Console.WriteLine("A bit low!");
            }
        }
    }

    private void GetResult()
    {

        Console.WriteLine();
        if (GameStats.GuessedNumber == GameStats.NumberToGuess)
        {
            string showAttempts = "attempt";
            if (GameStats.CurrentAttempt > 1) showAttempts = "attempts";
            Console.WriteLine($"Congrats! You beat the game in " +
                    $"{GameStats.GetTimeInSeconds()} seconds " +
                    $"and {GameStats.CurrentAttempt} {showAttempts}!");
        }
        else
        {
            Console.WriteLine($"You lost. The number was {GameStats.NumberToGuess}. " +
                        "Good luck next time.\n");
        }
    }

    private bool AskToRepeat()
    {
        string loopInput = "";
        Console.Write("Type 0 to exit or anything else to play again: ");
        loopInput = Console.ReadLine() ?? "";
        Console.WriteLine();

        if (loopInput.Equals("0"))
            return false;

        return true;
    }
}