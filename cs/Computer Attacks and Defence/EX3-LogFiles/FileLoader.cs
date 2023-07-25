using ExcelDataReader;
using System.Data;

namespace SAR0083_LogFiles
{
    public class FileLoader
    {
        public DataTableCollection LoadExcelFile(string excelFilePath)
        {
            if (File.Exists(excelFilePath))
            {
                using var excelFileStream = File.Open(excelFilePath, FileMode.Open, FileAccess.Read);
                IExcelDataReader reader = ExcelReaderFactory.CreateReader(excelFileStream);
                DataSet dataSet = reader.AsDataSet(new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = true
                    }
                });
                DataTableCollection tables = dataSet.Tables;
                excelFileStream.Close();

                return tables;
            }
            else
            {
                throw new FileNotFoundException("Excel file not found!"); ;
            }
        }

        public DataTable LoadTextFile(string textFilePath, List<string> columnHeaders)
        {
            if (File.Exists(textFilePath))
            {
                DataTable table = new DataTable();
                foreach (string columnHeader in columnHeaders)
                {
                    table.Columns.Add(new DataColumn(columnHeader));
                }
                foreach (string dataLine in File.ReadAllLines(textFilePath))
                {
                    string[] dataCols = dataLine.Split("  ");
                    DataRow row = table.NewRow();
                    for (int i = 0; i < dataCols.Length; i++)
                    {
                        row[columnHeaders[i]] = dataCols[i];
                    }
                    table.Rows.Add(row);
                }

                return table;
            }
            else
            {
                throw new FileNotFoundException("Text file not found!"); ;
            }
        }
    }
}
