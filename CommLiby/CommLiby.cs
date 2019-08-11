using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CommLiby
{
    public static partial class Common_liby
    {
        #region 常用变量


        #endregion

        #region 常用功能

        /// <summary>
        /// 获取一个GUID作为数据库表或者表单的主键
        /// </summary>
        /// <returns></returns>
        public static string GetGuidString()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        }

        /// <summary>
        /// 扩展 获取字段名称
        /// </summary>
        /// <param name="exp"></param>
        /// <returns>return string</returns>
        public static string FieldName<T>(Expression<Func<T, object>> exp)
        {
            try
            {
                MemberExpression me = exp.Body as MemberExpression;
                if (me != null)
                    return me.Member.Name;
                else
                {
                    return ((UnaryExpression)exp.Body).Operand.ToString().Split('.').Last();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// 截取数字制定位数
        /// </summary>
        /// <param name="num">数字</param>
        /// <param name="n">截取位数</param>
        /// <returns></returns>
        public static double TrimNumber(double num, int n = 4)
        {
            int b = num.ToString("0").Length; //整数一共3位
            int c = num.ToString().Length; //a总长度为10位
            int d = 0;
            if (c > b)
            {
                d = c - b - 1; //小数点後面有多少位
            }

            if (n < d) //如果小数点后的位数大於要取的位数，就截取前b+1+n位 得到最後结果
            {
                string Last = num.ToString().Substring(0, b + 1 + n);
                num = double.Parse(Last);
            }

            return num;
        }

        #endregion

        #region 手机号码相关

        /// <summary>
        /// 是否是手机号码
        /// </summary>
        /// <param name="phonestr"></param>
        /// <returns></returns>
        public static bool IsPhone(string phonestr)
        {
            if (string.IsNullOrWhiteSpace(phonestr)) return false;
            string regstr = @"^1[3|4|5|7|8]\d{9}$";
            return Regex.IsMatch(phonestr, regstr);
        }

        /// <summary>
        /// 是否是电话号码
        /// </summary>
        /// <param name="telstr"></param>
        /// <returns></returns>
        public static bool IsTel(string telstr)
        {
            if (string.IsNullOrWhiteSpace(telstr)) return false;
            string regstr = @"^(\(\d{3,4}\)|\d{3,4}-|\s)?\d{5,14}$";
            return Regex.IsMatch(telstr, regstr);
        }

        /// <summary>
        /// 是否是电话号码（手机或者固话）
        /// </summary>
        /// <param name="phonestr"></param>
        /// <returns></returns>
        public static bool IsPhoneOrTel(string phonestr)
        {
            if (string.IsNullOrWhiteSpace(phonestr)) return false;
            if (IsPhone(phonestr)) return true;
            else
            {
                if (IsTel(phonestr)) return true;
            }

            return false;
        }

        #endregion

        #region 验证是否ip地址

        /// <summary>
        /// 验证是否是正确的ip地址
        /// </summary>
        /// <param name="ipv4"></param>
        /// <returns></returns>
        public static bool IsIPV4(string ipv4)
        {
            if (string.IsNullOrWhiteSpace(ipv4)) return false;
            string regstr = @"^(25[0-5]|2[0-4]\d|[0-1]?\d?\d)(\.(25[0-5]|2[0-4]\d|[0-1]?\d?\d)){3}$";
            return Regex.IsMatch(ipv4, regstr);
        }

        #endregion

        #region JSON相关

        /// <summary>
        /// 获得值
        /// </summary>
        /// <returns></returns>
        public static T GetValueFromJson<T>(string jsonValue)
        {
            T value = default(T);
            try
            {
                value = JsonConvert.DeserializeObject<Jsonvalue<T>>(jsonValue).value;
            }
            catch
            {
                // ignored
            }

            return value;
        }

        /// <summary>
        /// 将值转换成JSON串
        /// </summary>
        /// <returns></returns>
        public static string SetValueToJson<T>(T value)
        {
            Jsonvalue<T> jv = new Jsonvalue<T>() { value = value };

            try
            {
                return JsonConvert.SerializeObject(jv);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        /// <summary>
        /// 对象深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(T obj)
        {
            if (obj == null) return obj;

            string jsonT = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(jsonT);
        }

        #endregion

        #region 验证是否为数字格式

        /// <summary>
        /// 验证是否为数字格式（包括浮点型）
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        /// <summary>
        /// 验证s是否为数字格式（只限整型）
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumricForInt(string str)
        {
            if (str == null || str.Length == 0)
            {
                return false;
            }

            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 验证是否为
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsByte(string str)
        {
            if (string.IsNullOrEmpty(str)) return false;
            byte b;
            bool suc = byte.TryParse(str, out b);
            return suc;
        }

        #endregion

        #region 线程相关

        ///// <summary>
        ///// 主线程同步上下文
        ///// </summary>
        public static SynchronizationContext MainContext { get; private set; }

        /// <summary>
        /// 设置主线程
        /// </summary>
        /// <param name="_context"></param>
        public static void SetMainContext(SynchronizationContext _context)
        {
            if (_context != null)
                MainContext = _context;
        }

        /// <summary>
        /// 在主线程上运行方法
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public static bool InvokeMethodOnMainThread(Action func, bool async = true)
        {
            if (func == null) return false;

            if (MainContext == null)
            {
                InvokeAction(func);
                return false;
            }

            if (SynchronizationContext.Current == null || SynchronizationContext.Current != MainContext)
            {
                if (async)
                {
                    MainContext.Post(state => { InvokeAction(func); }, null);
                }
                else
                {
                    MainContext.Send(state => { InvokeAction(func); }, null);
                }
            }
            else
            {
                InvokeAction(func);
            }

            return true;
        }

        private static void InvokeAction(Action func)
        {
            try
            {
                func();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static T InvokeFunc<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region 字节转换相关

        public static void GetBytes(string str, byte[] b, int Start = 0)
        {
            int len = str.Length;
            for (int i = 0; i < len; i++)
            {
                if (str[i] < 128)
                {
                    b[Start + i] = (byte)str[i];
                }
            }
        }

        public static void GetBytes(UInt16 value, byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                b[Start] = (byte)(value >> 8);
                b[Start + 1] = (byte)(value & 0xFF);
            }
            else
            {
                b[Start] = (byte)(value & 0xFF);
                b[Start + 1] = (byte)(value >> 8);
            }
        }

        public static void GetBytes(UInt32 value, byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                //Buffer.BlockCopy(new UInt32[] { value }, 0, b, Start, 4);
                b[Start] = (byte)(value >> 24);
                b[Start + 1] = (byte)((value & 0xFF0000) >> 16);
                b[Start + 2] = (byte)((value & 0xFF00) >> 8);
                b[Start + 3] = (byte)(value & 0xFF);
            }
            else
            {
                b[Start] = (byte)(value & 0xFF);
                b[Start + 1] = (byte)(value & 0xFF00 >> 8);
                b[Start + 2] = (byte)(value & 0xFF0000 >> 16);
                b[Start + 3] = (byte)(value >> 24);
            }
        }

        public static void GetBytes(UInt64 value, byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                //Buffer.BlockCopy(new UInt32[] { value }, 0, b, Start, 4);
                b[Start] = (byte)(value >> 56);
                b[Start + 1] = (byte)(value & 0xFF000000000000L >> 48);
                b[Start + 2] = (byte)(value & 0xFF0000000000L >> 40);
                b[Start + 3] = (byte)(value & 0xFF00000000L >> 32);
                b[Start + 4] = (byte)(value & 0xFF000000L >> 24);
                b[Start + 5] = (byte)(value & 0xFF0000L >> 16);
                b[Start + 6] = (byte)(value & 0xFF00L >> 8);
                b[Start + 7] = (byte)(value & 0xFFL);
            }
            else
            {
                b[Start] = (byte)(value & 0xFFL);
                b[Start + 1] = (byte)(value & 0xFF00L >> 8);
                b[Start + 2] = (byte)(value & 0xFF0000L >> 16);
                b[Start + 3] = (byte)(value & 0xFF000000L >> 24);
                b[Start + 4] = (byte)(value & 0xFF00000000L >> 32);
                b[Start + 5] = (byte)(value & 0xFF0000000000L >> 40);
                b[Start + 6] = (byte)(value & 0xFF000000000000L >> 48);
                b[Start + 7] = (byte)(value >> 56);
            }
        }

        public static void GetBytes(Int16 value, byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                b[Start] = (byte)(value >> 8);
                b[Start + 1] = (byte)(value & 0xFF);
            }
            else
            {
                b[Start] = (byte)(value & 0xFF);
                b[Start + 1] = (byte)(value >> 8);
            }
        }

        public static void GetBytes(Int32 value, byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                //Buffer.BlockCopy(new UInt32[] { value }, 0, b, Start, 4);
                b[Start] = (byte)(value >> 24);
                b[Start + 1] = (byte)(value & 0xFF0000 >> 16);
                b[Start + 2] = (byte)(value & 0xFF00 >> 8);
                b[Start + 3] = (byte)(value & 0xFF);
            }
            else
            {
                b[Start] = (byte)(value & 0xFF);
                b[Start + 1] = (byte)(value & 0xFF00 >> 8);
                b[Start + 2] = (byte)(value & 0xFF0000 >> 16);
                b[Start + 3] = (byte)(value >> 24);
            }
        }

        public static void GetBytes(Int64 value, byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                //Buffer.BlockCopy(new UInt32[] { value }, 0, b, Start, 4);
                b[Start] = (byte)(value >> 56);
                b[Start + 1] = (byte)(value & 0xFF000000000000L >> 48);
                b[Start + 2] = (byte)(value & 0xFF0000000000L >> 40);
                b[Start + 3] = (byte)(value & 0xFF00000000L >> 32);
                b[Start + 4] = (byte)(value & 0xFF000000L >> 24);
                b[Start + 5] = (byte)(value & 0xFF0000L >> 16);
                b[Start + 6] = (byte)(value & 0xFF00L >> 8);
                b[Start + 7] = (byte)(value & 0xFFL);
            }
            else
            {
                b[Start] = (byte)(value & 0xFFL);
                b[Start + 1] = (byte)(value & 0xFF00L >> 8);
                b[Start + 2] = (byte)(value & 0xFF0000L >> 16);
                b[Start + 3] = (byte)(value & 0xFF000000L >> 24);
                b[Start + 4] = (byte)(value & 0xFF00000000L >> 32);
                b[Start + 5] = (byte)(value & 0xFF0000000000L >> 40);
                b[Start + 6] = (byte)(value & 0xFF000000000000L >> 48);
                b[Start + 7] = (byte)(value >> 56);
            }
        }

        public static UInt16 GetUInt16(byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (UInt16)(b[1] | b[0] << 8);
            }
            else
            {
                return (UInt16)(b[0] | b[1] << 8);
            }
        }

        public static UInt32 GetUInt32(byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (UInt32)(b[0] | b[1] << 8 | b[2] << 16 | b[3] << 24);
            }
            else
            {
                return (UInt32)(b[3] | b[2] << 8 | b[1] << 16 | b[0] << 24);
            }
        }

        public static UInt64 GetUInt64(byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (UInt64)((UInt64)b[0] | (UInt64)b[1] << 8 | (UInt64)b[2] << 16 | (UInt64)b[3] << 24 |
                                 (UInt64)b[4] << 32 | (UInt64)b[5] << 40 | (UInt64)b[6] << 48 | (UInt64)b[7] << 56);
            }
            else
            {
                return (UInt64)((UInt64)b[7] | (UInt64)b[6] << 8 | (UInt64)b[5] << 16 | (UInt64)b[4] << 24 |
                                 (UInt64)b[3] << 32 | (UInt64)b[2] << 40 | (UInt64)b[1] << 48 | (UInt64)b[0] << 56);
                ;
            }
        }

        public static Int16 GetInt16(byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (Int16)(b[1] | b[0] << 8);
            }
            else
            {
                return (Int16)(b[0] | b[1] << 8);
            }
        }

        public static Int32 GetInt32(byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (Int32)(b[0] | b[1] << 8 | b[2] << 16 | b[3] << 24);
            }
            else
            {
                return (Int32)(b[3] | b[2] << 8 | b[1] << 16 | b[0] << 24);
            }
        }

        public static Int64 GetInt64(byte[] b, int Start = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                return (Int64)((Int64)b[0] | (Int64)b[1] << 8 | (Int64)b[2] << 16 | (Int64)b[3] << 24 |
                                (Int64)b[4] << 32 | (Int64)b[5] << 40 | (Int64)b[6] << 48 | (Int64)b[7] << 56);
            }
            else
            {
                return (Int64)((Int64)b[7] | (Int64)b[6] << 8 | (Int64)b[5] << 16 | (Int64)b[4] << 24 |
                                (Int64)b[3] << 32 | (Int64)b[2] << 40 | (Int64)b[1] << 48 | (Int64)b[0] << 56);
                ;
            }
        }

        #endregion

        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="val">数据值</param>
        /// <returns></returns>
        public static object Convert2Type(Type type, object val)
        {
            try
            {
                if (type == typeof(string))
                {
                    return val?.ToString();
                }
                else if (type == typeof(byte))
                {
                    return Convert.ToByte(val);
                }
                else if (type == typeof(byte?))
                {
                    if (val == null) return null;
                    return Convert.ToByte(val);
                }
                else if (type == typeof(byte[]))
                {
                    return val as byte[];
                }
                else if (type == typeof(short))
                {
                    return Convert.ToInt16(val);
                }
                else if (type == typeof(short?))
                {
                    if (val == null) return null;
                    return Convert.ToInt16(val);
                }
                else if (type == typeof(ushort))
                {
                    return Convert.ToUInt16(val);
                }
                else if (type == typeof(ushort?))
                {
                    if (val == null) return null;
                    return Convert.ToUInt16(val);
                }
                else if (type == typeof(int)) //int类型
                {
                    return Convert.ToInt32(val);
                }
                else if (type == typeof(int?)) //可空int类型
                {
                    if (val == null) return null;
                    return Convert.ToInt32(val);
                }
                else if (type == typeof(uint))
                {
                    return Convert.ToUInt32(val);
                }
                else if (type == typeof(uint?))
                {
                    if (val == null) return null;
                    return Convert.ToUInt32(val);
                }
                else if (type == typeof(long))
                {
                    return Convert.ToInt64(val);
                }
                else if (type == typeof(long?))
                {
                    if (val == null) return null;
                    return Convert.ToInt64(val);
                }
                else if (type == typeof(ulong))
                {
                    return Convert.ToUInt64(val);
                }
                else if (type == typeof(ulong?))
                {
                    if (val == null) return null;
                    return Convert.ToUInt64(val);
                }
                else if (type == typeof(float))
                {
                    return Convert.ToSingle(val);
                }
                else if (type == typeof(float?))
                {
                    if (val == null) return null;
                    return Convert.ToSingle(val);
                }
                else if (type == typeof(double))
                {
                    return Convert.ToDouble(val);
                }
                else if (type == typeof(double?))
                {
                    if (val == null) return null;
                    return Convert.ToDouble(val);
                }
                else if (type == typeof(decimal))
                {
                    return Convert.ToDecimal(val);
                }
                else if (type == typeof(decimal?))
                {
                    if (val == null) return null;
                    return Convert.ToDecimal(val);
                }
                else if (type == typeof(bool))
                {
                    if (val is string)
                    {
                        if (val.ToString() == "0")
                        {
                            return false;
                        }
                        else if (val.ToString() == "1")
                        {
                            return true;
                        }
                    }
                    return Convert.ToBoolean(val);
                }
                else if (type == typeof(bool?))
                {
                    if (val == null) return null;
                    if (val is string)
                    {
                        if (val.ToString() == "0")
                        {
                            return false;
                        }
                        else if (val.ToString() == "1")
                        {
                            return true;
                        }
                    }
                    return Convert.ToBoolean(val);
                }
                else if (type == typeof(DateTime))
                {
                    return Convert.ToDateTime(val);
                }
                else if (type == typeof(DateTime?))
                {
                    if (val == null) return null;
                    return Convert.ToDateTime(val);
                }
                else if (type == typeof(BinDateTime))
                {
                    if (val == null) return null;
                    return BinDateTime.NewWithDateTime(Convert.ToDateTime(val));
                }
                else
                    return val;
            }
            catch (Exception ex)
            {
                return "err_" + ex.Message;
            }
        }

        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="val">数据值</param>
        /// <returns></returns>
        public static T Convert2Type<T>(object val)
        {
            return (T)Convert2Type(typeof(T), val);
        }

        /// <summary>
        /// 计算两个点之间的距离
        /// </summary>
        /// <returns></returns>
        public static double CalTowPointLength(Point p1, Point p2)
        {
            double a = Math.Abs(p1.X - p2.X);
            double b = Math.Abs(p1.Y - p2.Y);
            double c = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            return c;
        }
    }

    #region JSON相关
    public class Jsonvalue<T>
    {
        public T value { get; set; }
    }
    #endregion
}
