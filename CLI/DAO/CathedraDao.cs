using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO
{
    public class CathedraDao
    {
        private readonly List<Cathedra> chairs;
        private readonly Storage<Cathedra> _storage;

    
        public CathedraDao()
        {
            _storage = new Storage<Cathedra>("cathedra.txt");
            chairs = _storage.Load();
        }

      

        private int GenerateId()
        {
            if (chairs.Count == 0) return 0;
            return chairs[^1].Id + 1;
        }

        public Cathedra AddCathedra(Cathedra ca)
        {
            ca.Id = GenerateId(); //generisi id za svaku katedru
            chairs.Add(ca);
            _storage.Save(chairs);
            return ca;
        }

        public Cathedra? UpdateCathedra(Cathedra ca)
        {
            Cathedra? oldca = GetCathedraById(ca.Id); // sa istim id treba da unesemo nove podatke koji su u st
            if (oldca is null) return null;

            oldca.CatName = ca.CatName; //apdejtuje se samo ime
           

            _storage.Save(chairs);
            return oldca;
        }

        public Cathedra? RemoveCathedra(int id)
        {
            Cathedra? chair = GetCathedraById(id);
            if (chair == null) return null;

            chairs.Remove(chair);
            _storage.Save(chairs);
            return chair;
        }

        private Cathedra? GetCathedraById(int id)
        {
            return chairs.Find(v => v.Id == id);
        }

        public List<Cathedra> GetAllCathedras()
        {
            return chairs;
        }
    }
}
