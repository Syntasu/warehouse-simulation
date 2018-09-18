using System.Dynamic;

namespace AmazonSimulator.Framework.Patterns
{
    public class ObservableProperty<T> : Observable
    {
        private T _prop = default(T);
        public T Prop
        {
            get
            {
                return _prop;
            }
            set
            {
                if (value.Equals(_prop)) return;

                _prop = value; 

                dynamic payload = new ExpandoObject();
                payload.Name = GetType().Name;
                payload.Value = _prop;

                Notify(payload);
            }
        }
    }
}
