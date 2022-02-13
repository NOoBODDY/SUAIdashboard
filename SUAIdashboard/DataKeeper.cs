using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using SUAIdashboard.Model;

namespace SUAIdashboard
{
    public class DataKeeper<T>
    {
        string Path;
        public DataKeeper(string fileName)
        {
            Path = Directory.GetCurrentDirectory() +"\\" + fileName;
        }

        public async void SaveAsync(T data)
        {
            JObject json = JObject.FromObject(data);
            using (StreamWriter sw = new StreamWriter(Path, false, System.Text.Encoding.Default))
            {
                await sw.WriteLineAsync(json.ToString());
            }
        }
        public T Load()
        {
            string data;
            FileInfo info = new FileInfo(Path);
            if (info.Exists)
            {
                using (StreamReader sr = new StreamReader(Path))
                {
                    data = sr.ReadToEnd();
                }
                JObject json = JObject.Parse(data);
                return json.ToObject<T>();
            }
            return default(T);
        }

    }
    
}
