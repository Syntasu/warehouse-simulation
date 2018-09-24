﻿using AmazonSimulator.Framework.Patterns.Serialization;
using System.Dynamic;

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
                if (value.Equals(State))
                {
                    return;
                }
                else
                {
                    State = value; 
                }

                dynamic payload = new ExpandoObject();
                payload.action = "modified";
                payload.content = State;

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
