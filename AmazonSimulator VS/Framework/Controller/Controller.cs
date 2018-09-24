using AmazonSimulator.Framework.Patterns;

using System.Collections.Generic;

namespace AmazonSimulator.Framework
{
    public abstract class Controller : IObserver
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

        /// <summary>
        ///     Implemented by the inheriting controller.
        ///     This abstract method receives all the changes from the model and view.
        /// </summary>
        /// <param name="observable">The observable that has changed.</param>
        /// <param name="command">The data accompanied by the observable.</param>
        public abstract void ObservableChanged(Observable observable, dynamic command);
    }
}
