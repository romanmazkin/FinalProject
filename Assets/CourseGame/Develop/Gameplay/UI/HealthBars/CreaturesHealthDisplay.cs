using Assets.CourseGame.Develop.CommonUI;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.UI.HealthBars
{
    public class CreaturesHealthDisplay : MonoBehaviour
    {
        [SerializeField] private Transform _parent;

        private Camera _camera;
        private List<BarWithText> _bars = new();

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Attach(BarWithText bar)
        {
            bar.transform.SetParent(_parent);
            _bars.Add(bar); 
        }

        public void UnAttached(BarWithText bar)
        {
            if(bar.gameObject is not null)
                bar.transform.SetParent(null);

            _bars.Remove(bar);
        }

        public void UpdatePositionFor(BarWithText bar, Vector3 worldPosition)
        {
            Vector3 position = _camera.WorldToScreenPoint(worldPosition);

            bar.transform.position = position;
        }
    }
}
