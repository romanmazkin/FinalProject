using Assets.CourseGame.Develop.Configs.Gameplay;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup
{
    public class LevelTileListPresenter
    {
        private readonly LevelListConfig _levelListConfig;
        private readonly LevelsMenuPopupFactory _levelsMenuPopupFactory;

        private readonly LevelTileListView _view;

        private List<LevelTilePresenter> _levelTilesPresenters = new();

        public LevelTileListPresenter(LevelListConfig levelListConfig, LevelsMenuPopupFactory levelsMenuPopupFactory, LevelTileListView view)
        {
            _levelListConfig = levelListConfig;
            _levelsMenuPopupFactory = levelsMenuPopupFactory;
            _view = view;
        }

        public void Enable()
        {
            for (int i = 0; i < _levelListConfig.Levels.Count; i++)
            {
                LevelTileView levelTileView = _view.SpawnElement();

                LevelTilePresenter levelTilePresenter = _levelsMenuPopupFactory.CreateLevelTilePresenter(levelTileView, i + 1);
                levelTilePresenter.Enable();

                _levelTilesPresenters.Add(levelTilePresenter);
            }
        }

        public void Disable()
        {
            foreach (LevelTilePresenter levelTilePresenter in _levelTilesPresenters)
            {
                levelTilePresenter.Disable();
                _view.Remove(levelTilePresenter.View);
            }

            _levelTilesPresenters.Clear();
        }
    }
}
