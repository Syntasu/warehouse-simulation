namespace AmazonSimulator.Framework.Patterns
{
    public abstract class Observer
    {
        public abstract void OnObservableChanged(dynamic payload);
    }
}
