using OpenClosedPrinciple.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenClosedPrinciple.Utilities
{
    internal class Utilities
    {
        static string ReadFile(string filename)
        {
            return File.ReadAllText(filename);
        }

        internal static List<Book> ReadData()
        {
            var cadJSON = ReadFile("D:\\Project\\VisualStudioProjects\\PRN222\\Demo\\PRN222Demo\\OpenClosedPrinciple\\Data\\BookStore01.json");
            return JsonConvert.DeserializeObject<List<Book>>(cadJSON);
        }

        internal static List<Book> ReadDataExtra()
        {
            List<Book> books = ReadData();
            var cadJSON = ReadFile("D:\\Project\\VisualStudioProjects\\PRN222\\Demo\\PRN222Demo\\OpenClosedPrinciple\\Data\\BookStore02.json");
            books.AddRange(JsonConvert.DeserializeObject<List<Book>>(cadJSON));
            return books;
        }
    }
}
