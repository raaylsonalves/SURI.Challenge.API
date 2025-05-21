using SURI.Challenge.API.Models;
using System.Text.Json;

namespace SURI.Challenge.API.Services;

public class JsonDataService
{
    private readonly string _filePath = "./data/bills.json";

    public List<Bill>? GetBills()
    {
        if (!File.Exists(_filePath))
            return [];

        var jsonData = File.ReadAllText(_filePath);
        var bills = JsonSerializer.Deserialize<Dictionary<string, List<Bill>>>(jsonData);

        return bills?["bills"] ?? [];
    }

    public void SaveBills(List<Bill> bills)
    {
        string jsonData = JsonSerializer.Serialize(new { bills }, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, jsonData);
    }
}
