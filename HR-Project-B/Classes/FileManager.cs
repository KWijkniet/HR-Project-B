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

        public FileManager(string filename, string[] dir = null)
        {
            path = Environment.CurrentDirectory + "/../../HR-Project-B/Data/";
            //for (int i = 0; i < dir.Length; i++)
            //{
            //    path += dir[i] + "/";
            //}
            path += filename;
        }

        public string[] ReadJSON()
        {
            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();
            sr.Close();
            return JsonConvert.DeserializeObject<string[]>(json);
        }

        public void WriteJSON(string[] json)
        {
            StreamWriter sw = new StreamWriter(path, false);
            sw.Write(JsonConvert.SerializeObject(json, Formatting.Indented));
            sw.Close();
        }

        //public void UpdateJSON(int id, string json)
        //{
        //    StreamReader sr = new StreamReader(path);
        //    dynamic data = JsonConvert.DeserializeObject(sr.ReadToEnd());
        //    sr.Close();

        //    int i = 0;
        //    foreach (var item in data)
        //    {
        //        var type = item.GetType().GetProperty("id");
        //        if(type == id)
        //        {
        //            string[] lines = File.ReadAllLines(path);
        //            lines[i] = json;


        //            StreamWriter sw = new StreamWriter(path, false);
        //            foreach (string line in lines)
        //            {
        //                sw.WriteLine(line);
        //            }
        //            sw.Close();
        //            return;
        //        }

        //        i++;
        //    }
        //}
    }
}
