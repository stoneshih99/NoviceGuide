using NoviceGuide.Runtime.Example.Scripts.Entities;
using NoviceGuide.Runtime.Scripts.System.Constraint;
using UnityEngine;

namespace NoviceGuide.Runtime.Example.Scripts.Constraint
{
    /// <summary>
    /// 限制英雄的約束
    /// </summary>
    public class NoviceGuidePickHeroConstraint : NoviceGuideConstraintBase
    {
        [SerializeField] private int heroId;
        [SerializeField] private HeroSlot heroSlot;
        
        protected override bool IsRunning()
        {
            if (heroSlot.GoodsId == heroId) return true;
            return false;
        }
    }
}