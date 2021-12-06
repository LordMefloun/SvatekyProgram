using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    class Table
    {
        int Width = 70;
        private string[] Columns;
        private string[,] Rows;
        public string[] Header;

        public Table(string[] header, string[,] rows, int width = 70)
        {
            Rows = rows;
            Header = header;
            Width = width;
        }

        public void Print()
        {
            Console.Clear();
            PrintLine();
            PrintRow(true);
            PrintLine();
            for (int k = 0; k < Rows.GetLength(0); k++)
            {
                List<string> row = new List<string>();
                for (int l = 0; l < Rows.GetLength(1); l++)
                {
                    row.Add(Rows[k, l]);
                }
                Columns = row.ToArray();
                PrintRow();
            }
            PrintLine();
        }

        private void PrintRow(bool header = false)
        {
            int width = (Width - Header.Length) / Header.Length;
            string row = "|";
            if (!header)
                foreach (string column in Columns)
                {
                    row += AlignCentre(column, width) + "|";
                }
            if (header)
                foreach (string column in Header)
                {
                    row += AlignCentre(column, width) + "|";
                }

            Console.WriteLine(row);
        }


        private void PrintLine()
        {
            Console.WriteLine(new string('-', Width));
        }


        private string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
        }
    }
}
