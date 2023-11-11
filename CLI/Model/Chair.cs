using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Storage.Serialization;

namespace CLI.Model
{
    public class Chair: ISerializable
    {
        public int Id { get; set; }
        public string CName { get; set; }
        // sef

        public List<Professor> Professors { get; set; }

        public Chair() { }
        public Chair(int id, string name)
        {
            Id = id;
            CName = name;
            Professors= new List<Professor>();
        }

        public Chair( string name)
        {
           
            CName = name;
            Professors = new List<Professor>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            CName
            

        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            CName = values[1];
            
        }
    }
}
