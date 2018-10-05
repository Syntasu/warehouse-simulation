using AmazonSimulator.Framework.Patterns;
using System;
using System.Collections.Generic;

namespace AmazonSimulator.Framework
{
    /// <summary>
    ///     A basic implementation of a model.
    ///     The model contains a set of observables which act as field.
    ///     It will intercept any changes in the observables and pass it along
    ///     to anyone observing the model.
    /// </summary>
    public class Model : Observable, IObserver
    {
        /// <summary>
        ///     The "human readable" name of the model, used for observable.
        /// </summary>
        private string modelName;

        /// <summary>
        ///     Store the observables and their names, so we can identify the observable by name 
        ///     when the observable changes value.
        /// </summary>
        private IDictionary<Observable, string> observables = new Dictionary<Observable, string>();

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="modelName">The name given to this model.</param>
        public Model(string modelName)
        {
            this.modelName = modelName;
        }

        /// <summary>
        ///     Tell the model to observe the given Observable.
        ///     This piece of data is now part of the model.
        /// </summary>
        /// <param name="observable">An observable we want to observe.</param>
        public void ModelObserveData(string name, Observable observable)
        {
            observable.Subscribe(this);
            observables.Add(observable, name.ToLower());  
        }

        /// <summary>
        ///     A listener for when any of the observed observables are changed.
        ///     This method will proxy the request through to the controller (who is observing the model).
        /// </summary>
        /// <param name="payload"></param>
        public void ObservableChanged(Observable observable, ObservableArgs args)
        {
            bool success = observables.TryGetValue(observable, out string name);

            if(success)
            {
                ObservableModelArgs modelArgs = new ObservableModelArgs
                {
                    Model = modelName,
                    Field = name,
                    Action = args.Action,
                    Content = args.Content
                };

                Notify(modelArgs);
            }
            else
            {
                //Something really bad went on here, should logically not be possible.
                throw new Exception("The model is observing a observable but is not registered to the model, huh!?");
            }
        }
    }   
}
