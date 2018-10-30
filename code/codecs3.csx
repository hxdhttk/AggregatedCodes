using System.Collections.Generic;

public class LoopDetector
{
  public class Node
  {
    public LoopDetector.Node next { get; set; }
  }
}

public class Kata
{
  public static int getLoopSize (LoopDetector.Node startNode)
  {
    var travelled = new Dictionary<LoopDetector.Node, int>();
    var temp = startNode;
    var pos = 0;
    while(true)
    {
      if(travelled.ContainsKey(temp))
      {
        var cycleStartPos = travelled[temp];
        return travelled.Count - cycleStartPos;
      }
      travelled.Add(temp, pos++);
      temp = temp.next;
    }
  }
}