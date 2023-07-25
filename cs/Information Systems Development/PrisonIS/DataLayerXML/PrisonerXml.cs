using Common.Class;
using DataLayerInterface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace DataLayerXML
{
    public class PrisonerXml : IPrisonerData
    {
        private Xml xmlPrisoner = new Xml("prisoners.xml", "prisoners", "prisoner");
        private Xml xmlVisit = new Xml("visits.xml", "visits", "visit");

        public int Insert(Prisoner prisoner)
        {
            Collection<Prisoner> prisoners = Select();
            xmlPrisoner.Insert(
                new List<(string, string)>
                {
                    ("prisoner_id", (prisoners.Count + 1).ToString()),
                    ("firstName", prisoner.FirstName.ToString()),
                    ("lastName", prisoner.LastName.ToString()),
                    ("gender", prisoner.Gender.ToString()),
                    ("birthDate", prisoner.BirthDate.ToString()),
                    ("punishmentStartDate", prisoner.PunishmentStartDate.ToString()),
                    ("punishmentEndDate", prisoner.PunishmentEndDate.ToString()),
                    ("released", prisoner.Released.ToString()),
                    ("cell_id", prisoner.Cell.CellId.ToString())
                }
            );

            return 0;
        }

        public int Update(Prisoner prisoner)
        {
            xmlPrisoner.Update(
                new List<(string, string)>
                {
                    ("prisoner_id", prisoner.PrisonerId.ToString())
                },
                new List<(string, string)>
                {
                    ("prisoner_id", prisoner.PrisonerId.ToString()),
                    ("firstName", prisoner.FirstName.ToString()),
                    ("lastName", prisoner.LastName.ToString()),
                    ("gender", prisoner.Gender.ToString()),
                    ("birthDate", prisoner.BirthDate.ToString()),
                    ("punishmentStartDate", prisoner.PunishmentStartDate.ToString()),
                    ("punishmentEndDate", prisoner.PunishmentEndDate.ToString()),
                    ("released", prisoner.Released.ToString()),
                    ("cell_id", prisoner.Cell.CellId.ToString())
                }
            );

            return 0;
        }

        public Collection<Prisoner> Select()
        {
            Collection<Prisoner> items = new Collection<Prisoner>();
            foreach (XmlNode element in xmlPrisoner.SelectAll())
            {
                Prisoner item = new Prisoner();
                item.PrisonerId = int.Parse(element.Attributes["prisoner_id"].Value);
                item.FirstName = element.Attributes["firstName"].Value;
                item.LastName = element.Attributes["lastName"].Value;
                item.Gender = char.Parse(element.Attributes["gender"].Value);
                item.BirthDate = DateTime.Parse(element.Attributes["birthDate"].Value);
                item.PunishmentStartDate = DateTime.Parse(element.Attributes["punishmentStartDate"].Value);
                item.PunishmentEndDate = DateTime.Parse(element.Attributes["punishmentEndDate"].Value);
                item.Released = char.Parse(element.Attributes["released"].Value);
                items.Add(item);
            }

            return items;
        }

        public Prisoner Select(int prisonerId)
        {
            XmlNode element = xmlPrisoner.SelectOne(
                new List<(string, string)>
                {
                    ("prisoner_id", prisonerId.ToString())
                }
            );
            Prisoner item = new Prisoner();
            item.PrisonerId = int.Parse(element.Attributes["prisoner_id"].Value);
            item.FirstName = element.Attributes["firstName"].Value;
            item.LastName = element.Attributes["lastName"].Value;
            item.Gender = char.Parse(element.Attributes["gender"].Value);
            item.BirthDate = DateTime.Parse(element.Attributes["birthDate"].Value);
            item.PunishmentStartDate = DateTime.Parse(element.Attributes["punishmentStartDate"].Value);
            item.PunishmentEndDate = DateTime.Parse(element.Attributes["punishmentEndDate"].Value);
            item.Released = char.Parse(element.Attributes["released"].Value);

            return item;
        }

        public Collection<Prisoner> SelectForCell(int cellId)
        {
            Collection<Prisoner> items = new Collection<Prisoner>();
            foreach (XmlNode element in xmlPrisoner.SelectAll(
                new List<(string, string)>
                {
                    ("cell_id", cellId.ToString())
                }
            ))
            {
                Prisoner item = new Prisoner();
                item.PrisonerId = int.Parse(element.Attributes["prisoner_id"].Value);
                item.FirstName = element.Attributes["firstName"].Value;
                item.LastName = element.Attributes["lastName"].Value;
                item.Gender = char.Parse(element.Attributes["gender"].Value);
                item.BirthDate = DateTime.Parse(element.Attributes["birthDate"].Value);
                item.PunishmentStartDate = DateTime.Parse(element.Attributes["punishmentStartDate"].Value);
                item.PunishmentEndDate = DateTime.Parse(element.Attributes["punishmentEndDate"].Value);
                item.Released = char.Parse(element.Attributes["released"].Value);
                items.Add(item);
            }

            return items;
        }

        public Prisoner SelectForVisit(int visitId)
        {
            XmlNode element = xmlVisit.SelectOne(
                new List<(string, string)>
                {
                    ("visit_id", visitId.ToString())
                }
            );

            return Select(int.Parse(element.Attributes["prisoner_id"].Value));
        }
    }
}
