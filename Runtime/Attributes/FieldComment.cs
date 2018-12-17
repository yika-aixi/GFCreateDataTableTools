//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2018年11月07日-10:25
//Didu.Jxsy.GameMain

using System;

namespace Didu.GameMain.Scripts.Runtime.Attributes
{
    [System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property,Inherited = true,AllowMultiple = false)]
    public class FieldComment:Attribute
    {
        /// <summary>
        /// 注释名
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; }
        
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; }

        public FieldComment(string name, string comment, string type, int priority)
        {
            Name = name;
            Comment = comment;
            Type = type;
            Priority = priority;
        }
        
        public FieldComment(string name, string comment, int priority):this(name,comment,string.Empty, priority)
        {
        }
    }
}