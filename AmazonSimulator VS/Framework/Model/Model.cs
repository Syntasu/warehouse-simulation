using AmazonSimulator.Framework.Patterns;

using System.Collections.Generic;
using System.Dynamic;

namespace AmazonSimulator.Framework
{
    public class Model : Observable, IModel
    {
        private List<ModelData> data = new List<ModelData>();

        public void RegisterData(params ModelData[] fields)
        {
            foreach (ModelData field in fields)
            {
                field.SetModel(this);
                data.Add(field);
            }
        }

        public void SetData(string name, dynamic value)
        {
            if(TryGetData(name, out ModelData field))
            {
                field.Value = value;
            }
        }

        public ModelData GetData(string name)
        {
            if(TryGetData(name, out ModelData field))
            {
                return field;
            }

            return null;
        }

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

        public void OnModelDataChanged(ModelData data)
        {
            dynamic payload = new ExpandoObject();
            payload.Name = data.Name;
            payload.Value = data.Value;

            Notify(payload);
        }
    }
}
