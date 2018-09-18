using AmazonSimulator.Data;
using AmazonSimulator.Framework;
using System;
using System.Collections.Generic;

namespace AmazonSimulator.Models
{
    public class WorldModel : Model
    {
        public ModelData Entities = new ModelData("entities", new List<Entity>());

        public WorldModel()
        {
            RegisterModelData(Entities);
        }

        public void AddEntity<T>(Vector3 position, Vector3 rotation) where T : Entity
        {
            T entityModel = (T)Activator.CreateInstance(typeof(T), new object[] { });
            entityModel.SetEntityPosition(position);
            entityModel.SetEntityRotation(rotation);

            Entities.Value.Add(entityModel);
        }
    }
}
