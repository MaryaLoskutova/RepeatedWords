using System.Collections.Generic;

namespace TextAnalyzer.Service.BusinessObjects
{
    public class WordsOrderList
    {
        private WordNode _head;
        private WordNode _tail;

        public void Actualize(WordNode wordNode)
        {
            AddToTail(wordNode);
            LiftToRightPosition(wordNode);
        }

        public List<WordInfo> SelectTop(int count)
        {
            var result = new List<WordInfo>();
            var current = _head;
            while (current != null
                   && count >= 0)
            {
                result.Add(current.Value);
                current = current.Next;
            }

            return result;
        }

        private void LiftToRightPosition(WordNode wordNode)
        {
            while (wordNode.Previous != null
                   && wordNode.Previous.Value.WordFrequency < wordNode.Value.WordFrequency)
            {
                Swap(wordNode);
            }
        }

        private void Swap(WordNode wordNode)
        {
            var prev = wordNode.Previous;
            if (prev.Previous != null)
            {
                Connect(prev.Previous, wordNode);
            }

            var next = wordNode.Next;
            if (next != null)
            {
                Connect(prev, next);
            }

            Connect(wordNode, prev);
            if (_head == prev)
            {
                _head = wordNode;
            }

            if (_tail == wordNode)
            {
                _tail = prev;
            }
        }

        private static void Connect(WordNode prev, WordNode next)
        {
            prev.Next = next;
            next.Previous = prev;
        }

        private void AddToTail(WordNode wordNode)
        {
            if (wordNode.Next != null || wordNode.Previous != null)
            {
                return;
            }

            if (_tail != null)
            {
                _tail.Next = wordNode;
            }

            _tail = wordNode;
            _head ??= wordNode;
        }
    }
}