using AmazonSimulator.Framework.Patterns;

using System;
using System.Collections.Generic;

namespace AmazonSimulator.Framework
{
    public class Controller : IObserver
    {
        private List<Observable> models = new List<Observable>();
        //private List<Observable> views  = new List<Observable>();

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

        //public void AddView(View view)
        //{
        //    view.Subscribe(this);
        //    views.Add(view);
        //}

        //public override void ObservableChanged(dynamic payload)
        //{
        //    MvcEventType type = (MvcEventType)payload.Type;

        //    switch (type)
        //    {
        //        case MvcEventType.ModelDataChange:
        //            NotifyViews(payload);
        //            break;
        //        case MvcEventType.ViewAction:
        //            NotifyModels(payload);
        //            break;

        //    }
        //}

        public virtual void ObservableChanged(dynamic payload)
        {
            Console.WriteLine($"[Name: {payload.Name}, Value: {payload.Value}, Operation: {payload.Operation}]");
        }
        //private void NotifyViews(dynamic payload)
        //{
        //    foreach (View view in views)
        //    { 
        //        view.OnModelChanged(payload);
        //    }
        //}

        private void NotifyModels(dynamic payload)
        {
            foreach (Model model in models)
            {
                //model.OnModelDataChanged
            }
        }
    }
}
