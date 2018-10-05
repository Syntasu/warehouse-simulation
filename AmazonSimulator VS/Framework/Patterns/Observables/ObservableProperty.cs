namespace AmazonSimulator.Framework.Patterns
{
    /// <summary>
    ///     Creates an property that can be observed by observers.
    /// </summary>
    /// <typeparam name="T">The type of the propertyy.</typeparam>
    public class ObservableProperty<T> : Observable
    {
        public T Value
        {
            get
            {
                return State;
            }
            set
            {
                T item = value;

                //HACK: item.Equals(value) does not work because we can't compare T.
                //      Knowing that most T types implement their own custom ToString method
                //      We can abuse this fact to compare 2 T's.
                if (item.ToString() != State.ToString())
                {
                    State = value;

                    //Notify any observers.
                    Notify(new ObservableArgs()
                    {
                        Content = State.ToString(),
                        Action = "modified"
                    });
                }
            }
        }

        /// <summary>
        ///     Assign ObservableProperty with default value.
        /// </summary>
        public ObservableProperty()
        {
            State = default(T);
        }

        /// <summary>
        ///     Assign ObservableProperty with a given value.
        /// </summary>
        /// <param name="value">Value we want to assign.</param>
        public ObservableProperty(T value)
        {
            State = value;
        }
    }
}
