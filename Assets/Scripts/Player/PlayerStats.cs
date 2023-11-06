using System.Collections.Generic;
using UnityEngine;
using TestMulti.Game.Stats;

namespace TestMulti.Game.Player
{
    public class PlayerStats : PlayerBaseComponent
    {
        [SerializeField] private List<ConstStat> constStats = new();
        [SerializeField] private List<DynamicStat> dynamicStats = new();

        private Dictionary<StatType, ConstStat> cahcedConstStats = new();
        private Dictionary<StatType, DynamicStat> cachedDynamicStats = new();

        protected override void OnSpawnOwner()
        {
            cahcedConstStats.Clear();
            cachedDynamicStats.Clear();
        }

        protected override void OnDespawnOwner()
        {
            foreach (ConstStat stat in constStats)
                stat.Reset();

            foreach (DynamicStat stat in dynamicStats)
                stat.Reset();
        }

        public ConstStat GetConstStat(StatType type)
        {
            if (!cahcedConstStats.ContainsKey(type))
                cahcedConstStats.Add(type, constStats.Find(stat => stat.Type == type));

            return cahcedConstStats[type];
        }

        public DynamicStat GetDynamicStat(StatType type)
        {
            if (!cachedDynamicStats.ContainsKey(type))
                cachedDynamicStats.Add(type, dynamicStats.Find(stat => stat.Type == type));

            return cachedDynamicStats[type];
        }
    }
}