using AmazonSimulator.Commands;
using System.Collections.Generic;

namespace AmazonSimulator.Framework.Patterns
{ 
    public class Observable
    {
        /// <summary>
        ///     The internal state of the observable.
        /// </summary>
        public dynamic State { get; set; }

        /// <summary>
        ///     A collection of observers currently observing this observable.
        /// </summary>
        private List<IObserver> observers = new List<IObserver>();

        /// <summary>
        ///     Tell a observers to receive events from this observable.
        /// </summary>
        /// <param name="observer">The observer we want to add.</param>
        public void Subscribe(IObserver observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        ///     Notify any observers that this observable has changed.
        /// </summary>
        /// <param name="payload">Any arguments we might need to pass.</param>
        protected void Notify(dynamic command)
        {
            foreach (IObserver observer in observers)
            {
                observer.ObservableChanged(this, command);
            }
        }
    }
}
