using System;
using System.Collections.Generic;
using AmazonSimulator.Data;
using AmazonSimulator.Models;
using Controllers;

namespace Models
{
    public class _WorldModel : IObservable<Command>, IUpdatable
    {
        private List<EntityModel> entities = new List<EntityModel>();
        private List<IObserver<Command>> observers = new List<IObserver<Command>>();

        public _WorldModel()
        {
            RobotModel robot = CreateEntity<RobotModel>();
            robot.SetEntityPosition(new Vector3(0.0f, 0.0f, 0.0f));
            robot.SetEntityRotation(new Vector3(4.6f, 0.0f, 13.0f));
        }

        private T CreateEntity<T>() where T : EntityModel
        {
            EntityModel entity = new EntityModel(EntityType.Entity);
            entities.Add(entity);
            return entity as T;
        }

        public IDisposable Subscribe(IObserver<Command> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);

                SendCreationCommandsToObserver(observer);
            }

            return new Unsubscriber<Command>(observers, observer);
        }

        private void SendCommandToObservers(Command c)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].OnNext(c);
            }
        }

        private void SendCreationCommandsToObserver(IObserver<Command> obs)
        {
            foreach (RobotModel m3d in entities)
            {
                obs.OnNext(new UpdateModel3DCommand(m3d));
            }
        }

        public bool Update(int tick)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                EntityModel entity = entities[i];

                if (entity is IUpdatable)
                {
                    bool needsCommand = ((IUpdatable)entity).Update(tick);

                    if (needsCommand)
                    {
                        //SendCommandToObservers(new UpdateModel3DCommand(entity));
                    }
                }
            }

            return true;
        }
    }

    internal class Unsubscriber<Command> : IDisposable
    {
        private List<IObserver<Command>> _observers;
        private IObserver<Command> _observer;

        internal Unsubscriber(List<IObserver<Command>> observers, IObserver<Command> observer)
        {
            _observers = observers;
            _observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}