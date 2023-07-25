using Common.Class;
using DataLayerInterface;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace DataLayerXML
{
    public class CellXml : ICellData
    {
        private Xml xmlCell = new Xml("cells.xml", "cells", "cell");
        private Xml xmlPrisoner = new Xml("prisoners.xml", "prisoners", "prisoner");

        public int Insert(Cell cell)
        {
            Collection<Cell> cells = Select();
            xmlCell.Insert(
                new List<(string, string)>
                {
                    ("cell_id", (cells.Count + 1).ToString()),
                    ("occupied", cell.Occupied.ToString()),
                    ("capacity", cell.Capacity.ToString())
                }
            );

            return 0;
        }

        public int Update(Cell cell)
        {
            xmlCell.Update(
                new List<(string, string)>
                {
                    ("cell_id", cell.CellId.ToString())
                },
                new List<(string, string)>
                {
                    ("cell_id", cell.CellId.ToString()),
                    ("occupied", cell.Occupied.ToString()),
                    ("capacity", cell.Capacity.ToString())
                }
            );

            return 0;
        }

        public Collection<Cell> Select()
        {
            Collection<Cell> items = new Collection<Cell>();
            foreach (XmlNode element in xmlCell.SelectAll())
            {
                Cell item = new Cell();
                item.CellId = int.Parse(element.Attributes["cell_id"].Value);
                item.Occupied = int.Parse(element.Attributes["occupied"].Value);
                item.Capacity = int.Parse(element.Attributes["capacity"].Value);
                items.Add(item);
            }

            return items;
        }

        public Cell Select(int cellId)
        {
            XmlNode element = xmlCell.SelectOne(
                new List<(string, string)>
                {
                    ("cell_id", cellId.ToString())
                }
            );
            Cell item = new Cell();
            item.CellId = int.Parse(element.Attributes["cell_id"].Value);
            item.Occupied = int.Parse(element.Attributes["occupied"].Value);
            item.Capacity = int.Parse(element.Attributes["capacity"].Value);

            return item;
        }

        public Cell SelectForPrisoner(int prisonerId)
        {
            XmlNode element = xmlPrisoner.SelectOne(
                new List<(string, string)>
                {
                    ("prisoner_id", prisonerId.ToString())
                }
            );

            return Select(int.Parse(element.Attributes["cell_id"].Value));
        }
    }
}
