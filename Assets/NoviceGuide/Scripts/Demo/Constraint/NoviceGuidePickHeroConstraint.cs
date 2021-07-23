using NoviceGuide.Scripts.Demo.Entities;
using NoviceGuide.Scripts.System.Constraint;
using UnityEngine;

namespace NoviceGuide.Scripts.Demo.Constraint
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