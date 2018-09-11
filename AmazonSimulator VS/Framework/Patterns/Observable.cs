﻿using System.Collections.Generic;

namespace AmazonSimulator.Framework.Patterns
{
    public class Observable
    {
        private List<Observer> observers = new List<Observer>();

        /// <summary>
        ///     Tell a observers to receive events from this observable.
        /// </summary>
        /// <param name="observer">The observer we want to add.</param>
        public void Subscribe(Observer observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        ///     Notify any observers that this observable has changed.
        /// </summary>
        /// <param name="payload">Any arguments we might need to pass.</param>
        public void Notify(dynamic payload)
        {
            foreach (Observer observer in observers)
            {
                observer.OnObservableChanged(payload);
            }
        }
    }
}