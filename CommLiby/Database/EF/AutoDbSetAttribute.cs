using System;

namespace CommLiby.Database.EF
{
    /// <summary>
    /// 该属性标记的类会自动注入到EF中
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AutoDbSetAttribute : Attribute
    {

    }
}
