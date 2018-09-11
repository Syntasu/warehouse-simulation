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
                _value = value;
                callback?.Invoke(this);
            }
        }

        Action<ModelData> callback = null;

        public ModelData(string name, dynamic value = null)
        {
            Name = name;
            _value = value;
        }

        public void SetModel(IModel model)
        {
            callback = model.OnModelDataChanged;
        }

    }
}
