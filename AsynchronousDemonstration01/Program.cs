using System;
using System.Net;

class Program
{
    private static void DouwnloadAsynchronously()
    {
        WebClient client = new WebClient();
        client.DownloadStringCompleted +=
            new DownloadStringCompletedEventHandler(DownloadComplete);
        client.DownloadStringAsync(new Uri("http://www.aspnet.com"));
    }

    private static void DownloadComplete(object sender, DownloadStringCompletedEventArgs e)
    {
        if (e.Error != null)
        {
            Console.WriteLine("Some error has occured.");
            return;
        }
        Console.WriteLine(e.Result);
        Console.WriteLine(new string('*', 30));
        Console.WriteLine("Download Complete.");
    }

    static void Main(string[] args)
    {
        DouwnloadAsynchronously();
        Console.WriteLine("Main thread : Done");
        Console.WriteLine(new string('*', 30));
        Console.ReadLine();
    }
}