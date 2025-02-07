﻿using System;
using System.IO;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        WebRequest request = WebRequest.Create("http://www.contoso.com/default.html");
        
        request.Credentials = CredentialCache.DefaultCredentials;

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        
        Console.WriteLine("Status: " + response.StatusCode);
        Console.WriteLine(new string('*', 50));

        Stream datastream = response.GetResponseStream();

        StreamReader reader = new StreamReader(datastream);

        string responseFromServer = reader.ReadToEnd();

        Console.WriteLine(responseFromServer);
        Console.WriteLine(new string('*', 50));

        reader.Close();
        datastream.Close();
        response.Close();
    }
}