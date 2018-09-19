namespace AmazonSimulator.Framework.Patterns
{
    public interface IObserver
    {
        /// <summary>
        ///     A method that gets called when the observable changed.
        /// </summary>
        /// <param name="payload">The arguments given from the observable.</param>
        void ObservableChanged(dynamic payload);
    }
}
