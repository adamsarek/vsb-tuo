using Common.Class;
using DataLayerInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace DataLayerXML
{
    public class VisitorXml : IVisitorData
    {
        private Xml xmlVisitor = new Xml("visitors.xml", "visitors", "visitor");
        private Xml xmlVisit = new Xml("visits.xml", "visits", "visit");

        public int Insert(Visitor visitor)
        {
            Collection<Visitor> visitors = Select();
            xmlVisitor.Insert(
                new List<(string, string)>
                {
                    ("visitor_id", (visitors.Count + 1).ToString()),
                    ("firstName", visitor.FirstName.ToString()),
                    ("lastName", visitor.LastName.ToString()),
                    ("gender", visitor.Gender.ToString()),
                    ("birthDate", visitor.BirthDate.ToString()),
                    ("forbidden", visitor.Forbidden.ToString())
                }
            );

            return 0;
        }

        public int Update(Visitor visitor)
        {
            xmlVisitor.Update(
                new List<(string, string)>
                {
                    ("visitor_id", visitor.VisitorId.ToString())
                },
                new List<(string, string)>
                {
                    ("visitor_id", visitor.VisitorId.ToString()),
                    ("firstName", visitor.FirstName.ToString()),
                    ("lastName", visitor.LastName.ToString()),
                    ("gender", visitor.Gender.ToString()),
                    ("birthDate", visitor.BirthDate.ToString()),
                    ("forbidden", visitor.Forbidden.ToString())
                }
            );

            return 0;
        }

        public Collection<Visitor> Select()
        {
            Collection<Visitor> items = new Collection<Visitor>();
            foreach (XmlNode element in xmlVisitor.SelectAll())
            {
                Visitor item = new Visitor();
                item.VisitorId = int.Parse(element.Attributes["visitor_id"].Value);
                item.FirstName = element.Attributes["firstName"].Value;
                item.LastName = element.Attributes["lastName"].Value;
                item.Gender = char.Parse(element.Attributes["gender"].Value);
                item.BirthDate = DateTime.Parse(element.Attributes["birthDate"].Value);
                item.Forbidden = char.Parse(element.Attributes["forbidden"].Value);
                items.Add(item);
            }

            return items;
        }

        public Visitor Select(int visitorId)
        {
            XmlNode element = xmlVisitor.SelectOne(
                new List<(string, string)>
                {
                    ("visitor_id", visitorId.ToString())
                }
            );
            Visitor item = new Visitor();
            item.VisitorId = int.Parse(element.Attributes["visitor_id"].Value);
            item.FirstName = element.Attributes["firstName"].Value;
            item.LastName = element.Attributes["lastName"].Value;
            item.Gender = char.Parse(element.Attributes["gender"].Value);
            item.BirthDate = DateTime.Parse(element.Attributes["birthDate"].Value);
            item.Forbidden = char.Parse(element.Attributes["forbidden"].Value);

            return item;
        }

        public Visitor SelectForVisit(int visitId)
        {
            XmlNode element = xmlVisit.SelectOne(
                new List<(string, string)>
                {
                    ("visit_id", visitId.ToString())
                }
            );

            return Select(int.Parse(element.Attributes["visitor_id"].Value));
        }
    }
}
