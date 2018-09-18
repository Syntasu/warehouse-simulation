using AmazonSimulator.Framework.Data;
using AmazonSimulator.Framework.Patterns;
using System.Collections.Generic;
using System.Dynamic;

namespace AmazonSimulator.Framework
{
    public class Model : CanBeObserved, ICanObserve
    {
        /// <summary>
        ///     A collection of all the data that is available in the model.
        /// </summary>
        private IDictionary<string, CanBeObserved> fields = new Dictionary<string, CanBeObserved>();

        /// <summary>
        ///     Set or create a piece of data for the model.
        ///     This data will be observed by the model for any changes.
        /// </summary>
        /// <param name="name">The name of the field we want to modify.</param>
        /// <param name="value">The value we want to assign to the field.</param>
        public void SetField(string name, dynamic value)
        {
            if (!TryGetField(name, out CanBeObserved obs))
            {
                observable.Subscribe(this);
                fields.Add(name, observable)
            }
            else
            {
                fields[name] = 
            }



        }

        /// <summary>
        ///     Fetch a field in the model by a given name.
        /// </summary>
        /// <param name="name">The name of the field we want to fetch.</param>
        /// <returns>Get the data from the model by a given field.</returns>
        public ModelData GetData(string name)
        {
            if(TryGetField(name, out ModelData field))
            {
                return field;
            }

            return null;
        }

        /// <summary>
        ///     A helper method for fetch a model data field for a given name.
        /// </summary>
        /// <param name="name">Name of the field.</param>
        /// <param name="field">Out variable for the field we have (potentially) found.</param>
        /// <returns>A boolean wether the model data was found or not.</returns>
        private bool TryGetField(string name, out CanBeObserved fieldInstance)
        {
            foreach (var field in fields)
            {
                if(field.Key == name)
                {
                    fieldInstance = field.Value;
                    return true;
                }
            }

            fieldInstance = null;
            return 
        }

        /// <summary>
        ///     Called when any of the model data has changed.
        ///     Proxy this through to the observable of the model.
        /// </summary>
        /// <param name="data"></param>
        public void OnModelDataChanged(ModelData data)
        {
            dynamic payload = new ExpandoObject();
            payload.Type = MvcEventType.ModelDataChange;
            payload.Name = data.Name;
            payload.Value = data.Value;

            Notify(payload);
        }

        public void ObservableChanged(dynamic payload)
        {
            throw new System.NotImplementedException();
        }
    }
}
