using Assets.CourseGame.Develop.CommonServices.LevelsManagement;
using Assets.CourseGame.Develop.CommonServices.SceneManagement;
using UnityEngine;

namespace Assets.CourseGame.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup
{
    public class LevelTilePresenter
    {
        private const int FirstLevel = 1;

        private readonly CompleteLevelsService _levelService;
        private readonly SceneSwitcher _sceneSwitcher;
        private readonly int _levelNumber;

        private LevelTileView _view;

        private bool _isBlocked;

        public LevelTilePresenter(
            CompleteLevelsService levelService,
            SceneSwitcher sceneSwitcher,
            int levelNumber,
            LevelTileView view)
        {
            _levelService = levelService;
            _sceneSwitcher = sceneSwitcher;
            _levelNumber = levelNumber;
            _view = view;
        }

        public LevelTileView View => _view;

        public void Enable()
        {
            _isBlocked = _levelNumber != FirstLevel && PreviousLevelCompleted() == false;

            _view.SetLevel(_levelNumber.ToString());

            if (_isBlocked)
            {
                _view.SetBlock();
            }
            else
            {
                if (_levelService.IsLevelCompleted(_levelNumber))
                    _view.SetComplete();
                else
                    _view.SetActive();
            }

            _view.Clicked += OnViewClicked;
        }

        public void Disable()
        {
            _view.Clicked -= OnViewClicked;
        }

        private void OnViewClicked()
        {
            if (_isBlocked)
            {
                Debug.Log("Level is blocked");
                return;
            }
            
            if(_levelService.IsLevelCompleted(_levelNumber))
            {
                Debug.Log("Level already completed");
                return;
            }

            _sceneSwitcher.ProcessSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(_levelNumber)));
        }

        private bool PreviousLevelCompleted() => _levelService.IsLevelCompleted(_levelNumber - 1);
    }
}
