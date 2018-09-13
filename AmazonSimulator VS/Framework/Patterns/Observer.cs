namespace AmazonSimulator.Framework.Patterns
{
    public abstract class Observer
    {
        /// <summary>
        ///     A method that gets called when the observable changed.
        /// </summary>
        /// <param name="payload">The arguments given from the observable.</param>
        public abstract void ObservableChanged(dynamic payload);
    }
}
