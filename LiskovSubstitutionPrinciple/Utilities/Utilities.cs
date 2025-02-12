using LiskovSubstitutionPrinciple.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LiskovSubstitutionPrinciple.Utilities
{
    internal class Utilities
    {
        static string ReadFile(string filename)
        {
            return File.ReadAllText(filename);
        }

        internal static List<Book> ReadData()
        {
            var cadJSON = ReadFile("D:\\Project\\VisualStudioProjects\\PRN222\\Demo\\PRN222Demo\\LiskovSubstitutionPrinciple\\Data\\BookStore01.json");
            return JsonConvert.DeserializeObject<List<Book>>(cadJSON);
        }

        internal static List<Book> ReadData(string extra)
        {
            List<Book> books = ReadData();
            var filename = "D:\\Project\\VisualStudioProjects\\PRN222\\Demo\\PRN222Demo\\LiskovSubstitutionPrinciple\\Data\\BookStore02.json";
            var cadJSON = ReadFile(filename);
            books.AddRange(JsonConvert.DeserializeObject<List<Book>>(cadJSON));
            if (extra == "topic")
            {
                filename = "D:\\Project\\VisualStudioProjects\\PRN222\\Demo\\PRN222Demo\\LiskovSubstitutionPrinciple\\Data\\BookStore03.json";
                cadJSON = ReadFile(filename);
                books.AddRange(JsonConvert.DeserializeObject<List<TopicBook>>(cadJSON));
            }
            return books;
        }
    }
}
