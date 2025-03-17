using Assets.CourseGame.Develop.CommonServices.ConfigsManagement;
using Assets.CourseGame.Develop.Configs.Gameplay.Loot;
using Assets.CourseGame.Develop.Gameplay.Entities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.CourseGame.Develop.Gameplay.Features.LootFeature
{
    public class DropLootService
    {
        private LootListConfig _lootListConfig;
        private LootFactory _lootFactory;   

        public DropLootService(ConfigsProviderService configsProviderService, LootFactory lootFactory)
        {
            _lootListConfig = configsProviderService.LootListConfig;
            _lootFactory = lootFactory;
        }

        public void DropLootFor(Entity entity)
        {
            Transform entityTransform = entity.GetTransform();

            List<ExperienceLootConfig> expConfigs = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(ExperienceLootConfig))
                .Cast<ExperienceLootConfig>() 
                .ToList();

            if (expConfigs.Count > 0)
                DropExp(entityTransform.position, expConfigs[Random.Range(0, expConfigs.Count)]);

            DropCoins(entityTransform.position);
            DropHealth(entityTransform.position);
        }

        private void DropExp(Vector3 position, ExperienceLootConfig experienceLootConfig)
        {
            int expInOnePotion = 300;

            if(experienceLootConfig.Experience < expInOnePotion)
            {
                _lootFactory.CreateExperienceLoot(experienceLootConfig.Prefab, position, experienceLootConfig.Experience);
            }
            else
            {
                int restOfExp = experienceLootConfig.Experience % expInOnePotion;

                int potionNumbers = (experienceLootConfig.Experience - restOfExp)/ expInOnePotion;

                for (int i = 0; i < potionNumbers; i++)
                {
                    _lootFactory.CreateExperienceLoot(experienceLootConfig.Prefab, position, expInOnePotion);
                }
            }
        }

        public void DropCoins(Vector3 position)
        {
            List<CoinsLootConfig> coinsConfigs = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(CoinsLootConfig))
                .Cast<CoinsLootConfig>()
                .ToList();

            if(coinsConfigs.Count > 0 && Random.Range(0, 100) > 50)
            {
                CoinsLootConfig coinsLootConfig = coinsConfigs[Random.Range(0, coinsConfigs.Count)];

                _lootFactory.CreateCoinsLoot(coinsLootConfig.Prefab, position, coinsLootConfig.Coins);
            }
        }

        public void DropHealth(Vector3 position)
        {
            List<HealthLootConfig>  healthConfigs = _lootListConfig.LootConfigs
                .Where(loot => loot.GetType() == typeof(HealthLootConfig))
                .Cast<HealthLootConfig>()
                .ToList();

            if (healthConfigs.Count > 0 && Random.Range(0, 100) > 50)
            {
                HealthLootConfig healthLootConfig = healthConfigs[Random.Range(0, healthConfigs.Count)];

                _lootFactory.CreateHealthLoot(healthLootConfig.Prefab, position, healthLootConfig.Health);
            }
        }
    }
}
