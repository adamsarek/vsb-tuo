using Common.Class;
using DataLayerInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace DataLayerXML
{
    public class VisitXml : IVisitData
    {
        private Xml xmlVisit = new Xml("visits.xml", "visits", "visit");

        public int Insert(Visit visit)
        {
            Collection<Visit> visits = Select();
            xmlVisit.Insert(
                new List<(string, string)>
                {
                    ("visit_id", (visits.Count + 1).ToString()),
                    ("visitDate", visit.VisitDate.ToString()),
                    ("allowed", visit.Allowed.ToString()),
                    ("prisoner_id", visit.Prisoner.PrisonerId.ToString()),
                    ("visitor_id", visit.Visitor.VisitorId.ToString())
                }
            );

            return 0;
        }

        public int Update(Visit visit)
        {
            xmlVisit.Update(
                new List<(string, string)>
                {
                    ("visit_id", visit.VisitId.ToString())
                },
                new List<(string, string)>
                {
                    ("visit_id", visit.VisitId.ToString()),
                    ("visitDate", visit.VisitDate.ToString()),
                    ("allowed", visit.Allowed.ToString()),
                    ("prisoner_id", visit.Prisoner.PrisonerId.ToString()),
                    ("visitor_id", visit.Visitor.VisitorId.ToString())
                }
            );

            return 0;
        }

        public Collection<Visit> Select()
        {
            Collection<Visit> items = new Collection<Visit>();
            foreach (XmlNode element in xmlVisit.SelectAll())
            {
                Visit item = new Visit();
                item.VisitId = int.Parse(element.Attributes["visit_id"].Value);
                item.VisitDate = DateTime.Parse(element.Attributes["visitDate"].Value);
                item.Allowed = char.Parse(element.Attributes["allowed"].Value);
                items.Add(item);
            }

            return items;
        }

        public Visit Select(int visitId)
        {
            XmlNode element = xmlVisit.SelectOne(
                new List<(string, string)>
                {
                    ("visit_id", visitId.ToString())
                }
            );
            Visit item = new Visit();
            item.VisitId = int.Parse(element.Attributes["visit_id"].Value);
            item.VisitDate = DateTime.Parse(element.Attributes["visitDate"].Value);
            item.Allowed = char.Parse(element.Attributes["allowed"].Value);

            return item;
        }

        public Collection<Visit> SelectForPrisoner(int prisonerId)
        {
            Collection<Visit> items = new Collection<Visit>();
            foreach (XmlNode element in xmlVisit.SelectAll(
                new List<(string, string)>
                {
                    ("prisoner_id", prisonerId.ToString())
                }
            ))
            {
                Visit item = new Visit();
                item.VisitId = int.Parse(element.Attributes["visit_id"].Value);
                item.VisitDate = DateTime.Parse(element.Attributes["visitDate"].Value);
                item.Allowed = char.Parse(element.Attributes["allowed"].Value);
                items.Add(item);
            }

            return items;
        }

        public Collection<Visit> SelectForVisitor(int visitorId)
        {
            Collection<Visit> items = new Collection<Visit>();
            foreach (XmlNode element in xmlVisit.SelectAll(
                new List<(string, string)>
                {
                    ("visitor_id", visitorId.ToString())
                }
            ))
            {
                Visit item = new Visit();
                item.VisitId = int.Parse(element.Attributes["visit_id"].Value);
                item.VisitDate = DateTime.Parse(element.Attributes["visitDate"].Value);
                item.Allowed = char.Parse(element.Attributes["allowed"].Value);
                items.Add(item);
            }

            return items;
        }
    }
}
