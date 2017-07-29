﻿using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace CurtainFireMakerPlugin
{
    internal class Configuration
    {
        private string CondigPath { get; }

        public string ScriptPath { get; set; }
        public string SettingScriptPath { get; set; }
        public string ModullesDirPath { get; set; }
        public string ExportDirPath { get; set; }
        public string ModelName { get; set; }
        public string ModelDescription { get; set; }
        public bool KeepLogOpen { get; set; }

        public Configuration(string path)
        {
            CondigPath = path;
        }

        public void Load()
        {
            var doc = new XmlDocument();
            doc.Load(CondigPath);

            XmlNode rootNode = doc.SelectSingleNode(@"//configuration");
            ScriptPath = GetPath(rootNode.SelectSingleNode("script").InnerText);
            SettingScriptPath = GetPath(rootNode.SelectSingleNode("setting").InnerText);
            ModullesDirPath = GetPath(rootNode.SelectSingleNode("scripts").InnerText);
            ExportDirPath = GetPath(rootNode.SelectSingleNode("export").InnerText);
            ModelName = rootNode.SelectSingleNode("model_name").InnerText;
            ModelDescription = rootNode.SelectSingleNode("model_description").InnerText;
            KeepLogOpen = bool.Parse(rootNode.SelectSingleNode("keep_log_open").InnerText);
        }

        public static void Save()
        {
            var doc = new XmlDocument();

            XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.CreateElement("configuration");

            doc.AppendChild(declaration);
            doc.AppendChild(root);

            XmlElement element = doc.CreateElement("script");
            element.InnerText = Plugin.Instance.ScriptPath.Replace(Plugin.Instance.CurtainFireMakerPath + "\\", "");
            root.AppendChild(element);

            element = doc.CreateElement("setting");
            element.InnerText = Plugin.Instance.SettingScriptPath.Replace(Plugin.Instance.CurtainFireMakerPath + "\\", "");
            root.AppendChild(element);

            element = doc.CreateElement("scripts");
            element.InnerText = Plugin.Instance.ModullesDirPath.Replace(Plugin.Instance.CurtainFireMakerPath + "\\", "");
            root.AppendChild(element);

            element = doc.CreateElement("export");
            element.InnerText = Plugin.Instance.ExportDirPath.Replace(Plugin.Instance.CurtainFireMakerPath + "\\", "");
            root.AppendChild(element);

            element = doc.CreateElement("model_name");
            element.InnerText = Plugin.Instance.ModelName;
            root.AppendChild(element);

            element = doc.CreateElement("model_description");
            element.InnerText = Plugin.Instance.ModelDescription;
            root.AppendChild(element);

            element = doc.CreateElement("keep_log_open");
            element.InnerText = Plugin.Instance.KeepLogOpen.ToString();
            root.AppendChild(element);

            doc.Save(CondigPath);
        }

        private static string GetPath(string path)
        {
            return Path.IsPathRooted(path) ? path : Plugin.Instance.CurtainFireMakerPath + "\\" + path;
        }
    }
}
