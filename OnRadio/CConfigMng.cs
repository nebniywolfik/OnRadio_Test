using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace OnRadio
{

    public class CConfigMng
    {
        private string m_sConfigFileName = Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath) + ".xml";
        private CConfigDO m_oConfig = new CConfigDO();

        public CConfigDO Config
        {
            get { return m_oConfig; }
            set { m_oConfig = value; }
        }

        // Загрузить файл конфигурации
        public void LoadConfig()
        {
            if (System.IO.File.Exists(m_sConfigFileName))
            {
                System.IO.StreamReader srReader = File.OpenText(m_sConfigFileName);
                Type tType = m_oConfig.GetType();
                System.Xml.Serialization.XmlSerializer xsSerializer = new XmlSerializer(tType);
                object oData = xsSerializer.Deserialize(srReader);
                m_oConfig = (CConfigDO)oData;
                srReader.Close();
            }
        }

        // Сохранить файл конфигурации
        public void SaveConfig()
        {
            StreamWriter swWriter = File.CreateText(m_sConfigFileName);
            Type tType = m_oConfig.GetType();
            if (tType.IsSerializable)
            {
                XmlSerializer xsSerializer = new System.Xml.Serialization.XmlSerializer(tType);
                xsSerializer.Serialize(swWriter, m_oConfig);
                swWriter.Close();
            }
        }
    }
}
