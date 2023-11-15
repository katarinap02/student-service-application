using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO;

public class ChairDao
{
    private readonly List<Chair> chairs;
    private readonly Storage<Chair> _storage;


    public ChairDao()
    {
        _storage = new Storage<Chair>("chair.txt");
        chairs = _storage.Load();
    }

  

    private int GenerateId()
    {
        if (chairs.Count == 0) return 0;
        return chairs[^1].Id + 1;
    }

    public Chair AddChair(Chair ca)
    {
        ca.Id = GenerateId(); //generisi id za svaku katedru
        chairs.Add(ca);
        _storage.Save(chairs);
        return ca;
    }

    public Chair? UpdateChair(Chair ca)
    {
        Chair? oldca = GetChairById(ca.Id); // sa istim id treba da unesemo nove podatke koji su u st
        if (oldca is null) return null;

        oldca.CName = ca.CName; //apdejtuje se samo ime
       

        _storage.Save(chairs);
        return oldca;
    }

    public Chair? RemoveChair(int id)
    {
        Chair? chair = GetChairById(id);
        if (chair == null) return null;

        chairs.Remove(chair);
        _storage.Save(chairs);
        return chair;
    }

    public Chair? GetChairById(int id)
    {
        return chairs.Find(v => v.Id == id);
    }

    public List<Chair> GetAllChairs()
    {
        return chairs;
    }
}
