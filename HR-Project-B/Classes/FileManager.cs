using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace HR_Project_B
{
    class FileManager
    {
        public string path;
        public bool debugging = true;

        public FileManager(string filename)
        {
            path = "";
            if (debugging)
            {
                path = Path.GetFullPath(@"data").Split("bin")[0] + "data\\" + filename;
            }
            else
            {
                path = "Data/" + filename;
            }
        }

        public dynamic[] ReadJSON()
        {
            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            sr.Close();
            return JsonConvert.DeserializeObject<dynamic[]>(json);
        }

        public void WriteJSON(dynamic[] json)
        {
            StreamWriter sw = new StreamWriter(path, false);
            sw.Write(JsonConvert.SerializeObject(json, Formatting.Indented));
            sw.Close();
        }

        public void UpdateJSON(int id, string json)
        {
            StreamReader sr = new StreamReader(path);
            dynamic data = JsonConvert.DeserializeObject(sr.ReadToEnd());
            sr.Close();

            int i = 0;
            foreach (var item in data)
            {
                var type = item.GetType().GetProperty("id");
                if (type == id)
                {
                    string[] lines = File.ReadAllLines(path);
                    lines[i] = json;


                    StreamWriter sw = new StreamWriter(path, false);
                    foreach (string line in lines)
                    {
                        sw.WriteLine(line);
                    }
                    sw.Close();
                    return;
                }

                i++;
            }
        }
    }
}
