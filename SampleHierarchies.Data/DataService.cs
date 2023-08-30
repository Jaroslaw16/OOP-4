using Newtonsoft.Json;
using SampleHierarchies.Data;
using System.Diagnostics;

namespace SampleHierarchies.Data;

/// <summary>
/// Data Service Class
/// </summary>
public static class DataService
{
    #region Static Methods

    /// Method used to read json file
    public static Hotel Read(string jsonPath)
    {
        try
        {
            if (jsonPath is null)
            {
                throw new ArgumentNullException(nameof(jsonPath));
            }
            string jsonSource = File.ReadAllText(jsonPath);
            var jsonContent = System.Text.Json.JsonSerializer.Deserialize<Hotel>(jsonSource);
            if (jsonContent == null)
            {
                throw new ArgumentNullException(nameof(jsonContent));
            }
            return jsonContent;
        }
        catch
        {
            Console.WriteLine("Data reading was not successful.");
            return null;
        }
    }

    /// Method used to write json file 
    public static void Write(Hotel hotel, string jsonPath)
    {
        try
        {
            if (jsonPath is null) throw new ArgumentNullException(nameof(jsonPath));
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(hotel));
        }
        catch
        {
            Console.WriteLine("Data saving was not successful.");
        }
    }

    #endregion // Static Methods
}
