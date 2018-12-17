//创建者:Icarus
//手动滑稽,滑稽脸
//ヾ(•ω•`)o
//https://www.ykls.app
//2018年09月25日-06:05
//Didu.Jxsy.GameMain

using System.IO;
using Didu.GameMain.Scripts.Runtime.Attributes;
using Didu.Icarus.GameFramework.DataTable;
using GameFramework;

namespace GFCreateDataTableTools.Test
{
    public class DRComment:CSVDataRow
    {
        /// <summary>
        /// 资源名称。
        /// </summary>
        [FieldComment("AssetName","资源名称",10)]
        public string AssetName
        {
            get;
            private set;
        }

        public override bool ParseDataRow(GameFrameworkSegment<Stream> dataRowSegment)
        {
            throw new System.NotImplementedException();
        }

        [FieldComment("ID","命令编号",0)]
        public override int Id { get; protected set; }
        
        protected override int ColCount { get; } = 3;
        public override bool ParseDataRow(GameFrameworkSegment<string> dataRowSegment)
        {
            throw new System.NotImplementedException();
        }

        public override bool ParseDataRow(GameFrameworkSegment<byte[]> dataRowSegment)
        {
            throw new System.NotImplementedException();
        }

        protected override void SetData(int index, string data)
        {
            switch (index)
            {
                case 0:
                    Id = int.Parse(data);
                    return;
                case 1:
                    AssetName = data;
                    return;
            }
        }
    }
}