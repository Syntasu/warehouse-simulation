using AmazonSimulator.Commands;

namespace AmazonSimulator.Framework.Patterns
{
    public interface IObserver
    {
        /// <summary>
        ///     A method that gets called when the observable changed.
        /// </summary>
        /// <param name="arguments">The arguments given from the observable.</param>
        void ObservableChanged(Observable observable, ObservableArgs arguments);
    }
}
