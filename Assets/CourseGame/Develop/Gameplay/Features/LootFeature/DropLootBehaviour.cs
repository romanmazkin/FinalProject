using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Gameplay.Features.LootFeature
{
    public class DropLootBehaviour : IEntityInitialize, IEntityUpdate
    {
        private DropLootService _dropLootService;


        private ICompositeCondition _dropLootCondition;
        private ReactiveVariable<bool> _lootIsDropped;
        private Entity _entity;


        public DropLootBehaviour(DropLootService dropLootService)
        {
            _dropLootService = dropLootService;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _lootIsDropped = entity.GetLootIsDropped();
            _dropLootCondition = entity.GetDropLootCondition();
        }

        public void OnUpdate(float deltaTime)
        {
            if (_dropLootCondition.Evaluate())
            {
                DropLoot();
                _lootIsDropped.Value = true;
            }
        }

        private void DropLoot() => _dropLootService.DropLootFor(_entity);
    }
}
