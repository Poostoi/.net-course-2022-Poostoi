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

        var clientsInFile = ReadPersonFromCsv(pathToFile, fileName);
        string fullPath = Path.Combine(pathToFile, fileName);
        using (FileStream fileStream = new FileStream(fullPath, FileMode.Append))
        {
            using (StreamWriter streamWriter = new StreamWriter(fileStream, System.Text.Encoding.UTF8))
            {
                var config = new CsvConfiguration(CultureInfo.CurrentCulture)
                    { Delimiter = ";" };
                using (var writer = new CsvWriter(streamWriter, config))
                {
                    if(clientsInFile==null)
                    {
                        writer.WriteRecords(clients);
                        writer.Flush();
                    }
                    else
                    {
                        foreach (var client in clients)
                        {
                            writer.WriteRecord(client);
                            writer.NextRecord();
                        }
                        writer.Flush();
                    }
                }
            }
        }
    }


    public List<Client>? ReadPersonFromCsv(string pathToFile, string fileName)
    {
        List<Client> clientReader = null;
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
                    if(reader.Parser.Count!=0)
                    {
                        reader.ReadHeader();
                        clientReader = reader.GetRecords<Client>().ToList();
                    }
                }
            }
        }

        return clientReader;
    }
}