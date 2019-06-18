using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommLiby.Cyhk.Models
{
    public abstract class ICar : ClassBase_Notify<ICar>
    {
        /// <summary>
        /// 车台通讯码      手机号
        /// </summary>		
        public string MAC { get; set; }
        /// <summary>
        /// 车牌号
        /// </summary>		
        public string LICENSE { get; set; }
        /// <summary>
        /// 车台类型  mobil_type
        /// </summary>		
        public ushort MTYPE { get; set; }
        /// <summary>
        /// 服务器IP
        /// </summary>		
        public ushort CTYPE { get; set; }
    }
}
