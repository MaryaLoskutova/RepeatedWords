using System.Collections.Generic;

namespace TextAnalyzer.Service.BusinessObjects
{
    public class WordsOrderList
    {
        private WordNode _head;
        private WordNode _tail;
        
        public void AddToTail(WordNode wordNode)
        {
            if (wordNode.Next != null
                || wordNode.Previous != null)
            {
                return;
            }

            if (_tail != null)
            {
                _tail.Next = wordNode;
                wordNode.Previous = _tail;
            }

            _tail = wordNode;
            _head ??= wordNode;
        }
        
        public void Remove(WordNode wordNode)
        {
            Connect(wordNode.Previous, wordNode.Next);
            ActualizeBorders(wordNode);
            wordNode.Next = null;
            wordNode.Previous = null;
        }

        public List<WordInfo> SelectTop(int count)
        {
            var result = new List<WordInfo>();
            var current = _head;
            while (current != null
                   && count > 0)
            {
                result.Add(current.Value);
                current = current.Next;
                count--;
            }

            return result;
        }

        public bool Empty()
        {
            return _head == null;
        }

        private void ActualizeBorders(WordNode wordNode)
        {
            if (_head == wordNode)
            {
                _head = wordNode.Next;
            }

            if (_tail == wordNode)
            {
                _tail = wordNode.Previous;
            }
        }

        private static void Connect(WordNode prev, WordNode next)
        {
            if (prev != null)
            {
                prev.Next = next;
            }

            if (next != null)
            {
                next.Previous = prev;
            }
        }
    }
}