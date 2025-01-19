using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.CourseGame.Develop.CommonUI
{
    public class IconWithTextListView : MonoBehaviour
    {
        [SerializeField] private IconWithText _iconWithTextPrefab;
        [SerializeField] private Transform _parent;

        private List<IconWithText> _elements = new();

        public IconWithText SpawnElement()
        {
            IconWithText iconWithText = Instantiate(_iconWithTextPrefab, _parent);
            _elements.Add(iconWithText);
            return iconWithText;
        }

        public void Remove(IconWithText iconWithText)
        {
            _elements.Remove(iconWithText);
            Destroy(iconWithText.gameObject);
        }
    }
}
