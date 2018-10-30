public class Solution
    {
        private Dictionary<string, HashSet<string>> _dict;

        private IList<IList<string>> _sentences;

        private string _beginWord;

        private int _minLength = -1;

        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            if (!wordList.Contains(endWord))
            {
                return new List<IList<string>>();
            }

            _dict = new Dictionary<string, HashSet<string>>();
            var wordListWithBeginWord = new List<string>(wordList);
            wordListWithBeginWord.Add(beginWord);
            foreach (var word in wordListWithBeginWord)
            {
                _dict[word] = new HashSet<string>();
                foreach (var anotherWord in wordListWithBeginWord)
                {
                    if (anotherWord == word)
                    {
                        continue;
                    }

                    if (IsOneLetterDifferent(word, anotherWord))
                    {
                        _dict[word].Add(anotherWord);
                    }
                }
            }

            _beginWord = beginWord;
            _sentences = new List<IList<string>>();
            FindSentences(beginWord, endWord, null);
            _minLength = -1;
            return _sentences;
        }

        private bool IsOneLetterDifferent(string lhs, string rhs)
        {
            var foundDifferentLetter = false;
            for (var index = 0; index < lhs.Length; index++)
            {
                if (lhs[index] != rhs[index])
                {
                    if (foundDifferentLetter)
                    {
                        return false;
                    }
                    foundDifferentLetter = true;
                }
            }

            return foundDifferentLetter;
        }

        private void FindSentences(string beginWord, string endWord, List<string> sentence)
        {
            if (beginWord == _beginWord)
            {
                sentence = new List<string>();
                sentence.Add(_beginWord);
            }

            var set = _dict[beginWord];
            foreach (var nextWord in set)
            {
                if (_minLength != -1 && sentence.Count + 1 > _minLength)
                {
                    return;
                }

                if (nextWord == endWord)
                {
                    var final = new List<string>(sentence);
                    final.Add(endWord);
                    if (_minLength == -1)
                    {
                        _minLength = final.Count;
                        _sentences.Add(final);
                        return;
                    }

                    if (final.Count < _minLength)
                    {
                        _minLength = final.Count;
                        _sentences = new List<IList<string>>();
                        _sentences.Add(final);
                        return;
                    }

                    _sentences.Add(final);
                    return;
                }

                if (sentence.Contains(nextWord))
                {
                    continue;
                }

                var temp = new List<string>(sentence);
                temp.Add(nextWord);
                FindSentences(nextWord, endWord, temp);
            }
        }
    }