using System.Linq;
using System.Collections.Generic;

public class Solution
{
    private int _maxI;
    private int _maxJ;

    public int MaxAreaOfIsland (int[, ] grid)
    {
        var areas = new List<int> ();
        _maxI = grid.GetUpperBound (0);
        _maxJ = grid.GetUpperBound (1);
        for(var i = 0; i < _maxI; i++)
        {
            for(var j = 0; j < _maxJ; j++)
            {
                if(grid[i, j] == 1)
                {
                    int area = 0;
                    FourDirectionalWalk(grid, i, j, ref area);
                    areas.Add(area);
                }
            }
        }

        return areas.Max();
    }

    private void FourDirectionalWalk (int[, ] grid, int i, int j, ref int area)
    {
        if (i < 0 || j < 0 || i > _maxI || j > _maxJ)
        {
            return;
        }

        if (grid[i, j] == 1)
        {
            area += 1;
            grid[i, j] = -1;
        }
        else
        {
            return;
        }

        FourDirectionalWalk(grid, i + 1, j, ref area);
        FourDirectionalWalk(grid, i, j + 1, ref area);
        FourDirectionalWalk(grid, i - 1, j, ref area);
        FourDirectionalWalk(grid, i, j - 1, ref area);
    }
}