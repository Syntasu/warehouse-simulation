﻿using System;
using System.Collections;
using System.Collections.Generic;

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
        ///     Indexer for ObservableList by int/index.
        /// </summary>
        /// <param name="index">The index we want to fetch.</param>
        /// <returns>Returns T as the given index.</returns>
        public T this[int index]
        {
            get
            {
                return State[index];
            }

            set
            {
                State[index] = value;

                //Notify the observer we added a new item.
                //TODO: Add delta compression, currently T does not implement IEquatable.
                Notify(new ObservableArgs()
                {
                    Content = value.ToString(),
                    Action = "modified"
                });
            }
        }

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
            Notify(new ObservableArgs() {
                Content = item.ToString(),
                Action = "add"
            });
        }

        /// <summary>
        ///     Clear out the collection.
        /// </summary>
        public void Clear()
        {
            //Before we clear out the list, notify to the observer of the removed items.
            foreach (T item in State)
            {
                Notify(new ObservableArgs()
                {
                    Content = item.ToString(),
                    Action = "remove"
                });
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
            Notify(new ObservableArgs()
            {
                Content = item.ToString(),
                Action = "remove"
            });

            return State.Remove(item);
        }

        /// <summary>
        ///     Fetch the index of an item. (return first occurence).
        /// </summary>
        /// <param name="item">The item we are looking for.</param>
        /// <returns>The index of the found item, -1 if nothing is found.</returns>
        public int IndexOf(T item)
        {
            int count = 0;
            foreach (T value in State)
            {
                //HACK: item.Equals(value) does not work because we can't compare T.
                //      Knowing that most T types implement their own custom ToString method
                //      We can abuse this fact to compare 2 T's.
                if (item.ToString() == value.ToString())
                {
                    return count;
                }

                count++;
            }

            return -1;
        }

        /// <summary>
        ///     Alias for ICollection<T>.GetEnumerator().
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return State.GetEnumerator();
        }
    }
}
