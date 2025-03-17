using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Features.InputFeature
{
    public class DesktopInput : IInputService
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        private Joystick _joystick;

        public bool IsEnabled { get ; set ; } = true;

        public DesktopInput(Joystick joystick)
        {
            _joystick = joystick;
        }

        public Vector3 Direction
        {
            get
            {
                if (IsEnabled == false)
                    return Vector3.zero;

                Vector3 direction = new Vector3(_joystick.Direction.x, 0, _joystick.Direction.y);

                if (direction == Vector3.zero)
                    direction = new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));

                return direction;
            }
        }
    }
}
