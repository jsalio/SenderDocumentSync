﻿using System;
using System.Configuration;
using UnityApiWrapper.BusinessLogic;
using UnityApiWrapper.BusinessLogic.Contracts;
using UnityApiWrapper.Models.Enums;

namespace DocumentSender
{
    public class OnBaseReleaser
    {
        private IntegrationManager manager;
        private IOnbaseCredentials credentials;
        private string _url;
        private string _username;
        private string _password;
        private string _odbc;

        public OnBaseReleaser()
        {
            ValidateSettings();
            ////credentials = new OnBaseCredentials("http://192.168.43.139/appserver/service.asmx", "observer", "manager", "password");
            credentials = new OnBaseCredentials(_url, _odbc, _username, _password);
            manager = new IntegrationManager(credentials, LicenseConnectionType.NamedLicense, true);
        }

        private void ValidateSettings()
        {
            try
            {
                _url = ConfigurationManager.AppSettings["OnBaseUrl"];
                _username = ConfigurationManager.AppSettings["ObUsername"];
                _password = ConfigurationManager.AppSettings["Password"];
                _odbc = ConfigurationManager.AppSettings["Odbc"];

                if (string.IsNullOrWhiteSpace(_url) || string.IsNullOrWhiteSpace(_username) ||
                    string.IsNullOrWhiteSpace(_password) || string.IsNullOrWhiteSpace(_odbc))
                {
                    throw new ArgumentException("Configuration not set");
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Configuration not set");
            }
        }

        public IntegrationManager GetInstance()
        {
            return manager;
        }
    }
}