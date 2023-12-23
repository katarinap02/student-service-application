using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.Storage;

namespace CLI.DAO;

public class ChairProfessorDao
{
    private readonly List<ChairProfessor> chairprofessor;
    private readonly Storage<ChairProfessor> _storage;


    public ChairProfessorDao()
    {
        _storage = new Storage<ChairProfessor>("chairprofessor.txt");
        chairprofessor = _storage.Load();

    }
    public List<ChairProfessor> GetAllChairProfessor()
    {
        return chairprofessor;
    }

    public ChairProfessor? RemoveChairProfessor(int id)
    {
        ChairProfessor? cp = GetChairProfessorByIdSt(id);
        if (cp == null) return null;

        chairprofessor.Remove(cp);
        _storage.Save(chairprofessor);
        return cp;
    }

    public ChairProfessor? GetChairProfessorByIdSt(int id)
    {
        return chairprofessor.Find(v => v.ChairId == id);
    }

}
