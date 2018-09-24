using AmazonSimulator.Commands;
using Newtonsoft.Json;

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace AmazonSimulator.Framework.Patterns
{
    public class ObservableList<T> : Observable, ICollection<T>
    {
        public int Count => State.Count;
        public bool IsReadOnly => State.IsReadOnly;

        public ObservableList()
        {
            State = new List<T>();
        }

        public void Add(T item)
        {
            State.Add(item);


            Notify(new Command(item, CommandOpCodes.Created));
        }

        public void Clear()
        {
            foreach (T item in State)
            {
                Notify(new Command(item, CommandOpCodes.Deleted));
            }

            State.Clear();
        }

        public bool Contains(T item)
        {
            return State.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            State.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return State.GetEnumerator();
        }

        public bool Remove(T item)
        {
            Notify(new Command(item, CommandOpCodes.Deleted));
            return State.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return State.GetEnumerator();
        }
    }
}
