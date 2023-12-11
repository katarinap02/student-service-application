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

        public int bind { get; set; }

        Professor Chief { get; set; }

        public List<Professor> Professors { get; set; }

        public Chair()
        {
            Professors = new List<Professor>();
        }
        public Chair(int id, string name, Professor p)
        {
            Id = id;
            CName = name;
            Chief = p;
            Professors= new List<Professor>();
        }

        public Chair( string name, Professor p)
        {
           
            CName = name;
            Chief = p;
            Professors = new List<Professor>();
        }
        public override string ToString()
        {
            string s;
            s= $"ID: {Id,6} | Name: {CName,21}| Chief's Name: {Chief.Name,10}| Chief's Surname: {Chief.Surname,10} |" +
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
            Chief.ToCSV1()[0],
            Chief.ToCSV1()[1],
            Chief.ToCSV1()[2]


        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            CName = values[1];
            Chief = new Professor();
            Chief.FromCSV1(new string[] { values[1], values[2], values[3] });

        }
    }
}
