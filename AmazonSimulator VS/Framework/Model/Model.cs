using AmazonSimulator.Framework.Patterns;

using System.Collections.Generic;
using System.Dynamic;

namespace AmazonSimulator.Framework
{
    public class Model : Observable, IModel
    {
        /// <summary>
        ///     A collection of all the data that is available in the model.
        /// </summary>
        private List<ModelData> data = new List<ModelData>();

        /// <summary>
        ///     Register a field or multiple fields to the model.
        ///     This will allow the model to receive events when data changes (observable).
        /// </summary>
        /// <param name="fields">The fields we want to register.</param>
        public void RegisterModelData(params ModelData[] fields)
        {
            foreach (ModelData field in fields)
            {
                field.SetModel(this);
                data.Add(field);
            }
        }

        /// <summary>
        ///     Set a specfic data for the model by a given name.
        /// </summary>
        /// <param name="name">The name of the field we want to modify.</param>
        /// <param name="value">The value we want to assign to the field.</param>
        public void SetData(string name, dynamic value)
        {
            if(TryGetData(name, out ModelData field))
            {
                field.Value = value;
            }
        }

        /// <summary>
        ///     Fetch a field in the model by a given name.
        /// </summary>
        /// <param name="name">The name of the field we want to fetch.</param>
        /// <returns>Get the data from the model by a given field.</returns>
        public ModelData GetData(string name)
        {
            if(TryGetData(name, out ModelData field))
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
        private bool TryGetData(string name, out ModelData field)
        {
            foreach (ModelData modelData in data)
            {
                if(modelData.Name == name)
                {
                    field = modelData;
                    return true;
                }
            }

            field = null;
            return false;
        }

        /// <summary>
        ///     Called when any of the model data has changed.
        ///     Proxy this through to the observable of the model.
        /// </summary>
        /// <param name="data"></param>
        public void OnModelDataChanged(ModelData data)
        {
            dynamic payload = new ExpandoObject();
            payload.Name = data.Name;
            payload.Value = data.Value;

            Notify(payload);
        }
    }
}
