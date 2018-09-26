using System.Dynamic;

namespace AmazonSimulator.Framework.Patterns
{
    /// <summary>
    ///     Event arguments when an observable has changed.
    /// </summary>
    public class ObservableArgs
    {
        /// <summary>
        ///     Convient wrapper to set the content;
        /// </summary>
        public string Content
        {
            get
            {
                return data.content;
            }
            set
            {
                data.content = value;
            }
        }


        /// <summary>
        ///     The action we want to perform for the changed field.
        /// </summary>
        public string Action
        {
            get
            {
                return data.action;
            }

            set
            {
                data.action = value;
            }
        }

        /// <summary>
        ///     Contains the raw data of the observable arguments.
        /// </summary>
        protected dynamic data = new ExpandoObject();
    }
}
