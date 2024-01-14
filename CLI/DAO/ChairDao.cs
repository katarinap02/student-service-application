using CLI.Model;
using CLI.Observer;
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

    public ObserverSub ChairObserverSub;
    public ChairDao()
    {
        _storage = new Storage<Chair>("chair.txt");
        chairs = _storage.Load();
        ChairObserverSub = new ObserverSub();
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
        ChairObserverSub.NotifyObservers();
        return ca;
    }

    public Chair? UpdateChair(Chair ca)
    {
        Chair? oldca = GetChairById(ca.Id); // sa istim id treba da unesemo nove podatke koji su u st
        if (oldca is null) return null;

        oldca.CName = ca.CName; //apdejtuje se samo ime
       

        _storage.Save(chairs);
        ChairObserverSub.NotifyObservers();
        return oldca;
    }

    public Chair? RemoveChair(int id)
    {
        Chair? chair = GetChairById(id);
        if (chair == null) return null;
        ChairObserverSub.NotifyObservers();

        chairs.Remove(chair);
        _storage.Save(chairs);
        ChairObserverSub.NotifyObservers();
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
    public Model.Chair FindChairById(List<Chair> chairs, int targetId)
    {
        return chairs.Find(chairs => chairs.Id == targetId);
    }

    public Chair setProfessor(Professor professor, Chair ch)
    {
        Chair? oldch = GetChairById(ch.Id); // sa istim id treba da unesemo nove podatke koji su u sub
        if (oldch is null) return null;

        oldch.Chief = professor;
        if (professor != null)
        {
            oldch.IdChef = professor.Id;
        }
        else
        {
            oldch.IdChef = -1;
        }


        _storage.Save(chairs);
        ChairObserverSub.NotifyObservers();
        return oldch;
    }
}
