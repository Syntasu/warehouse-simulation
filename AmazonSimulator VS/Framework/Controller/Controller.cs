using AmazonSimulator.Commands;
using AmazonSimulator.Framework.Patterns;

using System.Collections.Generic;

namespace AmazonSimulator.Framework
{
    public class Controller : IObserver
    {
        private List<Observable> models = new List<Observable>();
        private List<Observable> views  = new List<Observable>();

        /// <summary>
        ///     Add a new model to this controller.
        /// </summary>
        /// <param name="model">Model instance we want to add.</param>
        public void AddModel(Model model)
        {
            model.Subscribe(this);
            models.Add(model);
        }

        /// <summary>
        ///     Add a new view to this controller.
        /// </summary>
        /// <param name="view">The view instance we want to add.</param>
        public void AddView(View view)
        {
            view.Subscribe(this);
            views.Add(view);
        }

        /// <summary>
        ///     Get a model reference from the controller.
        /// </summary>
        /// <typeparam name="T">The type of model we are looking to aquire.</typeparam>
        /// <returns>A model of type T or default(T) if not found.</returns>
        protected T GetModel<T>() where T : Model
        {
            foreach (Model model in models)
            {
                if(model is T)
                {
                    return model as T;
                }
            }

            return default(T);
        }

        public virtual void ObservableChanged(Command command)
        {
            System.Console.WriteLine($"{command.ToJson()}");
        }
    }
}
