using System.Globalization;
using CsvHelper;
using Migration;
using Models;

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

        foreach (var client in clients)
        {
            ExportClientCSV(client, pathToFile, fileName);
        }
    }

    private void ExportClientCSV(Client client, string pathToFile, string fileName)
    {
        string fullPath = GetFullPathToFile(pathToFile, fileName);
        using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                using (var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    writer.WriteField(nameof(client.Id));
                    writer.WriteField(nameof(client.Surname));
                    writer.WriteField(nameof(client.Name));
                    writer.WriteField(nameof(client.DateBirth));
                    writer.WriteField(nameof(client.PassportId));
                    writer.WriteField(nameof(client.Bonus));
                    writer.WriteField(nameof(client.NumberPhone));
                    writer.NextRecord();
                    writer.WriteField(client.Id);
                    writer.WriteField(client.Surname);
                    writer.WriteField(client.Name);
                    writer.WriteField(client.DateBirth);
                    writer.WriteField(client.PassportId);
                    writer.WriteField(client.Bonus);
                    writer.WriteField(client.NumberPhone);
                    writer.NextRecord();
                    writer.Flush();
                }
            }
        }
    }

    public List<Client> ReadPersonFromCsv(string pathToFile, string fileName)
    {
        List<Client> clientReader;
        string fullPath = GetFullPathToFile(pathToFile, fileName);
        using (FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate))
        {
            using (StreamReader streamReader = new StreamReader(fileStream))
            {
                using (var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    reader.Read();
                    clientReader = reader.GetRecords<Client>().ToList();
                }
            }
        }

        return clientReader;
    }


    private string GetFullPathToFile(string pathToFile, string fileName)
    {
        return Path.Combine(pathToFile, fileName);
    }
}