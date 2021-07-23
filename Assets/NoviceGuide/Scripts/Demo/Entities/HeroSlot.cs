using UnityEngine;

namespace NoviceGuide.Scripts.Demo.Entities
{
    public class HeroSlot : MonoBehaviour
    {
        [Tooltip("此 slot 所擁有的英雄編號")] [SerializeField] private int goodsId;
        public int GoodsId => goodsId;
    }
}