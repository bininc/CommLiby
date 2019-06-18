using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CommLiby.Database.EF
{
    public class ModelBase<Tmodel> : ClassBase_Notify<Tmodel> where Tmodel : ModelBase<Tmodel>
    {
        /// <summary>
        /// 默认数据JSON格式
        /// </summary>
        public string DefaultJsonData;
        /// <summary>
        /// 是否清空表数据
        /// </summary>
        public bool ClearData;
        /// <summary>
        /// 是否强制更新表结构
        /// </summary>
        public bool ForceUpdateTable;
    }
}
