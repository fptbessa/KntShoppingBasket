using System.IO.Abstractions;

namespace Json.Repository.Repositories.Generic
{
    //The generic part of our repositories - fetching data from a file
    public class JsonRepository : IJsonRepository
    {
        private readonly string jsonDataPath;
        private readonly IFileSystem fileSystem;

        public JsonRepository(IFileSystem fileSystem, string jsonDataPath)
        {
            this.jsonDataPath = jsonDataPath;
            this.fileSystem = fileSystem;
        }

        public string GetJsonDataAsString()
        {
            //Decision: Using JsonConvert to deserialize the JsonData, many other options are available
            //but I think this is appropriate for a code challenge
            return this.fileSystem.File.ReadAllText(this.jsonDataPath);
        }
    }
}