﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.Storage;

namespace CLI.DAO
{
    public class ProfessorDao
    {
        private readonly List<Professor> professors;
        private readonly Storage<Professor> _storage;

        public ProfessorDao()
        {
            _storage = new Storage<Professor>("professor.txt");
            professors = _storage.Load();
        }
        private int GenerateId()
        {
            if (professors.Count == 0) return 0;
            return professors[^1].Id + 1;
        }
        public Professor AddProfessor(Professor pr)
        {
            pr.Id = GenerateId(); //generisi id za svakog profesora
            professors.Add(pr);
            _storage.Save(professors);
            return pr;
        }

        public Professor? UpdateProfessor(Professor pr)
        {
            Professor? oldpr = GetProfessorById(pr.Id); // sa istim id treba da unesemo nove podatke koji su u pr
            if (oldpr is null) return null;

            oldpr.Name = pr.Name;
            oldpr.Surname = pr.Surname;
            oldpr.Adress1 = pr.Adress1;
            oldpr.Birthdate = pr.Birthdate;
            oldpr.PhoneNumber= pr.PhoneNumber;
            oldpr.Email = pr.Email; 
            oldpr.Title= pr.Title;
            oldpr.YearS=pr.YearS;   
          
            _storage.Save(professors);
            return oldpr;
        }

        public Professor? RemoveProfessor(int id)
        {
            Professor? professor = GetProfessorById(id);
            if (professor == null) return null;

            professors.Remove(professor);
            _storage.Save(professors);
            return professor;
        }

        public Professor? GetProfessorById(int id)
        {
            return professors.Find(v => v.Id == id);
        }

        public List<Professor> GetAllProfessors()
        {
            return professors;
        }

        public Model.Professor FindProfessorById(List<Professor> professors, int tId)
        {
            return professors.Find(professors => professors.Id == tId);
        }

    }
}
