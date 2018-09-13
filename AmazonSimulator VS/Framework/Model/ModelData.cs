using System;

namespace AmazonSimulator.Framework
{
    public class ModelData
    {
        public string Name { get; set; }

        private dynamic _value;
        public dynamic Value
        {
            get
            {
                return _value;
            }
            set
            {
                //RESEARCH: Do we really need to check for 
                if (_value == value) return;

                _value = value;
                callback?.Invoke(this);
            }
        }
            
        /// <summary>
        ///     Callback to the model that has this ModelData.
        ///     
        /// </summary>
        Action<ModelData> callback = null;

        public ModelData(string name, dynamic value = null)
        {
            Name = name;
            _value = value;
        }

        /// <summary>
        ///     Set the model we should push the model data changes to.
        /// </summary>
        /// <param name="model">A model to listen for changes</param>
        public void SetModel(IModelDataListener model)
        {
            callback = model.OnModelDataChanged;
        }

    }
}
