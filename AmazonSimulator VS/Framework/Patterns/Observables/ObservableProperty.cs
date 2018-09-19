using System.Dynamic;

namespace AmazonSimulator.Framework.Patterns
{
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
                if (value.Equals(State))
                {
                    return;
                }
                else
                {
                    State = value; 
                }

                dynamic payload = new ExpandoObject();
                payload.Operation = "modified";
                payload.Content = State;

                Notify(payload);
            }
        }

        public ObservableProperty()
        {
            State = default(T);
        }

        public ObservableProperty(T value)
        {
            State = value;
        }
    }
}
