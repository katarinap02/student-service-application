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

        public Professor Chief { get; set; }

        public int IdChef { get; set; }

        public List<Professor> Professors { get; set; }

        public Chair()
        {
            Professors = new List<Professor>();
            IdChef = -1;
            Chief = null; 
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
            s= $"ID: {Id,6} | Name: {CName,21}|" +
               $" \nProfessors:  |";
            foreach (Professor p in Professors)
             {

                 s += p.Name;
                 s += " ";
                s += p.Surname;
                s+= "   ";
             }
            s += "\n";

            return s;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            CName,
            IdChef.ToString()


        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            CName = values[1];
            IdChef = int.Parse(values[2]);

        }
    }
}
