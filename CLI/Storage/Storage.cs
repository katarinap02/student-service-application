﻿using CLI.Storage.Serialization;

namespace CLI.Storage;

public class Storage<T> where T : ISerializable, new()
{
    private readonly string _filePath = AppDomain.CurrentDomain.BaseDirectory + @"../../../../CLI/Data/{0}";
    private readonly Serializer<T> _serializer = new();

    public Storage(string fileName)
    {
        _filePath = string.Format(_filePath, fileName);
    }

    public List<T> Load()
    {
        // ukoliko fajl ne postoji napravi novi fajl i
        // zatvori stream za pisanje u fajl
        if (!File.Exists(_filePath))
        {
            FileStream fs = File.Create(_filePath);
            fs.Close();
        }

        IEnumerable<string> lines = File.ReadLines(_filePath);
        List<T> objects = _serializer.FromCSV(lines);

        return objects;
    }

    public void Save(List<T> objects)
    {
        string serializedVehicles = _serializer.ToCSV(objects);
        using (StreamWriter streamWriter = new StreamWriter(_filePath))
        {
            streamWriter.Write(serializedVehicles);
        }
    }
}