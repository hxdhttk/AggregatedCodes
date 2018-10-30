using System;
using System.Collections.Generic;
using System.Linq;

public static string formatDuration (int seconds)
{
    if (seconds == 0)
    {
        return "now";
    }
    var timeSpan = TimeSpan.FromSeconds (seconds);
    var years = timeSpan.Days / 365;
    var days = timeSpan.Days % 365;
    var hours = timeSpan.Hours;
    var minutes = timeSpan.Minutes;
    var secs = timeSpan.Seconds;

    var segs = new List<string> ();
    if (years != 0)
    {
        if (years == 1)
        {
            segs.Add ($"{years} year");
        }
        else
        {
            segs.Add ($"{years} years");
        }
    }
    if (days != 0)
    {
        if (days == 1)
        {
            segs.Add ($"{days} day");
        }
        else
        {
            segs.Add ($"{days} days");
        }
    }
    if (hours != 0)
    {
        if (hours == 1)
        {
            segs.Add ($"{hours} hour");
        }
        else
        {
            segs.Add ($"{hours} hours");
        }
    }
    if (minutes != 0)
    {
        if (minutes == 1)
        {
            segs.Add ($"{minutes} minute");
        }
        else
        {
            segs.Add ($"{minutes} minutes");
        }
    }
    if (secs != 0)
    {
        if (secs == 1)
        {
            segs.Add ($"{secs} second");
        }
        else
        {
            segs.Add ($"{secs} seconds");
        }
    }

    if (segs.Count == 1)
    {
        return segs.Single ();
    }
    if (segs.Count == 2)
    {
        return $"{segs[0]} and {segs[1]}";
    }

    var sb = new StringBuilder ();
    for (var index = 0; index < segs.Count - 1; index++)
    {
        sb.Append (segs[index] + ", ");
    }
    sb.Remove (sb.Length - 2, 2);
    sb.Append (" and " + segs.Last ());
    return sb.ToString ();
}

public static string High (string s)
{
    var max = s.Split (' ')
        .Select (CalculateScore)
        .Max ();
    return s.Split (' ').First (str => CalculateScore (str) == max);
}

public static int CalculateScore (string s)
{
    return s.Select (c => (int) (c - 'a') + 1)
        .Sum ();
}

public static bool IsPronic (long n)
{
    if (n < 0)
    {
        return false;
    }
    var root = (long) Math.Sqrt (n);
    return root * (root + 1) == n;
}

