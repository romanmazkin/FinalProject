namespace Assets.CourseGame.Develop.Gameplay.Entities
{
    public enum EntityValues
    {
        MoveDirection,
        MoveSpeed,
        MoveCondition,
        IsMoving,

        RotationDirection,
        RotationSpeed,
        RotationCondition,

        SelfTriggerReciever,
        SelfTriggerDamage,

        CharacterController,
        Transform,
        Rigidbody,
        ShootPoint,

        AttackTrigger,
        AttackCondition,
        IsAttackProcess,
        AttackCanceledCondition,

        InstantAttackEvent,

        IntervalBetweenAttacks,
        AttackCooldown,

        Damage,

        DetectedEntitiesBuffer,

        Health,
        MaxHealth,

        TakeDamageRequest,
        TakeDamageEvent,
        TakeDamageCondition,

        IsDead,
        IsDeathProcess,
        DeathCondition,
        SelfDestroyCondition,

        Team,

        IsMainHero,

        AbilityList,

        BaseStats,
        ModifiedStats,
        StatsEffectsList,
    }
}

