using Newtonsoft.Json;
using SampleHierarchies.Data;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System.Diagnostics;

namespace SampleHierarchies.Services;

/// <summary>
/// Implementation of data service.
/// </summary>
public static class DataService
{
    #region DataService Implementation

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public static void Write(Hotel hotel, string jsonPath)
    {
        try
        {
            if (jsonPath is null) throw new ArgumentNullException(nameof(jsonPath));
            File.WriteAllText(jsonPath, JsonConvert.SerializeObject(hotel));
            Console.WriteLine("Data saving to: '{0}' was successful.", jsonPath);
        }
        catch
        {
            Console.WriteLine("Data saving was not successful.");
        }
    }

    #endregion // DataService Implementation
}
