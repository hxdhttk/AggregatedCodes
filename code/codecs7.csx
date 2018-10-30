using System;
using System.Collections.Generic;
using System.Linq;

public class Kata
{
  public static string Encrypt (string text, int n)
  {
    if (string.IsNullOrEmpty (text) || n <= 0)
    {
      return text;
    }

    var chars = text.ToCharArray ();
    for (var i = 0; i < n; i++)
    {
      var oddChars = new List<char> ();
      var evenChars = new List<char> ();

      for (var j = 0; j < chars.Length; j = j + 2)
      {
        if (j == chars.Length - 1)
        {
          oddChars.Add (chars[j]);
          break;
        }
        oddChars.Add (chars[j]);
        evenChars.Add (chars[j + 1]);
      }

      chars = evenChars.Concat (oddChars).ToArray ();
    }

    return new String (chars);
  }

  public static string Decrypt (string encryptedText, int n)
  {
    if (string.IsNullOrEmpty (encryptedText) || n <= 0)
    {
      return encryptedText;
    }

    var middlePos = encryptedText.Length / 2;
    var chars = encryptedText.ToCharArray ();
    for (var i = 0; i < n; i++)
    {
      var evenChars = new Queue<char> ();
      var oddChars = new Queue<char> ();

      for (var j = 0; j < chars.Length; j++)
      {
        if (j >= middlePos)
        {
          oddChars.Enqueue (chars[j]);
          continue;
        }
        evenChars.Enqueue (chars[j]);
      }

      var temp = new List<char> ();
      for (var j = 0; j < chars.Length; j = j + 2)
      {
        if (j == chars.Length - 1)
        {
          temp.Add (oddChars.Dequeue ());
          break;
        }

        temp.Add (oddChars.Dequeue ());
        temp.Add (evenChars.Dequeue ());
      }

      chars = temp.ToArray ();
    }

    return new String (chars);
  }
}