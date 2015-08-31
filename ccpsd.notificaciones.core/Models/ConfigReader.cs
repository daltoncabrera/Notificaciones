using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ccpsd.notificaciones.core
{
    public static class ConfigReader
    {

        static AppSettingsReader reader = new AppSettingsReader();
        /// <summary>
        /// Método interno. Lee una variable tipo String
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAppSettingAsString(string key)
        {

            return ConfigurationManager.AppSettings.Get(key);
        }

        /// <summary>
        /// Método interno. Lee una variable tipo Int
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetAppSettingAsInt(string key)
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings.Get(key));

        }



        public static string GetServerUrl()
        {
            var server = GetAppSettingAsString(Constantes.TAG_SERVER).Trim();
            
            if (!server.EndsWith("/"))
                server = server + "/";

            var serverSignalR = string.Format("{0}Signalr", server);
            return  serverSignalR;
        }

        public static void Modify(string key, string value)
        {
            string appSettingsTag = "appSettings";
            //ar appSettings = (AppSettingsSection)ConfigurationManager.GetSection("appSettings");
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static void UpdateSetting(string key, string value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            config.AppSettings.Settings.Remove(key);
            config.AppSettings.Settings.Add(key, value);    

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
        }

        public static void SaveServerUrl(string value)
        {
            UpdateSetting(Constantes.TAG_SERVER, value);
        }

        public static string GetLdapServer()
        {
            return GetAppSettingAsString("ldap");
        }
    }
}
