namespace Assets.CourseGame.Develop.MainMenu.LevelsMenuFeature.LevelsMenuPopup
{
    public class LevelsMenuPopupPresenter
    {
        private const string TitleName = "Levels";

        private readonly LevelsMenuPopupFactory _levelsMenuPopupFactory;

        private LevelTileListPresenter _levelsTileListPresenter;

        private readonly LevelsMenuPopupView _view;

        public LevelsMenuPopupPresenter(
            LevelsMenuPopupFactory levelsMenuPopupFactory, 
            LevelsMenuPopupView view)
        {
            _levelsMenuPopupFactory = levelsMenuPopupFactory;
            _view = view;
        }

        public void Enable()
        {
            _view.SetTitle(TitleName);

            _levelsTileListPresenter = _levelsMenuPopupFactory.CreateLevelTilesListPresenter(_view.LevelTileListView);
            _levelsTileListPresenter.Enable();

            _view.CloseRequest += OnCloseRequest;
        }

        public void Disable()
        {
            _levelsTileListPresenter.Disable();

            _view.CloseRequest -= OnCloseRequest;

            UnityEngine.Object.Destroy(_view.gameObject);
        }


        private void OnCloseRequest()
        {
            Disable();
        }
    }
}
