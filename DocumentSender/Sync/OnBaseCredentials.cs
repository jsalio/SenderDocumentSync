using UnityApiWrapper.BusinessLogic.Contracts;

namespace DocumentSender
{
    /// <summary>
    /// Represents the <see cref="IOnbaseCredentials"/>
    /// </summary>
    public class OnBaseCredentials : IOnbaseCredentials
    {
        private readonly string _url;
        private readonly string _odbc;
        private readonly string _username;
        private readonly string _password;

        /// <summary>
        /// Creates a new instance of <see cref="OnBaseCredentials"/>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="odbc"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public OnBaseCredentials(string url, string odbc, string username, string password)
        {
            _url = url;
            _odbc = odbc;
            _username = username;
            _password = password;
        }


        string IOnbaseCredentials.GetAppServerUrl()
            => _url;

        string IOnbaseCredentials.GetOnbaseDatasource()
            => _odbc;

        string IOnbaseCredentials.GetOnbaseUserName()
            => _username;

        string IOnbaseCredentials.GetOnbaseUserPassword()
            => _password;
    }
}