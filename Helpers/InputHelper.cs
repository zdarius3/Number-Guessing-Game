namespace NumberGuessingGame.Helpers;

public static class InputHelper
{
    private static bool IsInputValid(string input)
    {
        if (input.Any(char.IsLetter) ||
            input.Any(char.IsSymbol) ||
            input.Any(char.IsPunctuation) ||
            input.Length == 0 ||
            input.Length > 10 ||
            input == null)
        {
            return false;
        }

        return true;
    }

    public static int ParseInput(string input)
    {
        if (!IsInputValid(input))
        {
            return 0;
        }

        input = input.Trim();
        int.TryParse(input, out int num);
        return num;
    }
}