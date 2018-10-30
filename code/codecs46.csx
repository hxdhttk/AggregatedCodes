public class LRUCache
{
    private int _capacity;
    private Dictionary<int, int> _storage;
    private List<int> _used;
    public LRUCache (int capacity)
    {
        _capacity = capacity;
        _storage = new Dictionary<int, int> ();
        _used = new List<int> (_capacity);
    }

    public int Get (int key)
    {
        if (_storage.TryGetValue (key, out var value))
        {
            _used.Remove (key);
            _used.Add (key);
            return value;
        }
        return -1;
    }

    public void Put (int key, int value)
    {
        _storage[key] = value;
        if (!_used.Contains (key))
        {
            _used.Add (key);
        }
        else
        {
            _used.Remove (key);
            _used.Add (key);
        }

        if (_used.Count > _capacity)
        {
            var keyToRemove = _used[0];
            _storage.Remove (keyToRemove);
            _used.Remove (keyToRemove);
        }
    }
}