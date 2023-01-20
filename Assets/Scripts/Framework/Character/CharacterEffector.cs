using UnityEngine;

namespace Framework.Character
{
    public abstract class CharacterEffector
    {
        /// <summary>
        /// 效果器ID
        /// </summary>
        public int EffectorID { get; set; }
        
        /// <summary>
        /// 拥有者
        /// </summary>
        public GameObject Owner { get; set; }
        
        /// <summary>
        /// 持续时间。持续时间若为0，则是立即生效的效果；若小于0，则持续时间无限；若大于0，则持续时间有限。立即生效的效果将不会被显示为buff。
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// 最大叠加层数
        /// </summary>
        public int MaxStack { get; set; }

        /// <summary>
        /// 每帧调用，用于检查并释放效果器的效果
        /// </summary>
        public abstract void OnTick();
    }
}