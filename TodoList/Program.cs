﻿using System.Collections.Generic;
using TodoList.controllers;
using TodoList.Interfaces;


namespace TodoList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITodo todoList = new Todo();
            string enviromentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
                                todoList.SortByDate();
                                break;
                            case "2":
                                todoList.SortByProject();
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again");
                                break;
                        }

                        break;
                    case "2":
                        while (true)
                        {
                            Console.Write("Enter title: ");
                            string taskTitle = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(taskTitle))
                            {
                                Console.WriteLine("Task title cannot be empty. Please enter a valid task title.");
                            }
                            else
                            {
                                Console.Write("Enter the due date (yyyy-MM-dd): ");
                                if (DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime dueDate))
                                {
                                    Console.Write("Is the task complete? (true/false): ");
                                    if (bool.TryParse(Console.ReadLine(), out bool isComplete))
                                    {
                                        Console.Write("Enter project: ");
                                        string project = Console.ReadLine();
                                        todoList.Add(taskTitle, dueDate, isComplete, project);
                                        break; // Exit the while loop if all inputs are valid
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input for status. Please enter true or false.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format. Please enter a valid date.");
                                }
                            }
                        }
                        break;
                    case "3":
                        Console.WriteLine(">> Choose an option to edit Task: ");
                        Console.WriteLine(">> (1) Update Task");
                        Console.WriteLine(">> (2) Mark Task as Done");
                        Console.WriteLine(">> (3) Remove Task");
                        string editChoice = Console.ReadLine();
                        switch (editChoice)
                        {
                            case "1":
                                while (true)
                                {
                                    Console.Write("Enter the task number to update: ");
                                    if (int.TryParse(Console.ReadLine(), out int updateIndex))
                                    {
                                        todoList.Update(updateIndex - 1);
                                        break; // Exit the inner while loop if input is valid
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please enter a valid task number.");
                                    }
                                }
                                break;

                            case "2":
                                while (true)
                                {
                                    Console.Write("Enter the task number to mark as completed: ");
                                    if (int.TryParse(Console.ReadLine(), out int taskIndex))
                                    {
                                        todoList.Mark(taskIndex - 1);
                                        break; // Exit the inner while loop if input is valid
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please enter a valid task number.");
                                    }
                                }
                                break;

                            case "3":
                                while (true)
                                {
                                    Console.Write("Enter the task number to remove: ");
                                    if (int.TryParse(Console.ReadLine(), out int removeIndex))
                                    {
                                        todoList.Remove(removeIndex - 1);
                                        break; // Exit the inner while loop if input is valid
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input. Please enter a valid task number.");
                                    }
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid choice. Please try again");
                                break;
                        }
                        break;
                    case "4":
                        Console.Write("Save and Quit: ");
                        string saveFilePath = Console.ReadLine();
                        string environment = Path.Combine(enviromentPath, saveFilePath);
                        todoList.SaveToFile(environment);
                        Console.WriteLine("Tasks saved successfully.");
                        Console.WriteLine(">> Do you want to load tasks from a file or quit the application?");
                        Console.WriteLine(">> (1) Load tasks from a file");
                        Console.WriteLine(">> (2) Quit application");
                        string quitOption = Console.ReadLine();

                        switch (quitOption)
                        {
                            case "1":
                                Console.WriteLine(">> Enter file path to load tasks from or press Enter to continue: ");
                                string loadOption = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(loadOption))
                                {
                                    string loadFilePath = Path.Combine(enviromentPath, loadOption);
                                    todoList.ReadFromFile(loadFilePath);
                                }
                                break;
                            case "2":
                                exit = true;
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}