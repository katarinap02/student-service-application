using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    internal class Cathedra
    {
        public int Id_Cat { get; set; }
        public string CatName { get; set; }
        // sef

        public List<Professor> Professors { get; set; }

        public Cathedra(int id, string name)
        {
            Id_Cat = id;
            CatName = name;
            Professors= new List<Professor>();
        }

       
    }
}
