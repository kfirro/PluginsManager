using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Plugins.Core
{
    public class CsvFileConfiguration : FileConfigurationBase
    {
        public CsvFileConfiguration(string filePath) : base(filePath)
        {
            KVP = new Dictionary<string, object>();
        }

        public override bool LoadConfiguration()
        {
            using (StreamReader sr = new StreamReader(FilePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split(',');
                    //File should contain 3 columns: key name, value type, value i.e: "Name,System.String,Kfir"
                    if (columns.Length < 3)
                        throw new NotSupportedException($"Plugins.Core::CsvFileConfiguration::LoadConfiguration CSV File contains rows with less than 3 columns!");
                    KVP.Add(columns[0], Convert.ChangeType(columns[2].ToString(), System.Type.GetType(columns[1])));
                }
            }
            return true;
        }
    }
}
