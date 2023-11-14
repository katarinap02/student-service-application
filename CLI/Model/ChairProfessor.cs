using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Storage.Serialization;

namespace CLI.Model
{
    public class ChairProfessor : ISerializable
    {
        public int ChairId { get; set; }

        public int ProfessorId { get; set; }

        public ChairProfessor()
        {
        }

        public ChairProfessor(int chairId, int professorId)
        {
            ChairId = chairId;
            ProfessorId = professorId;
        }
        public void FromCSV(string[] values)
        {
            ChairId = int.Parse(values[0]);
            ProfessorId = int.Parse(values[1]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            ChairId.ToString(),
            ProfessorId.ToString()
        };
            return csvValues;
        }
    }
}
