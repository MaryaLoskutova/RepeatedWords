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
                   && count > 0)
            {
                result.Add(current.Value);
                current = current.Next;
                count--;
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
            
            Connect(prev.Previous, wordNode);
            Connect(prev, wordNode.Next);
            Connect(wordNode, prev);
            
            ActualizeBorders(wordNode, prev);
        }

        private void ActualizeBorders(WordNode wordNode, WordNode prev)
        {
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
            if (prev != null)
            {
                prev.Next = next;
            }

            if (next != null)
            {
                next.Previous = prev;
            }
        }

        private void AddToTail(WordNode wordNode)
        {
            if (wordNode.Next != null
                || wordNode.Previous != null
                || wordNode.Value.WordFrequency > 1)
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
    }
}