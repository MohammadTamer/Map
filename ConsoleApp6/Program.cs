using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class MapDetails
{
    public int IntersectionCount { get; set; }
    public int RoadCount { get; set; }
    public List<Intersection> Intersections { get; set; } = new List<Intersection>();
    public List<Road> Roads { get; set; } = new List<Road>();
}

public class Intersection
{
    public int Id { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
}

public class Road
{
    public int FirstPoint { get; set; }
    public int SecondPoint { get; set; }
    public double Length { get; set; }
    public double Speed { get; set; }
}

public class DataReader
{
    public static MapDetails ReadMap(string path)
    {
        // Create a new MapDetails instance to store our map data.

        MapDetails mapData = new MapDetails();

        if (File.Exists(path))
        {
            // Read all lines from the file.
            string[] lines = File.ReadAllLines(path);
            int lineNumber = 0;
            // First line: number of intersections.
            if (lineNumber < lines.Length)
            {
                mapData.IntersectionCount = int.Parse(lines[lineNumber++]);
            }
            // Next lines: details for each intersection.
            for (int i = 0; i < mapData.IntersectionCount; i++)
            {
                if (lineNumber < lines.Length)
                {
                    string[] parts = lines[lineNumber++].Split(' ');
                    if (parts.Length == 3)
                    {
                        int id = int.Parse(parts[0]);
                        double x = double.Parse(parts[1]);
                        double y = double.Parse(parts[2]);
                        Intersection tmp = new Intersection();
                        tmp.Id = id;
                        tmp.X = x;
                        tmp.Y = y;
                        mapData.Intersections.Add(tmp);
                    }
                }
            }
            // Next line: number of roads.
            if (lineNumber < lines.Length)
            {
                mapData.RoadCount = int.Parse(lines[lineNumber++]);
            }
            // Following lines: details for each road.
            for (int i = 0; i < mapData.RoadCount; i++)
            {
                if (lineNumber < lines.Length)
                {
                    string[] parts = lines[lineNumber++].Split(' ');
                    if (parts.Length == 4)
                    {
                        int firstId = int.Parse(parts[0]);
                        int secondId = int.Parse(parts[1]);
                        double length = double.Parse(parts[2]);
                        double speed = double.Parse(parts[3]);
                        Road tmp = new Road();
                        tmp.FirstPoint = firstId;
                        tmp.SecondPoint = secondId;
                        tmp.Length = length;
                        tmp.Speed = speed;
                        mapData.Roads.Add(tmp);
                    }
                }
            }
        }
        return mapData;
    }

    public static void Main(string[] args)
    {
        // Assign the Map here
        string path = "map1.txt";

        MapDetails mapData = ReadMap(path);

        // Displaying The Map
        if (mapData != null)
        {
            Console.WriteLine(mapData.IntersectionCount);
            foreach (var i in mapData.Intersections)
            {
                Console.WriteLine($"{i.Id} {i.X} {i.Y}");
            }
            Console.WriteLine(mapData.RoadCount);
            foreach (var i in mapData.Roads)
            {
                Console.WriteLine($"{i.FirstPoint} {i.SecondPoint} {i.Length} {i.Speed}");
            }
        }
        Console.ReadLine();
    }
}
