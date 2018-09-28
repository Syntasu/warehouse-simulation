using AmazonSimulator.Data;
using AmazonSimulator.Framework;
using AmazonSimulator.Framework.Patterns;
using System;
using System.Linq;

namespace AmazonSimulator.Models
{
    public class WorldModel : Model
    {
        /// <summary>
        ///     A counter to assign a unique ID to every entity in the world.
        /// </summary>
        private ushort entityIdCounter = 0;

        /// <summary>
        ///     A list of all the entities in the system.
        /// </summary>
        public ObservableList<Entity> Entities = new ObservableList<Entity>();
        public ObservableProperty<string> WorldName = new ObservableProperty<string>();

        /// <summary>
        ///     Constructor, set the model name and register the observables to the base model.
        /// </summary>
        public WorldModel() : base("world")
        {
            ModelObserveData(nameof(Entities), Entities);
            ModelObserveData(nameof(WorldName), WorldName);
        }

        /// <summary>
        ///     Add a new entity to the world.
        /// </summary>
        /// <typeparam name="T">Type of the entity we want to spawn.</typeparam>
        /// <param name="position">Position to spawn the entity at.</param>
        /// <param name="rotation">The rotation to give the entity.</param>
        public ushort AddEntity<T>(Vector3 position, Vector3 rotation) where T : Entity
        {
            entityIdCounter++;

            T entity = (T)Activator.CreateInstance(typeof(T), new object[] { entityIdCounter });
            entity.SetEntityPosition(position);
            entity.SetEntityRotation(rotation);

            Entities.Add(entity);
            return entityIdCounter;
        }

        /// <summary>
        ///     Get an entity from the world by the given entity id.
        /// </summary>
        /// <typeparam name="T">The type of entity we fetched.</typeparam>
        /// <param name="id">THe Id we want to search for.</param>
        /// <returns>A entity or default(T).</returns>
        public T GetEntity<T>(ushort entityId) where T : Entity
        {
            return (T)Entities.FirstOrDefault(entity => entity.EntityId == entityId);
        }

        /// <summary>
        /// /   Update an entity in the entities list, this is required
        ///     so the updates get passed along the model to the controller -> view.
        /// </summary>
        /// <typeparam name="T">Type of the entity.</typeparam>
        /// <param name="entity"></param>
        public void UpdateEntity(Entity entity)
        {
            int index = Entities.IndexOf(entity);
            Entities[index] = entity;
        }

    }
}
