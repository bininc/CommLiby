using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommLiby.SocketLib;

namespace CommLiby.Cyhk.Models
{
    public class GSYJ_MSG
    {
        /// <summary>
        /// 消息每包大小
        /// </summary>
        public static int PackSize = 896;
        /// <summary>
        /// 消息类别 S:短消息 P:业务指令 X:系统消息
        /// </summary>
        public char Type { get; set; }

        private static readonly object lockObj = new object();
        private static byte SendNo = 1;
        /// <summary>
        /// 获得发送序号
        /// </summary>
        /// <returns></returns>
        public string GetSendNo()
        {
            lock (lockObj)
            {
                return (SendNo++).ToString("D2");
            }
        }
        /// <summary>
        /// 下发用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 下发消息内容
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 创建消息实例
        /// </summary>
        /// <param name="type">消息类别 S:短消息 P:业务指令 X:系统消息</param>
        /// <param name="msg">下发消息内容</param>
        /// <param name="userName">下发消息内容</param>
        public GSYJ_MSG(char type, string msg, string userName = null)
        {
            this.Type = type;
            this.Msg = msg;
            this.UserName = userName;
        }

        public byte[][] GetSendBytes()
        {
            byte[] bs = NetTools.HostToNetworkOrderToBytes(Msg, -1);
            int len = bs.Length / PackSize + (bs.Length % PackSize > 0 ? 1 : 0);
            if (len > 99)
            {
                throw new Exception("发送内容长度超过最大长度限制");
            }
            byte[][] array = new byte[len][];

            for (int i = 1; i <= len; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Type);
                sb.Append(GetSendNo());
                sb.Append(len.ToString("D2"));
                sb.Append(i.ToString("D2"));
                sb.Append("!");
                if (!string.IsNullOrEmpty(UserName))
                {
                    sb.Append(UserName);
                    sb.Append("&");
                }

                byte[] head_bs = NetTools.HostToNetworkOrderToBytes(sb.ToString(), -1);
                int msg_len = i < len ? PackSize : bs.Length % PackSize;
                byte[] all_bs = new byte[msg_len + head_bs.Length + 3];
                Array.Copy(head_bs, 0, all_bs, 0, head_bs.Length);
                Array.Copy(bs, (i - 1) * PackSize, all_bs, head_bs.Length, msg_len);
                all_bs[all_bs.Length - 1] = all_bs[all_bs.Length - 2] = all_bs[all_bs.Length - 3] = (byte)'#';
                array[i - 1] = all_bs;
            }

            return array;
        }
    }
}
