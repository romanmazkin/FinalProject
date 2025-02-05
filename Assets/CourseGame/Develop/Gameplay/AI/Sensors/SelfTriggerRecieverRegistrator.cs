using Assets.CourseGame.Develop.Gameplay.Entities;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.AI.Sensors
{
    public class SelfTriggerRecieverRegistrator : MonoEntityRegistrator
    {
        [SerializeField] private TriggerReciever _triggerReciever;

        public override void Register(Entity entity)
        {
            entity.AddSelfTriggerReciever(_triggerReciever);
        }
    }
}