using System;
using System.Collections.Generic;

public class RandomizedCollection {
    private readonly List<int> _elements;
    private int _count;
    private readonly Random _random;

    /** Initialize your data structure here. */
    public RandomizedCollection() {
        _elements = new List<int>();
        _count = 0;
        _random = new Random();
    }
    
    /** Inserts a value to the collection. Returns true if the collection did not already contain the specified element. */
    public bool Insert(int val) {
        if (_elements.Contains(val))
        {
            _elements.Add(val);
            _count = _elements.Count;
            return false;
        }
        _elements.Add(val);
        _count = _elements.Count;
        return true;
    }
    
    /** Removes a value from the collection. Returns true if the collection contained the specified element. */
    public bool Remove(int val) {
        if (_elements.Contains(val))
        {
            _elements.Remove(val);
            _count = _elements.Count;
            return true;
        }
        return false;
    }
    
    /** Get a random element from the collection. */
    public int GetRandom() {
        var index = _random.Next(0, _count);
        return _elements[index];
    }
}

public class RandomizedSet {
    private readonly List<int> _elements;
    private int _count;
    private readonly Random _random;

    /** Initialize your data structure here. */
    public RandomizedSet() {
        _elements = new List<int>();
        _count = 0;
        _random = new Random();
    }
    
    /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
    public bool Insert(int val) {
        if (_elements.Contains(val))
        {
            return false;
        }
        _elements.Add(val);
        _count = _elements.Count;
        return true;
    }
    
    /** Removes a value from the set. Returns true if the set contained the specified element. */
    public bool Remove(int val) {
        if (_elements.Contains(val))
        {
            _elements.Remove(val);
            _count = _elements.Count;
            return true;
        }
        return false;
    }
    
    /** Get a random element from the set. */
    public int GetRandom() {
        var index = _random.Next(0, _count);
        return _elements[index];
    }
}