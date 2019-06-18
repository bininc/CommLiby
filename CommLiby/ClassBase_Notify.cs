using System;
using System.Net;
using System.ComponentModel;

namespace CommLiby
{
    public class ClassBase_Notify<T> : INotifyPropertyChanged where T : ClassBase_Notify<T>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propName, bool postMainThread = false)
        {
            //try
            //{
                if (postMainThread)
                {
                    Common_liby.InvokeMethodOnMainThread(() =>
                    {
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
                        StaticPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
                    });
                }
                else
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
                    StaticPropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
                }
            //}
            //catch (Exception ex)
            //{

            //}
        }

        public static event PropertyChangedEventHandler StaticPropertyChanged;

        /// <summary>
        /// 复制一个对象
        /// </summary>
        /// <returns></returns>
        public T Copy()
        {
            T copyT = Common_liby.DeepCopy<T>((T)this);
            return copyT;
        }
    }
}
