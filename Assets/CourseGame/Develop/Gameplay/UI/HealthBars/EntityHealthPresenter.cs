using Assets.CourseGame.Develop.CommonUI;
using Assets.CourseGame.Develop.Gameplay.Features.TeamFeature;
using Assets.CourseGame.Develop.Utils.Reactive;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.UI.HealthBars
{
    public class EntityHealthPresenter
    {
        private BarWithText _bar;
        private ReactiveVariable<int> _team;
        private ReactiveVariable<float> _health;
        private ReactiveVariable<float> _maxHealth;

        public EntityHealthPresenter(
            ReactiveVariable<float> health,
            ReactiveVariable<float> maxHealth,
            ReactiveVariable<int> team,
            BarWithText bar)
        {
            _health = health;
            _maxHealth = maxHealth;
            _team = team;
            _bar = bar;
        }

        public BarWithText Bar => _bar;

        public void Enable()
        {
            _health.Changed += OnHealthChanged;
            _maxHealth.Changed += OnMaxHealthChanged;
            _team.Changed += OnTeamChanged;

            UpdateHealth();
            UpdateFillerColorBy(_team.Value);
        }

        public void Disable()
        {
            _health.Changed -= OnHealthChanged;
            _maxHealth.Changed -= OnMaxHealthChanged;
            _team.Changed -= OnTeamChanged;
        }

        private void OnTeamChanged(int oldValue, int newTeam) => UpdateFillerColorBy(newTeam);

        private void OnMaxHealthChanged(float oldValue, float newValue) => UpdateHealth();

        private void OnHealthChanged(float oldValue, float newValue) => UpdateHealth();

        private void UpdateHealth()
        {
            _bar.UpdateText(_health.Value.ToString("0"));
            _bar.UpdateSlider(_health.Value / _maxHealth.Value);
        }

        private void UpdateFillerColorBy(int team)
        {
            if (team == TeamTypes.MainHero)
                _bar.SetFillerColor(Color.green);
            else if (team == TeamTypes.Enemies)
                _bar.SetFillerColor(Color.red);
        }
    }
}
