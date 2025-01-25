using Assets.CourceGame.Develop.DI;
using Assets.CourseGame.Develop.CommonServices.AssetsManagement;
using Assets.CourseGame.Develop.Gameplay.Features.MovementFeature;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;


namespace Assets.CourseGame.Develop.Gameplay.Entities
{
    public class EntityFactory
    {
        private string _ghostPrefabPath = "Gameplay/Creatures/Ghost";

        private DIContainer _container;
        private ResourcesAssetLoader _assets;

        public EntityFactory(DIContainer container)
        {
            _container = container;
            _assets = container.Resolve<ResourcesAssetLoader>();
        }

        public Entity CreateGhost(Vector3 position)
        {
            Entity prefab = _assets.LoadResource<Entity>(_ghostPrefabPath);

            Entity instance = Object.Instantiate(prefab, position, Quaternion.identity, null);

            instance.AddValue(EntityValues.MoveDirection, new ReactiveVariable<Vector3>())
                .AddValue(EntityValues.MoveSpeed, new ReactiveVariable<float>(10))
                .AddValue(EntityValues.RotationDirection, new ReactiveVariable<Vector3>())
                .AddValue(EntityValues.RotationSpeed, new ReactiveVariable<float>(900))
                .AddValue(EntityValues.Transform, instance.GetComponent<Transform>())
                .AddValue(EntityValues.CharacterController, instance.GetComponent<CharacterController>());

            instance
                .AddBehaviour(new CharacterControllerMovementBehaviour())
                .AddBehaviour(new RotationBehaviour());

            instance.Initialize();

            return instance;
        }
    }
}
