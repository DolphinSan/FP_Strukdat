using System;
using System.Collections.Generic;



namespace StrukturData; 


public class Program
{  
    public Stack <string> taskStack = new Stack<string>();
    // Simple BinarySearchTree class definition to resolve CS0246 error
   
    
    public Stack<string> redoStack = new Stack<string>();
    public Stack<string> undoStack = new Stack<string>();
   

    public void AddTask(string task)
    {
        // Implementation for adding a task
        Console.WriteLine($"Task '{task}' added.");
    } 

    public void PrintListTask() {

    }
    
    // Simple BinarySearchTree class definition
    
    public void RemoveTask(string task)
    {
        // Implementation for removing a task
        Console.WriteLine($"Task '{task}' removed.");
    } 


    public void UndoTask() {

    } 

    public void RedoTask() {

    } 

    public void PrintHistory() {
        // Implementation for printing task history
        Console.WriteLine("Task history printed.");
    } 

    public void ClearHistory() {
        // Implementation for clearing task history
        Console.WriteLine("Task history cleared.");
    } 

    public void InsertTask() { //update task

    } 

    public void FilterTask() { 

    } 



    static void Main(string[] args)
    {
        Console.WriteLine("Hello, User! Welcome to the Daily Task Management" 
                          + " Application. \n"
                          + "This application allows you to manage your daily tasks efficiently.\n"
                          + "You can add, remove, and view tasks, as well as undo and redo actions.\n"
                          + "Let's get started!");
        Program program = new Program();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Remove Task");
            Console.WriteLine("3. Undo Task");
            Console.WriteLine("4. Redo Task");
            Console.WriteLine("5. Print Task History");
            Console.WriteLine("6. Clear Task History");
            Console.WriteLine("7. Insert Task");
            Console.WriteLine("8. Filter Task");
            Console.WriteLine("9. Exit");

            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    Console.Write("Enter task to add: ");
                    string addTask = Console.ReadLine();
                    program.AddTask(addTask);
                    break;
                case 2:
                    Console.Write("Enter task to remove: ");
                    string removeTask = Console.ReadLine();
                    program.RemoveTask(removeTask);
                    break;
                case 3:
                    program.UndoTask();
                    break;
                case 4:
                    program.RedoTask();
                    break;
                case 5:
                    program.PrintHistory();
                    break;
                case 6:
                    program.ClearHistory();
                    break;
                case 7:
                    program.InsertTask();
                    break;
                case 8:
                    program.FilterTask();
                    break;
                case 9:
                    running = false;
                    Console.WriteLine("Exiting the application. Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            } 
        }
        Console.WriteLine("Thank you for using the Daily Task Management Application!");

    }
}
