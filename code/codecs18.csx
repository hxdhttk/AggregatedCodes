using System.Linq;
using System.Collections.Generic;

public class MapSum {
    private readonly Dictionary<string, int> _dict;

    /** Initialize your data structure here. */
    public MapSum() {
        _dict = new Dictionary<string, int>();
    }
    
    public void Insert(string key, int val) {
        _dict[key] = val;
    }
    
    public int Sum(string prefix) {
        return _dict.Sum(kv => kv.Key.StartsWith(prefix) ? kv.Value : 0);
    }
}