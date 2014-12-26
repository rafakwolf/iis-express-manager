using System;
using System.IO;

namespace IISExpressManager
{
    public class IISExpressConfiguration
    {

        /*
            Written by lordamit
         *  lordamit {at] gmail [dot} com
        */

        private string _iisExpressAddress;
        private string _iisExpressConfigAddress;

        public IISExpressConfiguration()
        {
            SetIISExpressConfigAddress();
            SetIISExpressAddress();
        }

        #region IIS Express Address

        private void SetIISExpressAddress()
        {
            if (IntPtr.Size == 8)
            {
                _iisExpressAddress = @"C:\Program Files (x86)\IIS Express\iisexpress.exe";
            }
            else if (IntPtr.Size == 4)
            {
                _iisExpressAddress = @"C:\Program Files\IIS Express\iisexpress.exe";
            }
        }

        public bool CheckIISExpressExistence()
        {
            if (_iisExpressAddress != null)
                return File.Exists(_iisExpressAddress);
            return false;
        }

        #endregion

        #region IIS Express Configuration File

        internal void SetIISExpressConfigAddress()
        {
            _iisExpressConfigAddress = Environment.GetFolderPath(
                Environment.SpecialFolder.MyDocuments) +
                                       "\\IISExpress\\config\\applicationhost.config";
        }


        public bool CheckIISExpressConfigExistence()
        {
            if (_iisExpressConfigAddress != null)

                return File.Exists(_iisExpressConfigAddress);

            return false;
        }

        #endregion
     
        internal string IISExpressAddress
        {
            get { return _iisExpressAddress; }
        }

        internal string IISExpressConfigAddress
        {
            get { return _iisExpressConfigAddress; }
        }
    }
}