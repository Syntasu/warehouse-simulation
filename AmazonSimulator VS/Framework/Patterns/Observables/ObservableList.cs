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

            dynamic payload = CreatePayload(item, "created");
            Notify(payload);
        }

        public void Clear()
        {
            foreach (T item in State)
            {
                dynamic payload = CreatePayload(item, "deleted");
                Notify(payload);
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
            dynamic payload = CreatePayload(item, "deleted");
            Notify(payload);

            return State.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return State.GetEnumerator();
        }

        private dynamic CreatePayload(T item, string opCode)
        {
            string json = JsonConvert.SerializeObject(item);

            dynamic payload = new ExpandoObject();
            payload.Name = GetType().Name;
            payload.Operation = opCode;
            payload.Value = item;

            return payload;
        }
    }
}
