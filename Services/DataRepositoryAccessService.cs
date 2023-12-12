using Newtonsoft.Json;

namespace SimpleRESTServerAPI.Services
{
    public class DataRepositoryAccessService<T>
    {
        public T? GetData(string filePath)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(json);
            }

            return default;
        }

        public void SaveData(T data, string filePath)
        {
            var json = JsonConvert.SerializeObject(data);

            // Create the file if it doesn't exists
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            // Write the information on the file
            File.WriteAllText(filePath, json);
        }
    }
}
