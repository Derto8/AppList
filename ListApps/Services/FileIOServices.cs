using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;
using ListApps.Models;

namespace ListApps.Services
{
    internal class FileIOServices
    {
        private readonly string Path;

        public FileIOServices(string Path)
        {
            this.Path = Path;
        }

        public BindingList<AppModel> LoadData()
        {
            var fileExists = File.Exists(Path);
            if (!fileExists)
            {
                File.CreateText(Path).Dispose();
                return new BindingList<AppModel>();
            }
            using (var reader = File.OpenText(Path))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<AppModel>>(fileText);
            }
        }

        public void SaveData(object appDataList)
        {
            using (StreamWriter writer = File.CreateText(Path))
            {
                string output = JsonConvert.SerializeObject(appDataList);
                writer.Write(output);
            }
        }
    }
}
