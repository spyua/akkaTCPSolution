using System.Configuration;

namespace TcpWindowsForm.Config
{
    public class AppSetting
    {
        public static class SingletonHolder
        {
            static SingletonHolder()
            {

            }
            internal static readonly AppSetting INSTANCE = new AppSetting();
        }

        public static AppSetting Instance { get { return SingletonHolder.INSTANCE; } }

        public string AkkaSysName { get; private set; }
        public string LocalIp { get; private set; }
        public int LocalPort { get; private set; }
        public string RemoteIp { get; private set; }
        public int RemotePort { get; private set; }
        public string ClientLog { get; private set; }
        public string ServerLog { get; private set; }

        public AppSetting()
        {
            AkkaSysName = GetAppConfigValue("ActorSystemName");
            LocalIp = GetAppConfigValue("LocalIp");
            LocalPort = GetAppConfigIntVaule("LocalPort");
            RemoteIp = GetAppConfigValue("RemoteIP");
            RemotePort = GetAppConfigIntVaule("RemotePort");

            ClientLog = "ClientLog";
            ServerLog = "ServerLog";
        }

        private string GetAppConfigValue(string value)
        {
            return ConfigurationManager.AppSettings[value];
        }
        private int GetAppConfigIntVaule(string value)
        {
            return int.Parse(ConfigurationManager.AppSettings[value]);
        }
        private bool GetAppConfigBoolVaule(string value)
        {
            return bool.Parse(ConfigurationManager.AppSettings[value]);
        }
    }

}
