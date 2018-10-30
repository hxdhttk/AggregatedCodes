using System.Text.RegularExpressions;

public class Runes
{
    public static int solveExpression (string expression)
    {
        var pattern = @"^(-?[1-9?][0-9?]*|-?0)([\+\-\*])(-?[1-9?][0-9?]*|-?0)=(-?[1-9?][0-9?]*|-?0)$";
        var match = Regex.Match (pattern, expression);
        if (!match.Success)
        {
            return -1;
        }

        var lhs = match.Groups[1].Value;
        var op = match.Groups[2].Value;
        var rhs = match.Groups[3].Value;
        var res = match.Groups[4].Value;

        for (var missingDigit = 0; missingDigit < 10; missingDigit++)
        {
            var newLhs = int.Parse (lhs.Replace ("?", missingDigit.ToString ()));
            var newRhs = int.Parse (rhs.Replace ("?", missingDigit.ToString ()));
            var newRes = int.Parse (res.Replace ("?", missingDigit.ToString ()));
            if (evaluate (newLhs, op, newRhs) == newRes)
            {
                return missingDigit;
            }
        }

        return -1;
    }

    private static int evaluate (int lhs, string op, int rhs)
    {
        switch (op)
        {
            case "+":
                return lhs + rhs;
            case "-":
                return lhs - rhs;
            case "*":
                return lhs * rhs;
        }

        throw new ArgumentException ($"Unknown op {op}.");
    }
}