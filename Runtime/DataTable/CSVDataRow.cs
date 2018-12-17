//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//2018年08月30日-11:25
//Icarus.GameFramework

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Didu.GameMain.Scripts.Runtime.Attributes;
using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

namespace Didu.Icarus.GameFramework.DataTable
{
    public abstract class CSVDataRow : IDataRow,IDataRowCreate
    {
        #region Runtime

        /// <summary>
        /// 分隔号,默认为空格
        /// </summary>
        protected virtual Char SeparatedValue { get; } = ' ';

        /// <summary>
        /// 字段数量
        /// </summary>
        protected virtual int ColCount { get; } = 2;

        [FieldComment(nameof(Id),"ID",0)]
        public virtual int Id { get; protected set; }
        
        public virtual bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            string[] text = dataRowSegment.Source.Split(SeparatedValue);

            if(text.Length != ColCount)
            {
                throw new GameFrameworkException($"CSV 字段数量和数据不一致," +
                                                 $"请重写ColCount,现在ColCount为:{ColCount}," +
                                                 $"数据解析数量为:{text.Length}");
            }

            for (var index = 0; index < text.Length; index++)
            {
                var str = text[index];
                SetData(index, str);
            }

            return true;
        }

        public abstract bool ParseDataRow(GameFrameworkSegment<byte[]> dataRowSegment);

        public abstract bool ParseDataRow(GameFrameworkSegment<Stream> dataRowSegment);

        /// <summary>
        /// 由 ParseDataRow 调用
        /// </summary>
        /// <param name="index">列号</param>
        /// <param name="data">数据</param>
        protected abstract void SetData(int index, string data);

        internal sealed class FieldCommentInfo
        {
            public PropertyInfo PropertyInfo;
            public FieldComment FComment;
        }

        #endregion
        
        //todo 主要的,生成块,其他代码不用管
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual string Create()
        {
            StringBuilder sbPropertyName = new StringBuilder("#");
            StringBuilder sbPropertyComment = new StringBuilder("#");
            StringBuilder sbPropertyType = new StringBuilder("#");
            StringBuilder sbPropertyValue = new StringBuilder();

            List<FieldCommentInfo> fieldComments = new List<FieldCommentInfo>();
            
            foreach (var property in GetType().GetProperties())
            {
                var att = property.GetCustomAttribute(typeof(FieldComment), true);
                
                fieldComments.Add(new FieldCommentInfo()
                {
                    PropertyInfo = property,
                    FComment = (FieldComment)  att
                });
            }
                
            foreach (var attribute in fieldComments.OrderBy(x => x.FComment.Priority))
            {
                FieldComment comment = attribute.FComment;
                sbPropertyName.Append($"{comment.Name}{SeparatedValue}");
                sbPropertyComment.Append($"{comment.Comment}{SeparatedValue}");
                
                if (string.IsNullOrEmpty(comment.Type))
                {
                    sbPropertyType.Append($"{attribute.PropertyInfo.PropertyType.Name}{SeparatedValue}");
                }
                else
                {
                    sbPropertyType.Append($"{comment.Type}{SeparatedValue}");
                }

                sbPropertyValue.Append($"{attribute.PropertyInfo.GetValue(this)}{SeparatedValue}");
            }

            sbPropertyName.Append($"分隔符:'{SeparatedValue}'");

            //删除最后一个 SeparatedValue
            sbPropertyValue.Remove(sbPropertyValue.Length - 1, 1);

            return new StringBuilder()
                .AppendLine(sbPropertyName.ToString())
                .AppendLine(sbPropertyComment.ToString())
                .AppendLine(sbPropertyType.ToString())
                .Append(sbPropertyValue)
                .ToString();
        }
    }
}