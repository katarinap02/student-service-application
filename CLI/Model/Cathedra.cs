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
    public class Cathedra: ISerializable
    {
        public int Id { get; set; }
        public string CatName { get; set; }
        // sef

        public List<Professor> Professors { get; set; }

        public Cathedra() { }
        public Cathedra(int id, string name)
        {
            Id = id;
            CatName = name;
            Professors= new List<Professor>();
        }

        public Cathedra( string name)
        {
           
            CatName = name;
            Professors = new List<Professor>();
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            CatName
            

        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            CatName = values[1];
            
        }
    }
}
