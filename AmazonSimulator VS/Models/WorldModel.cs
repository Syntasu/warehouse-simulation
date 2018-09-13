using AmazonSimulator.Data;
using AmazonSimulator.Framework;
using System;
using System.Collections.Generic;

namespace AmazonSimulator.Models
{
    public class WorldModel : Model
    {
        public ModelData Entities = new ModelData("entities", new List<EntityModel>());

        public WorldModel()
        {
            RegisterModelData(Entities);
        }

        public void AddEntity<T>(Vector3 position, Vector3 rotation) where T : EntityModel
        {
            EntityModel entity = (EntityModel)Activator.CreateInstance(typeof(T), new object[] { });
            entity.SetEntityPosition(position);
            entity.SetEntityRotation(rotation);

            Entities.Value.Add(entity);
        }
    }
}
