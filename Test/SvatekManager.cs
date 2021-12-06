using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace test {

    public class SvatekManager
    {
        public string Url { get; private set; } = "https://svatky.adresa.info/json?name=";
        public string Name { get; set; }

        public Svatek GetObject()
        {
            char firstLetter = Name[0];
            firstLetter = char.ToUpper(firstLetter);
            Name = firstLetter + Name.Substring(1);

            string html = string.Empty;
            string url = $"{Url}{Name}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new(stream))
            {
                html = reader.ReadToEnd();
            }
            List<Svatek> svatek = JsonConvert.DeserializeObject<List<Svatek>>(html);
            svatek.ForEach(x => x.GetDateTime());
            return svatek.FirstOrDefault();
        }

    }

    public class Svatek
    {
        public string Date;
        public string Name;
        public DateTime DateTime;
        public void GetDateTime()
        {
            DateTime = DateTime.ParseExact(Date, "ddMM", CultureInfo.InvariantCulture);
        }
    }
}
