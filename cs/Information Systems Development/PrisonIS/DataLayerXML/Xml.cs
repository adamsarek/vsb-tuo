using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace DataLayerXML
{
    public class Xml
    {
        private string fileName;
        private string rootElementName;
        private string dataElementName;
        private XmlDocument document = new XmlDocument();

        public XmlDocument Document
        {
            get
            {
                return document;
            }
        }

        public Xml(string fileName, string rootElementName, string dataElementName)
        {
            if (HttpContext.Current != null)
            {
                this.fileName = HttpContext.Current.Server.MapPath("~/xml/" + fileName);
            }
            else
            {
                this.fileName = Application.StartupPath + "\\..\\..\\WebApp\\xml\\" + fileName;
            }

            this.rootElementName = rootElementName;
            this.dataElementName = dataElementName;
        }

        private void Load()
        {
            try
            {
                document.Load(fileName);
            }
            catch (System.IO.FileNotFoundException)
            {
                using (XmlWriter writer = XmlWriter.Create(fileName))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement(rootElementName);
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
                document.Load(fileName);
            }
        }

        private void Save()
        {
            document.Save(fileName);
        }

        public void Insert(List<(string, string)> attrs)
        {
            Load();
            XmlNode rootElement = document.SelectSingleNode(rootElementName);
            XmlNode element = document.CreateElement(dataElementName);
            for (int i = 0; i < attrs.Count; i++)
            {
                XmlAttribute attr = document.CreateAttribute(attrs[i].Item1);
                attr.InnerText = attrs[i].Item2;
                element.Attributes.Append(attr);
            }
            rootElement.AppendChild(element);
            Save();
        }

        public void Update(List<(string, string)> findAttrs, List<(string, string)> attrs)
        {
            XmlNode element = SelectOne(findAttrs);
            for (int i = 0; i < attrs.Count; i++)
            {
                element.Attributes[attrs[i].Item1].Value = attrs[i].Item2;
            }
            Save();
        }

        public List<XmlNode> SelectAll()
        {
            Load();
            List<XmlNode> elements = new List<XmlNode>();
            foreach (XmlNode element in document.SelectNodes("/" + rootElementName + "/" + dataElementName))
            {
                elements.Add(element);
            }

            return elements;
        }

        public List<XmlNode> SelectAll(List<(string, string)> findAttrs)
        {
            Load();
            string attrsString = "";
            for (int i = 0; i < findAttrs.Count; i++)
            {
                attrsString += "[@" + findAttrs[i].Item1 + "='" + findAttrs[i].Item2 + "']";
            }
            List<XmlNode> elements = new List<XmlNode>();
            foreach (XmlNode element in document.SelectNodes("/" + rootElementName + "/" + dataElementName + attrsString))
            {
                elements.Add(element);
            }

            return elements;
        }

        public XmlNode SelectOne(List<(string, string)> findAttrs)
        {
            Load();
            string attrsString = "";
            for (int i = 0; i < findAttrs.Count; i++)
            {
                attrsString += "[@" + findAttrs[i].Item1 + "='" + findAttrs[i].Item2 + "']";
            }

            return document.SelectSingleNode("/" + rootElementName + "/" + dataElementName + attrsString);
        }
    }
}
