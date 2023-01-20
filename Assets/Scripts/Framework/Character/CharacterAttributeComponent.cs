using System;
using UnityEngine;

namespace Framework.Character
{
    public class CharacterAttributeComponent : MonoBehaviour
    {
        /// <summary>
        /// 基础血量
        /// </summary>
        public int BasicHP { get; set; }
        
        /// <summary>
        /// 基础攻击力
        /// </summary>
        public int BasicATK { get; set; }
        
        /// <summary>
        /// 基础防御力
        /// </summary>
        public int BasicDEF { get; set; }

        void Awake()
        {
            
        }
    }
}