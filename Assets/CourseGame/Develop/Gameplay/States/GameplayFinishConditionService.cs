using Assets.CourseGame.Develop.Utils.Conditions;

namespace Assets.CourseGame.Develop.Gameplay.States
{
    public class GameplayFinishConditionService
    {
        public ICompositeCondition WinCondition { get; } = new CompositeCondition(LogicOperations.AndOperation);
        public ICompositeCondition DefeatCondition { get; } = new CompositeCondition(LogicOperations.AndOperation);
    }
}
