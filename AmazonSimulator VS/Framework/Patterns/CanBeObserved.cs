using System.Collections.Generic;

namespace AmazonSimulator.Framework.Patterns
{
    public class CanBeObserved
    {
        /// <summary>
        ///     A collection of observers currently observing this observable.
        /// </summary>
        private List<ICanObserve> observers = new List<ICanObserve>();

        /// <summary>
        ///     Tell a observers to receive events from this observable.
        /// </summary>
        /// <param name="observer">The observer we want to add.</param>
        public void Subscribe(ICanObserve observer)
        {
            observers.Add(observer);
        }

        /// <summary>
        ///     Notify any observers that this observable has changed.
        /// </summary>
        /// <param name="payload">Any arguments we might need to pass.</param>
        protected void Notify(dynamic payload)
        {
            foreach (ICanObserve observer in observers)
            {
                observer.ObservableChanged(payload);
            }
        }
    }
}
