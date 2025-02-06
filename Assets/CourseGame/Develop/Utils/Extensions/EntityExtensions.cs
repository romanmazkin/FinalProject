using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Utils.Reactive;

namespace Assets.CourseGame.Develop.Utils.Extensions
{
    public static class EntityExtensions   
    {
        public static bool TryTakeDamage(this Entity entity, float damage)
        {
            if (entity.TryGetTakeDamageRequest(out ReactiveEvent<float> damageRequest))
            {
                damageRequest?.Invoke(damage);
                return true;
            }

            return false;
        }

        public static bool TryTakeDamage(this Entity entity, float damage, int sourceDamageTeam)
        {
            if(entity.TryGetTeam(out ReactiveVariable<int> entityTeam) == false)
                return false;

            if(entityTeam.Value == sourceDamageTeam)
                return false;

            return entity.TryTakeDamage(damage);
        }
    }
}
