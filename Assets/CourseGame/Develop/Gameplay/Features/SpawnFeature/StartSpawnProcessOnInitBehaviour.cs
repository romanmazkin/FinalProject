using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;

namespace Assets.CourseGame.Develop.Gameplay.Features.SpawnFeature
{
    public class StartSpawnProcessOnInitBehaviour : IEntityInitialize
    {
        public void OnInit(Entity entity)
        {
            entity.GetIsSpawningProcess().Value = true;
        }
    }
}