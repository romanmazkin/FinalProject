using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.InputFeature
{
    public interface IInputService
    {
        bool IsEnabled { get; set; }

        Vector3 Direction { get; }
    }
}
