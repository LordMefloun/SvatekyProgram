using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Linq;
using test;

namespace Htpprequest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Svatek> Svatky;
            List<string> svatky = new List<string>();
            string UserInput = "";
            Console.WriteLine("Piš jména, až budeš chtít skončit se zápisem napiš \"konec\"");
            while (UserInput != "konec")
            {
                UserInput = Console.ReadLine();
                if (UserInput != "konec") svatky.Add(UserInput);
            }

            SvatekManager sm = new SvatekManager();
            Svatky = new List<Svatek>();
            foreach(var x in svatky ){
                sm.Name = x;
                Svatek s = sm.GetObject();
                if (s != null) Svatky.Add(s);
                else 
                {
                    Svatek s2 = new Svatek();
                    s2.DateTime = DateTime.Now;
                    s2.Name = "Nenalezeno - " + x;
                    Svatky.Add(s2); 
                }

            }
            Svatky = Svatky.OrderBy(x => x.DateTime).ThenBy(x => x.Name).ToList();
            string[] header = { "Jmeno", "Svatek"};
            string[,] rows = new string[Svatky.Count, 2];
            int i = 0;
            foreach(var svatek in Svatky)
            {
                rows[i, 0] = svatek.Name;
                rows[i, 1] = svatek.DateTime.ToString("dd.MM.yyyy");
                i++;
            }
            Table t = new Table(header, rows);
            t.Print();
        }
    }


}
