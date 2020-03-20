using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace RO_bot.Config
{
    class JsonReader
    {
        private const string DEFAULTPATH = "file.json";
        private readonly string _path;

        public JsonReader() => _path = null;

        public JsonReader(string path) => _path = path;

        public T Load<T>(string path = DEFAULTPATH)
        {
            if (_path != null && path == DEFAULTPATH)
                path = _path;

            if (!File.Exists(_path))
            {
                throw new Exception($"File \"{path}\" not found");                
            }
            using (StreamReader sr = File.OpenText(path))
            {
                using (JsonTextReader jtr = new JsonTextReader(sr))
                {
                    return new JsonSerializer().Deserialize<T>(jtr);
                }
            }
        }
    }
}
