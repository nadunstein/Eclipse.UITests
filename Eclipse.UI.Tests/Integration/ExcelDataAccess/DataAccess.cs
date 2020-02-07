using System.Data;
using System.IO;
using ExcelDataReader;

namespace Eclipse.UI.Tests.Integration.ExcelDataAccess
{
    public class DataAccess
    {
        public static DataTable ImportExcelData(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var config = new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration
                        {
                            UseHeaderRow = true
                        }
                    };

                    var dataSet = reader.AsDataSet(config);
                    var dataTable = dataSet.Tables[0];
                    return dataTable;
                }
            }
        }
    }
}