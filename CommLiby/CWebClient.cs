using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace CommLiby
{
    public class CWebClient
    {
        public event CWebClientCompleted Completed;
        public bool IsBusy { get; private set; }
        private object _userState;

        public void GetStringAsync(Uri url, object userState)
        {
            WebTools.GetString(((status, s) =>
            {
                Completed?.Invoke(this, new CWebClientEventArgs(status, s, _userState));
                IsBusy = false;
            }), url);
            _userState = userState;
            IsBusy = true;
        }
    }

    public delegate void CWebClientCompleted(CWebClient sender, CWebClientEventArgs e);

    public class CWebClientEventArgs : EventArgs
    {
        public WebExceptionStatus Error { get; }
        public object Result { get; }
        public object UserState { get; }

        public CWebClientEventArgs(WebExceptionStatus error, object result, object userState)
        {
            this.Error = error;
            this.Result = result;
            this.UserState = userState;
        }
    }
}
