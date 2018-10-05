using AmazonSimulator.Commands;
using Newtonsoft.Json;

namespace AmazonSimulator.Framework.Patterns
{
    /// <summary>
    ///     Event arguments when a model has changed data.
    /// </summary>
    public class ObservableModelArgs : ObservableArgs
    {
        /// <summary>
        ///     The model the change happened in.
        /// </summary>
        public string Model
        {
            get
            {
                return data.model;
            }

            set
            {
                data.model = value;
            }
        }

        /// <summary>
        ///     The field within the model that has changed.
        /// </summary>
        public string Field
        {
            get
            {
                return data.field;
            }

            set
            {
                data.field = value;
            }
        }

        /// <summary>
        ///     Convert to a json string.
        /// </summary>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(new string[]
            {
                Model,
                Field,
                Action,
                Content
            });
        }
    }
}
