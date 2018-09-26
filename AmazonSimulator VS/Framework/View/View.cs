using AmazonSimulator.Framework.Patterns;
using System;
using System.Collections.Generic;

namespace AmazonSimulator.Framework
{
    public class View : Observable, IObserver
    {
        protected List<Observable> controllers = new List<Observable>();

        public void AddController(Observable controller)
        {
            if(!controllers.Contains(controller))
            {
                controller.Subscribe(this);
                controllers.Add(controller);
            }
        }

        public virtual void ObservableChanged(Observable observable, ObservableArgs arguments)
        {
            //Console.WriteLine("Oh a controller changed: " + arguments.Content);
        }
    }
}
