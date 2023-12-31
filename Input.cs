﻿namespace Advent_Of_Code_2023
{
    public static class Input
    {
        public static string[] Get(string file)
        {
            var dir = Directory.GetCurrentDirectory();
            var measurements = File.ReadAllLines(dir.Substring(0, dir.IndexOf("bin")) + @"\Input\" + file + ".txt");
            return measurements;
        }

        public static string GetSingle(string file)
        {
            var dir = Directory.GetCurrentDirectory();
            var measurements = File.ReadAllText(dir.Substring(0, dir.IndexOf("bin")) + @"\Input\" + file + ".txt");
            return measurements;
        }
    }
}
