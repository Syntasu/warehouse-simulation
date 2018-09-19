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
                payload.Name = GetType().FullName;
                payload.Operation = "modified";
                payload.Value = State;

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
