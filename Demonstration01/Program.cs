﻿using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void PrintNumber(string message)
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"{message}:{i}");
            Thread.Sleep(1000); // Pause for 1 second
        }
    }

    static void Main()
    {
        Thread.CurrentThread.Name = "Main";

        // Create a task using a lambda expression
        Task task01 = new Task(() => PrintNumber("Task 01"));
        task01.Start();

        // Create a task using Task.Run with a delegate
        Task task02 = Task.Run(delegate {
            PrintNumber("Task 02");
        });

        // Create a task using Action delegate
        Task task03 = new Task(new Action(() =>
        {
            PrintNumber("Task 03");
        }));
        task03.Start();

        // Main thread output
        Console.WriteLine($"Thread '{Thread.CurrentThread.Name}'");

        Console.ReadKey();
    }
}
