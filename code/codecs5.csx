using System;
using System.Linq;
using System.Collections.Generic;

public static class Kata
{
  public static List<int> MultipleOfIndex(List<int> xs)
  {
    return xs.Where((num, index) => index == 0 ? false : num % index == 0).ToList();
  }
}