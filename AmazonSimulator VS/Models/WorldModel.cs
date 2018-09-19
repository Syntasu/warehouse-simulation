using AmazonSimulator.Data;
using AmazonSimulator.Framework;
using AmazonSimulator.Framework.Patterns;
using System;

namespace AmazonSimulator.Models
{
    public class WorldModel : Model
    {
        public ObservableList<Entity> Entities = new ObservableList<Entity>();
        public ObservableProperty<string> WorldName = new ObservableProperty<string>("memes");
        public ObservableProperty<string> WorldName2 = new ObservableProperty<string>("memes2");


        public WorldModel()
        {
            ModelObserveData(Entities);
            ModelObserveData(WorldName);
            ModelObserveData(WorldName2);
        }

        public void AddEntity<T>(Vector3 position, Vector3 rotation) where T : Entity
        {
            T entityModel = (T)Activator.CreateInstance(typeof(T), new object[] { });
            entityModel.SetEntityPosition(position);
            entityModel.SetEntityRotation(rotation);

            WorldName.Value = "edas";
            WorldName2.Value = "edas";

            Entities.Add(entityModel);
        }
    }
}
