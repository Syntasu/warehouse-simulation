using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

namespace AmazonSimulator.Framework.Patterns
{
    /// <summary>
    ///     Creates a new list that can be observed by an observer or observers.
    /// </summary>
    /// <typeparam name="T">The type of the list </typeparam>
    public class ObservableList<T> : Observable, ICollection<T>
    {
        /// <summary>
        ///     Return the count of items in the ObservableList.
        /// </summary>
        public int Count => State.Count;

        /// <summary>
        ///     Return wether the Observable list is readonly.
        /// </summary>
        public bool IsReadOnly => State.IsReadOnly;

        /// <summary>
        ///     Constructor, make the internal state a new list of type T.
        /// </summary>
        public ObservableList()
        {
            State = new List<T>();
        }

        /// <summary>
        ///     Add a new item to the collection.
        /// </summary>
        /// <param name="item">Item we want to add.</param>
        public void Add(T item)
        {
            State.Add(item);

            //Notify the observer we added a new item.
            Notify(CreatePacket(item, "add"));
        }

        /// <summary>
        ///     Clear out the collection.
        /// </summary>
        public void Clear()
        {
            //Before we clear out the list, notify to the observer of the removed items.
            foreach (T item in State)
            { 
                Notify(CreatePacket(item, "remove"));
            }

            State.Clear();
        }

        /// <summary>
        ///     Check if a collection contains a item.
        /// </summary>
        /// <param name="item">The item we want to check against.</param>
        /// <returns>A boolean wether the collections contains the given item.</returns>
        public bool Contains(T item)
        {
            return State.Contains(item);
        }

        /// <summary>
        ///     Copy the list to an array.
        /// </summary>
        /// <param name="array">The array to copy to.</param>
        /// <param name="arrayIndex">The index we want to start at.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            State.CopyTo(array, arrayIndex);
        }

        /// <summary>
        ///     Return the enumerator of the list.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return State.GetEnumerator();
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            //Notify observer something was removed from the collection
            Notify(CreatePacket(item, "remove"));

            return State.Remove(item);
        }

        /// <summary>
        ///     Alias for ICollection<T>.GetEnumerator().
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return State.GetEnumerator();
        }

        /// <summary>
        ///     Create a new packet to notify observers with.
        /// </summary>
        /// <param name="item">Item in the collection that got operated on.</param>
        /// <param name="action">The action that has occured.</param>
        /// <returns>A dynamic containing the nessacarry</returns>
        private dynamic CreatePacket(T item, string action)
        {
            dynamic packet = new ExpandoObject();
            packet.content = item;
            packet.action = action;

            return packet;
        }
    }
}
