﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommLiby
{
    public class ClassBase<T> where T : class, new()
    {
        #region 单例模式

        public static T Instance
        {
            get
            {
                return InnerInstance.instance;
            }
        }

        private class InnerInstance
        {
            /// <summary>
            /// 当一个类有静态构造函数时，它的静态成员变量不会被beforefieldinit修饰
            /// 就会确保在被引用的时候才会实例化，而不是程序启动的时候实例化
            /// </summary>
            static InnerInstance() { }
            internal static T instance = new T();
        }

        #endregion
    }
}
