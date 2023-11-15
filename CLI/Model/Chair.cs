using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.Storage.Serialization;
using static CLI.Model.Student;

namespace CLI.Model
{
    public class Chair: ISerializable
    {
        public int Id { get; set; }
        public string CName { get; set; }
       
        Professor sef { get; set; }

        public List<Professor> Professors { get; set; }

        public Chair()
        {
            Professors = new List<Professor>();
        }
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
        public override string ToString()
        {
            string s;
            s= $"ID: {Id,6} | Name: {CName,21} |";
             foreach (Professor p in Professors)
             {
                 s += p.ToString();
            }

            return s;
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
