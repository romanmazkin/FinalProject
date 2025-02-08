using Assets.CourseGame.Develop.Gameplay.Entities;
using Assets.CourseGame.Develop.Gameplay.Entities.Behaviours;
using System.Collections.Generic;

namespace Assets.CourseGame.Develop.Gameplay.Features.StatsFeature
{
    public class StatEffectsApplierBehaviour : IEntityInitialize, IEntityDispose
    {
        private StatsEffectsList _statsEffectsList;
        private Dictionary<StatTypes, float> _baseStats;
        private Dictionary<StatTypes, float> _modifiedStats;

        public void OnInit(Entity entity)
        {
            _statsEffectsList = entity.GetStatsEffectsList();
            _baseStats = entity.GetBaseStats();
            _modifiedStats = entity.GetModifiedStats();

            _statsEffectsList.Added += OnStatEffectAdded;
            _statsEffectsList.Removed += OnStatEffectRemoved;

            RecalculateStats();
        }

        public void OnDispose()
        {
            _statsEffectsList.Added -= OnStatEffectAdded;
        }

        private void OnStatEffectRemoved(IStatsEffect statEffect)
            => RecalculateStats();

        private void OnStatEffectAdded(IStatsEffect statEffect)
            => RecalculateStats();

        private void RecalculateStats()
        {
            foreach (StatTypes stat in _baseStats.Keys)
                _modifiedStats[stat] = _baseStats[stat];

            foreach (IStatsEffect statsEffect in _statsEffectsList.Elements)
                statsEffect.ApplyTo(_modifiedStats);
        }
    }
}

