	//WebServiceStudio Application to test WCF web services.

	//Copyright (C) 2012  Irdeto Customer Care And Billing

	//This program is free software: you can redistribute it and/or modify
	//it under the terms of the GNU General Public License as published by
	//the Free Software Foundation, either version 3 of the License, or
	//(at your option) any later version.

	//This program is distributed in the hope that it will be useful,
	//but WITHOUT ANY WARRANTY; without even the implied warranty of
	//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	//GNU General Public License for more details.

	//You should have received a copy of the GNU General Public License
	//along with this program.  If not, see http://www.gnu.org/licenses/

	//Web service Studio has been modifided to understand more complex input types
	//such as Iredeto's Customer Care input type of Base Query Request. 
	




using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace IBS.Utilities.ASMWSTester
{
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public class Configuration
    {
        private CustomHandler[] dataEditors;
        private InvokeProperties invokeSettings;
        private static Configuration masterConfig;
        private CustomHandler[] proxyProperties;
        private CustomHandler[] typeConverters;
        private UiProperties uiSettings;
        private WsdlProperties wsdlSettings;
        private AuthenticationHeaderProperties authenticationHeaderSettings;
        private OtherProperties otherSettings;
        private BatchUpdateAssemblyPropetties batchUpdateAssemblySettings;
        private ServerHistoryProperties serverHistory;

        static Configuration()
        {
            masterConfig = null;
        }

        public Configuration()
        {
            invokeSettings = new InvokeProperties();
        }

        public Configuration Copy()
        {
            MemoryStream stream1 = new MemoryStream();
            XmlSerializer serializer1 = new XmlSerializer(typeof (Configuration));
            serializer1.Serialize(stream1, masterConfig);
            stream1.Position = 0;
            return (serializer1.Deserialize(stream1) as Configuration);
        }

        private static string GetConfigFileName()
        {
            string test = Assembly.GetExecutingAssembly().Location;

            return (Assembly.GetExecutingAssembly().Location + ".options");
        }

        internal string[] GetProxyBaseTypes()
        {
            CustomHandler[] handlerArray1 = ProxyProperties;
            string[] textArray1 = new string[(handlerArray1 != null) ? (handlerArray1.Length + 1) : 1];
            textArray1[0] = "";
            for (int num1 = 1; num1 < textArray1.Length; num1++)
            {
                textArray1[num1] = handlerArray1[num1 - 1].TypeName;
            }
            return textArray1;
        }

        public static void LoadMasterConfig()
        {
            try
            {
                FileStream stream1 = File.OpenRead(GetConfigFileName());
                StreamReader reader1 = new StreamReader(stream1);
                XmlSerializer serializer1 = new XmlSerializer(typeof (Configuration));
                masterConfig = serializer1.Deserialize(reader1) as Configuration;
                stream1.Flush();
                stream1.Close();
            }
            catch
            {
            }
            if (masterConfig == null)
            {
                masterConfig = new Configuration();
            }
            if (masterConfig.DataEditors == null)
            {
                masterConfig.DataEditors = new CustomHandler[0];
            }
            if (masterConfig.TypeConverters == null)
            {
                masterConfig.TypeConverters = new CustomHandler[0];
            }
            if (masterConfig.WsdlSettings == null)
            {
                masterConfig.WsdlSettings = new WsdlProperties();
            }
            if (masterConfig.UiSettings == null)
            {
                masterConfig.UiSettings = new UiProperties();
            }
            if (masterConfig.AuthenticationHeaderSettings == null)
            {
                masterConfig.AuthenticationHeaderSettings = new AuthenticationHeaderProperties();
            }
            if (masterConfig.BatchUpdateAssemblySettings == null)
            {
                masterConfig.BatchUpdateAssemblySettings = new BatchUpdateAssemblyPropetties();
            }
            if (masterConfig.OtherSettings == null)
            {
                masterConfig.OtherSettings = new OtherProperties();
            }
            if (masterConfig.ServerHistory == null)
            {
                masterConfig.ServerHistory = new ServerHistoryProperties();
            }
        }

        public static void SaveMasterConfig()
        {
            FileStream stream1 = File.OpenWrite(GetConfigFileName());
            StreamWriter writer1 = new StreamWriter(stream1);
            new XmlSerializer(typeof (Configuration)).Serialize(writer1, masterConfig);
            stream1.SetLength(stream1.Position);
            stream1.Flush();
            stream1.Close();
        }


        [Browsable(false)]
        public CustomHandler[] DataEditors
        {
            get { return dataEditors; }
            set { dataEditors = value; }
        }

        [Browsable(false)]
        public InvokeProperties InvokeSettings
        {
            get { return invokeSettings; }
            set { invokeSettings = value; }
        }

        internal static Configuration MasterConfig
        {
            get
            {
                if (masterConfig == null)
                {
                    LoadMasterConfig();
                }
                return masterConfig;
            }
            set
            {
                masterConfig = value;
                SaveMasterConfig();
            }
        }

        [Browsable(false)]
        public CustomHandler[] ProxyProperties
        {
            get { return proxyProperties; }
            set { proxyProperties = value; }
        }

        [Browsable(false)]
        public CustomHandler[] TypeConverters
        {
            get { return typeConverters; }
            set { typeConverters = value; }
        }

        public UiProperties UiSettings
        {
            get { return uiSettings; }
            set { uiSettings = value; }
        }

        public WsdlProperties WsdlSettings
        {
            get { return wsdlSettings; }
            set { wsdlSettings = value; }
        }

        public AuthenticationHeaderProperties AuthenticationHeaderSettings
        {
            get { return authenticationHeaderSettings; }
            set { authenticationHeaderSettings = value; }
        }

        public OtherProperties OtherSettings
        {
            get { return otherSettings; }
            set { otherSettings = value; }
        }

        public BatchUpdateAssemblyPropetties BatchUpdateAssemblySettings
        {
            get { return batchUpdateAssemblySettings; }
            set { batchUpdateAssemblySettings = value; }
        }

        [Browsable(true)]
        public ServerHistoryProperties ServerHistory
        {
            get { return serverHistory; }
            set { serverHistory = value; }
        }
    }
}
