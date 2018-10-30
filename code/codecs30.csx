public class Solution
    {
        public int ScoreOfParentheses(string S)
        {
            var stack = new Stack<int>();
            for(var index = 0; index < S.Length; index++)
            {
                if(S[index] == '(')
                {
                    stack.Push(-1);
                }

                if(S[index] == ')')
                {
                    if(stack.Peek() == -1)
                    {
                        stack.Pop();
                        stack.Push(1);
                        continue;
                    }

                    var tempResults = new List<int>();
                    while(stack.Peek() != -1)
                    {
                        tempResults.Add(stack.Pop());
                    }

                    if(stack.Peek() == -1)
                    {
                        stack.Pop();
                        stack.Push(2 * tempResults.Sum());
                        continue;
                    }

                    stack.Push(tempResults.Sum());
                }
            }

            return stack.Sum();
        }
    }