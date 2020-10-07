using UnityApiWrapper.BusinessLogic.Contracts;

namespace DocumentSender
{
    public class OnBaseCredentials : IOnbaseCredentials
    {
        private readonly string _url;
        private readonly string _odbc;
        private readonly string _username;
        private readonly string _password;

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