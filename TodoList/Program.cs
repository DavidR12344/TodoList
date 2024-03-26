﻿using System.Collections.Generic;
using TodoList.controllers;
using TodoList.interfaces;

namespace TodoList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITodo todoList = new Todo();
            string enviromentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Console.WriteLine("Enter file path to load task or press Enter to continue: ");
            string filePath = Console.ReadLine();
            string loadfilePath = Path.Combine(enviromentPath, filePath);
            todoList.ReadFromFile(loadfilePath);
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine(">> Welcome to ToDoly");
                Console.WriteLine(">> You have X and Y task are done!");
                Console.WriteLine(">> Pick an option: ");
                Console.WriteLine(">> (1) Show Task list by date or project");
                Console.WriteLine(">> (2) Add new Task");
                Console.WriteLine(">> (3) Edit Task (update, mark as done, remove)");
                Console.WriteLine(">> (4) Save and Quit");
                Console.Write("Please enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine(">> Choose an option to sort Task list by date (1) or project (2)");
                        string extraChoice = Console.ReadLine();
                        switch (extraChoice)
                        {
                            case "1":
                                todoList.sortByDate();
                                break;
                            case "2":
                                todoList.sortByProject();
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again");
                                break;
                        }

                        break;
                    case "2":
                        Console.Write("Enter title: ");
                        string taskTitle = Console.ReadLine();
                        Console.Write("Enter the due date (yyyy-MM-dd): ");
                        if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime dueDate))
                        {
                            Console.Write("Is the task complete? (true/false): ");
                            if (bool.TryParse(Console.ReadLine(), out bool isComplete))
                            {
                                Console.Write("Enter project: ");
                                string project = Console.ReadLine();
                                todoList.Add(taskTitle, dueDate, isComplete, project);
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for status. Please enter completed or pending.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. Please enter a valid date.");
                        }
                        break;
                    case "3":
                        Console.WriteLine(">> Choose an option to Task list by update(1), mark as done (2) and remove (3)");
                        string editChoice = Console.ReadLine();
                        switch (editChoice)
                        {
                            case "1":
                                Console.Write("Enter the task number to update: ");
                                if (int.TryParse(Console.ReadLine(), out int updateIndex))
                                {
                                    todoList.Update(updateIndex - 1);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid task number.");
                                }
                                break;
                            case "2":
                                Console.Write("Enter the task number to mark as completed: ");
                                if (int.TryParse(Console.ReadLine(), out int taskIndex))
                                {
                                    todoList.Mark(taskIndex - 1);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid task number.");
                                }
                                break;
                            case "3":
                                Console.Write("Enter the task number to remove: ");
                                if (int.TryParse(Console.ReadLine(), out int removeIndex))
                                {
                                    todoList.Remove(removeIndex - 1);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid task number.");
                                }
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again");
                                break;
                        }
                        break;
                    case "4":
                        Console.Write("Enter the file path to save tasks: ");
                        string saveFilePath = Console.ReadLine();
                        string enviroment = Path.Combine(enviromentPath, saveFilePath);
                        todoList.SaveToFile(enviroment);
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}