using AmazonSimulator.Framework.Patterns;

namespace AmazonSimulator.Framework
{
    public class Model : Observable, IObserver
    {
        /// <summary>
        ///     Tell the model to observe the given ObservableList.
        ///     This piece of data is now part of the model.
        /// </summary>
        /// <param name="observable">An observable we want to observe.</param>
        public void ModelObserveData(Observable observable)
        {
            observable.Subscribe(this);
        }

        /// <summary>
        ///     Tell the model to observer multiple ObservableLists.
        ///     This piece of data will now become a part of the model data.
        /// </summary>
        /// <param name="observables">An array of observables we want to observe.</param>
        public void ModelObserveDatas(params Observable[] observables)
        {
            foreach (Observable observable in observables)
            {
                ModelObserveData(observable);
            }
        }

        /// <summary>
        ///     A listener for when any of the observed observables are changed.
        ///     This method will proxy the request through to the controller (who is observing the model).
        /// </summary>
        /// <param name="payload"></param>
        public void ObservableChanged(dynamic payload)
        {
            Notify(payload);
        }
    }   
}
