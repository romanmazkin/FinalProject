using Assets.CourseGame.Develop.Gameplay.AI.Sensors;
using Assets.CourseGame.Develop.Gameplay.Features.AbilitiesFeature;
using Assets.CourseGame.Develop.Gameplay.Features.AttackFeature;
using Assets.CourseGame.Develop.Gameplay.Features.StatsFeature;
using Assets.CourseGame.Develop.Utils.Conditions;
using Assets.CourseGame.Develop.Utils.Reactive;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Assets.CourseGame.Develop.Gameplay.Entities.CodeGeneration
{
    public static class EntityExtensionsGenerator
    {
        private static Dictionary<EntityValues, Type> _entityValuesToType = new Dictionary<EntityValues, Type>()
        {
            {EntityValues.MoveDirection, typeof(ReactiveVariable<Vector3>) },
            {EntityValues.MoveSpeed, typeof(ReactiveVariable<float>) },
            {EntityValues.MoveCondition, typeof(ICompositeCondition) },
            {EntityValues.IsMoving, typeof(ReactiveVariable<bool>) },

            {EntityValues.RotationDirection, typeof(ReactiveVariable<Vector3>) },
            {EntityValues.RotationSpeed, typeof(ReactiveVariable<float>) },
            {EntityValues.RotationCondition, typeof(ICompositeCondition) },

            {EntityValues.SelfTriggerDamage, typeof(ReactiveVariable<float>) },
            {EntityValues.SelfTriggerReciever, typeof(TriggerReciever) },

            {EntityValues.CharacterController, typeof(CharacterController)},
            {EntityValues.Transform, typeof(Transform)},
            {EntityValues.Rigidbody, typeof(Rigidbody)},
            {EntityValues.ShootPoint, typeof(Transform)},

            {EntityValues.AttackTrigger, typeof(ReactiveEvent)},
            {EntityValues.AttackCondition, typeof(ICompositeCondition)},
            {EntityValues.IsAttackProcess, typeof(ReactiveVariable<bool>)},
            {EntityValues.AttackCanceledCondition, typeof(ICompositeCondition)},

            {EntityValues.InstantAttackEvent, typeof(ReactiveEvent)},
            {EntityValues.InstanShootingDirections, typeof(InstantShootingDirectionArgs)},

            {EntityValues.IntervalBetweenAttacks, typeof(ReactiveVariable<float>)},
            {EntityValues.AttackCooldown, typeof(ReactiveVariable<float>)},

            {EntityValues.Damage, typeof(ReactiveVariable<float>)},

            {EntityValues.DetectedEntitiesBuffer, typeof(List<Entity>)},

            {EntityValues.Health, typeof(ReactiveVariable<float>) },
            {EntityValues.MaxHealth, typeof(ReactiveVariable<float>) },

            {EntityValues.TakeDamageRequest, typeof(ReactiveEvent<float>) },
            {EntityValues.TakeDamageEvent, typeof(ReactiveEvent<float>) },
            {EntityValues.TakeDamageCondition, typeof(ICompositeCondition) },

            {EntityValues.IsDead, typeof(ReactiveVariable<bool>) },
            {EntityValues.IsDeathProcess, typeof(ReactiveVariable<bool>) },
            {EntityValues.DeathCondition, typeof(ICompositeCondition) },
            {EntityValues.SelfDestroyCondition, typeof(ICompositeCondition) },

            {EntityValues.Team, typeof(ReactiveVariable<int>) },

            {EntityValues.IsMainHero, typeof(ReactiveVariable<bool>) },

            {EntityValues.AbilityList, typeof(AbilityList) },

            {EntityValues.BaseStats, typeof(Dictionary<StatTypes, float>) },
            {EntityValues.ModifiedStats, typeof(Dictionary<StatTypes, float>) },
            {EntityValues.StatsEffectsList, typeof(StatsEffectsList) },

            {EntityValues.Level, typeof(ReactiveVariable<int>) },
            {EntityValues.Experience, typeof(ReactiveVariable<float>) },

            {EntityValues.Owner, typeof(Entity) },
            {EntityValues.IsProjectile, typeof(bool) },

            {EntityValues.DeathLayer, typeof(LayerMask) },
            {EntityValues.IsTouchDeathLayer, typeof(ReactiveVariable<bool>) },
            {EntityValues.IsTouchAnotherTeam, typeof(ReactiveVariable<bool>) },

            {EntityValues.BounceCount, typeof(ReactiveVariable<int>) },
            {EntityValues.BounceEvent, typeof(ReactiveEvent<RaycastHit>) },
            {EntityValues.LayerToBounceReaction, typeof(LayerMask) },

            {EntityValues.IsSpawningProcess, typeof(ReactiveVariable<bool>) },
            {EntityValues.Target, typeof(ReactiveVariable<Entity>) },

            {EntityValues.IsPullable, typeof(ReactiveVariable<bool>) },
            {EntityValues.IsPullingProcess, typeof(ReactiveVariable<bool>) },
            {EntityValues.IsCollected, typeof(ReactiveVariable<bool>) },

            {EntityValues.Coins, typeof(ReactiveVariable<int>) },

            {EntityValues.DropLootCondition, typeof(ICompositeCondition) },
            {EntityValues.LootIsDropped, typeof(ReactiveVariable<bool>) },

            {EntityValues.HealthBarPoint, typeof(Transform) },

            {EntityValues.CollidersDisabledOnDeath, typeof(IEnumerable<Collider>) },
        };

        [InitializeOnLoadMethod]
        [MenuItem("Tools/GenerateEntityExtensions")]
        private static void Generate()
        {
            string path = GetPathToExtensionsFile();

            StreamWriter writer = new StreamWriter(path);

            writer.WriteLine(GetClassHeader());
            writer.WriteLine("{");

            foreach (KeyValuePair<EntityValues, Type> entityValueToTypePair in _entityValuesToType)
            {
                string type = entityValueToTypePair.Value.FullName;

                if (entityValueToTypePair.Value.IsGenericType)
                {
                    type = type.Substring(0, type.IndexOf('`'));

                    type += "<";

                    for (int i = 0; i < entityValueToTypePair.Value.GenericTypeArguments.Length; i++)
                    {
                        type += entityValueToTypePair.Value.GenericTypeArguments[i].FullName;

                        if (i != entityValueToTypePair.Value.GenericTypeArguments.Length - 1)
                            type += ",";
                    }

                    type += ">";
                }

                if (HasEmptyConstructor(entityValueToTypePair.Value))
                    writer.WriteLine($"public static {typeof(Entity)} Add{entityValueToTypePair.Key}(this {typeof(Entity)} entity) => entity.AddValue({typeof(EntityValues)}.{entityValueToTypePair.Key}, new {type}());");

                writer.WriteLine($"public static {typeof(Entity)} Add{entityValueToTypePair.Key}(this {typeof(Entity)} entity, {type} value) => entity.AddValue({typeof(EntityValues)}.{entityValueToTypePair.Key}, value);");
                writer.WriteLine($"public static {type} Get{entityValueToTypePair.Key}(this {typeof(Entity)} entity) => entity.GetValue<{type}>({typeof(EntityValues)}.{entityValueToTypePair.Key});");
                writer.WriteLine($"public static {typeof(bool)} TryGet{entityValueToTypePair.Key}(this {typeof(Entity)} entity, out {type} value) => entity.TryGetValue<{type}>({typeof(EntityValues)}.{entityValueToTypePair.Key}, out value);");
            }

            writer.WriteLine("}");

            writer.Close();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static string GetClassHeader() => "public static class EntityExtensionGenerated";

        private static string GetPathToExtensionsFile() => $"{Application.dataPath}/CourseGame/Develop/Gameplay/Entities/CodeGeneration/EntityExtensionGenerated.cs";

        private static bool HasEmptyConstructor(Type type) =>
            type.IsAbstract == false
            && type.IsSubclassOf(typeof(UnityEngine.Object)) == false
            && type.IsInterface == false
            && type.GetConstructors().Any(constructor => constructor.GetParameters().Count() == 0);
    }
}
