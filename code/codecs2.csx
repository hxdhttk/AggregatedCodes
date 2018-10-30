using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface Instr { }
public struct MoveR : Instr { }
public struct MoveL : Instr { }
public struct Flip : Instr { }
public struct JumpF : Instr { public int ToPoint { get; set; } }
public struct JumpB : Instr { public int ToPoint { get; set; } }
public struct Nop : Instr { }

public static class Smallfuck
{
    public static string Interpreter (string code, string tape)
    {
        var exprs = SolveExpr (code);
        var exprsLength = exprs.Length;
        var memory = tape.ToCharArray ();
        var memoryLength = memory.Length;
        var curr = 0;
        var index = 0;

        while (true)
        {
            if (index >= exprsLength || curr < 0 || curr >= memoryLength)
            {
                break;
            }

            if (exprs[index] is MoveR)
            {
                curr += 1;
                index += 1;
            }
            else if (exprs[index] is MoveL)
            {
                curr -= 1;
                index += 1;
            }
            else if (exprs[index] is Flip)
            {
                memory[curr] = Flip (memory[curr]);
                index += 1;
            }
            else if (exprs[index] is JumpF)
            {
                if (memory[curr] == '0')
                {
                    var instr = (JumpF) exprs[index];
                    index = instr.ToPoint;
                    index += 1;
                }
                else
                {
                    index += 1;
                }
            }
            else if (exprs[index] is JumpB)
            {
                if (memory[curr] == '1')
                {
                    var instr = (JumpB) exprs[index];
                    index = instr.ToPoint;
                }
                else
                {
                    index += 1;
                }
            }
            else if (exprs[index] is Nop)
            {
                index += 1;
            }
        }

        return memory.Aggregate (new StringBuilder (), (sb, c) => sb.Append (c), sb => sb.ToString ());
    }

    private static char Flip (char ch)
    {
        if (ch == '0')
        {
            return '1';
        }
        return '0';
    }

    private static Instr[] SolveExpr (string exprStr)
    {
        var exprLength = exprStr.Length;
        var ret = new Instr[exprLength];
        var codes = exprStr.ToCharArray ();

        var backPoints = new Stack<int> ();
        for (var pos = 0; pos < exprLength; pos++)
        {
            switch (codes[pos])
            {
                case '>':
                    {
                        ret[pos] = new MoveR ();
                        break;
                    }
                case '<':
                    {
                        ret[pos] = new MoveL ();
                        break;
                    }
                case '*':
                    {
                        ret[pos] = new Flip ();
                        break;
                    }
                case '[':
                    {
                        backPoints.Push (pos);
                        break;
                    }
                case ']':
                    {
                        var jumpBackTo = backPoints.Pop ();
                        ret[pos] = new JumpB { ToPoint = jumpBackTo };
                        ret[jumpBackTo] = new JumpF { ToPoint = pos };
                        break;
                    }
                default:
                    {
                        ret[pos] = new Nop();
                        break;
                    }
            }
        }

        return ret;
    }
}