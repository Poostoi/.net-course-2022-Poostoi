using System.Collections;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Migration;
using Models;
using Services;

namespace ExportTool;

public class ExportService
{
    public void ExportClientsInFileCSV(List<Client> clients, string pathToFile, string fileName)
    {
        DirectoryInfo dirInfo = new DirectoryInfo(pathToFile);
        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }

        string fullPath = Path.Combine(pathToFile, fileName);
        using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            using (StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
            {
                var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                    { Delimiter = ";" };
                using (var writer = new CsvWriter(streamWriter, config))
                {
                    writer.WriteRecords(clients);
                    writer.Flush();
                }
            }
        }
    }


    public IEnumerable ReadPersonFromCsv(string pathToFile, string fileName)
    {
        IEnumerable clientReader;
        string fullPath = Path.Combine(pathToFile, fileName);
        using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            using (StreamReader streamReader = new StreamReader(fileStream, System.Text.Encoding.UTF8))
            {
                var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                    { Delimiter = ";" };
                using (var reader = new CsvReader(streamReader, config))
                {
                    reader.Read();
                    reader.ReadHeader();
                    clientReader = reader.GetRecords<Client>().ToList();
                }
            }
        }

        return clientReader;
    }
}