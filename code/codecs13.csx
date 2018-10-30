using System;
using System.Collections.Generic;

public class Solution
    {
        public IList<string> LetterCasePermutation(string S)
        {
            var indices = new List<int>();
            for (var index = 0; index < S.Length; index++)
            {
                if (char.IsLetter(S[index]))
                {
                    indices.Add(index);
                }
            }

            var ret = new List<string>();
            var patternCount = 1 << indices.Count;
            for (var i = 0; i < patternCount; i++)
            {
                var toAdd = S.ToCharArray();
                for (var j = 0; j < indices.Count; j++)
                {
                    if ((i & (1 << j)) > 0)
                    {
                        toAdd[indices[j]] = char.ToUpper(toAdd[indices[j]]);
                    }
                    else
                    {
                        toAdd[indices[j]] = char.ToLower(toAdd[indices[j]]);
                    }
                }
                ret.Add(new String(toAdd));
            }

            return ret;
        }
    }