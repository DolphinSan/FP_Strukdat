using System;
using System.Collections.Generic;
using System.Linq;

namespace StrukturData
{
    public class Program
    {
        private CustomStack<TaskAction> undoStack = new CustomStack<TaskAction>();
        private CustomStack<TaskAction> redoStack = new CustomStack<TaskAction>();
        
        private BinaryTree<Task> taskTree = new BinaryTree<Task>();
        private List<Task> taskList = new List<Task>();

        public void AddTask(string taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
            {
                Console.WriteLine("Nama Task tidak boleh kosong kayak hatiku");
                return;
            }

            Console.Write("Enter priority (1=High, 2=Medium, 3=Low) [default=2]: ");
            string priorityInput = Console.ReadLine();
            
            int priority = 2;
            if (!string.IsNullOrEmpty(priorityInput) && int.TryParse(priorityInput, out int p) && p >= 1 && p <= 3)
            {
                priority = p;
            }

            Task newTask = new Task(taskName, priority);
            taskList.Add(newTask);
            taskTree.Insert(newTask);
            
            TaskAction action = new TaskAction("ADD", newTask);
            undoStack.Push(action);
            redoStack = new CustomStack<TaskAction>();
            
            Console.WriteLine($"Task '{taskName}' berhasil ditamabh dengan prioritas berskala {GetPriorityString(priority)} .");
        }

        public void RemoveTask(string taskName)
        {
            if (string.IsNullOrWhiteSpace(taskName))
            {
                Console.WriteLine("Nama Task tidak boleh kosong kayak hatiku");
                return;
            }

            Task taskToRemove = taskList.FirstOrDefault(t => 
                t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
            
            if (taskToRemove != null)
            {
                taskList.Remove(taskToRemove);
                taskTree.Delete(taskToRemove);
                
                TaskAction action = new TaskAction("REMOVE", taskToRemove);
                undoStack.Push(action);
                redoStack = new CustomStack<TaskAction>(); 
                
                Console.WriteLine($"Task '{taskName}' sudah di remove.");
            }
            else
            {
                Console.WriteLine($"Task '{taskName}' tidak ditemukan ygy.");
            }
        }

        public void CompleteTask(string taskName)
        {
            Task task = taskList.FirstOrDefault(t => 
                t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
            
            if (task != null)
            {
                if (task.IsCompleted)
                {
                    Console.WriteLine($"Task '{taskName}' sudah selesai (lupakan ae wes kelar ae).");
                    return;
                }
                
                Task oldTask = new Task(task.Name, task.Priority) 
                { 
                    CreatedAt = task.CreatedAt, 
                    IsCompleted = task.IsCompleted 
                };
                
                task.IsCompleted = true;
                
                TaskAction action = new TaskAction("COMPLETE", oldTask);
                undoStack.Push(action);
                redoStack = new CustomStack<TaskAction>(); 
                
                Console.WriteLine($"Task '{taskName}' sudah ditandai selesai lek.");
            }
            else
            {
                Console.WriteLine($"Task '{taskName}' tidak ditemukan ygy.");
            }
        }

        public void UndoTask()
        {
            if (undoStack.IsEmpty())
            {
                Console.WriteLine("Tidak ada action yang bisa di undo.");
                return;
            }

            TaskAction lastAction = undoStack.Pop();
            redoStack.Push(lastAction);

            switch (lastAction.ActionType)
            {
                case "ADD":
                    taskList.Remove(lastAction.Task);
                    taskTree.Delete(lastAction.Task);
                    Console.WriteLine($"Undid: Menambah task '{lastAction.Task.Name}'");
                    break;
                
                case "REMOVE":
                    taskList.Add(lastAction.Task);
                    taskTree.Insert(lastAction.Task);
                    Console.WriteLine($"Undid: Menghapus task '{lastAction.Task.Name}'");
                    break;
                
                case "COMPLETE":
                    Task task = taskList.FirstOrDefault(t => t.Name == lastAction.Task.Name);
                    if (task != null)
                    {
                        task.IsCompleted = false;
                        Console.WriteLine($"Undid: Menamatkan task '{lastAction.Task.Name}'");
                    }
                    break;
            }
        }

        public void RedoTask()
        {
            if (redoStack.IsEmpty())
            {
                Console.WriteLine("Gaada yang bisa di redo.");
                return;
            }

            TaskAction actionToRedo = redoStack.Pop();
            undoStack.Push(actionToRedo);

            switch (actionToRedo.ActionType)
            {
                case "ADD":
                    taskList.Add(actionToRedo.Task);
                    taskTree.Insert(actionToRedo.Task);
                    Console.WriteLine($"Redid: Menambah task '{actionToRedo.Task.Name}'");
                    break;
                
                case "REMOVE":
                    taskList.Remove(actionToRedo.Task);
                    taskTree.Delete(actionToRedo.Task);
                    Console.WriteLine($"Redid: Menghapus task '{actionToRedo.Task.Name}'");
                    break;
                
                case "COMPLETE":
                    Task task = taskList.FirstOrDefault(t => t.Name == actionToRedo.Task.Name);
                    if (task != null)
                    {
                        task.IsCompleted = true;
                        Console.WriteLine($"Redid: Menamatkan task '{actionToRedo.Task.Name}'");
                    }
                    break;
            }
        }

        public void PrintListTask()
        {
            Console.WriteLine("\n=== Current Tasks ===");
            if (taskList.Count == 0)
            {
                Console.WriteLine("Tidak ada task (free njir).");
                return;
            }

            Console.WriteLine("Tasks di sort dengan prioritas dan waktu pembuatan:");
            var sortedTasks = taskTree.GetInorderList();
            
            for (int i = 0; i < sortedTasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {sortedTasks[i]}");
            }
            
            Console.WriteLine($"\nTotal tasks: {taskList.Count}");
            Console.WriteLine($"Tamat: {taskList.Count(t => t.IsCompleted)}");
            Console.WriteLine($"Pending: {taskList.Count(t => !t.IsCompleted)}");
        }

        public void PrintHistory()
        {
            Console.WriteLine("\n=== Task History ===");
            
            if (undoStack.IsEmpty())
            {
                Console.WriteLine("History nya kosong kayak browser ku.");
                return;
            }

            var actions = undoStack.GetItems();
            Console.WriteLine("Aktivitas terakhi (yang paling baru duluan):");
            
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                Console.WriteLine($"{actions.Count - i}. {actions[i]}");
            }
        }

        public void ClearHistory()
        {
            undoStack = new CustomStack<TaskAction>();
            redoStack = new CustomStack<TaskAction>();
            Console.WriteLine("Task history dihapus.");
        }

        public void InsertTask()
        {
            Console.Write("Nama task yang mau diupdate: ");
            string taskName = Console.ReadLine();
            
            Task task = taskList.FirstOrDefault(t => 
                t.Name.Equals(taskName, StringComparison.OrdinalIgnoreCase));
            
            if (task == null)
            {
                Console.WriteLine($"Task '{taskName}' tidak dtemukan.");
                return;
            }

            Console.WriteLine($"Task saat ini: {task}");
            Console.WriteLine("Apa yang mau dirubah?");
            Console.WriteLine("1. Nama task");
            Console.WriteLine("2. Prioritas (1=High, 2=Medium, 3=Low)");
            Console.WriteLine("3. Status (tamat/tidak tamat)");
            
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                Task oldTask = new Task(task.Name, task.Priority) 
                { 
                    CreatedAt = task.CreatedAt, 
                    IsCompleted = task.IsCompleted 
                };

                switch (choice)
                {
                    case 1:
                        Console.Write("Masukan nama task baru: ");
                        string newName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(newName))
                        {
                            task.Name = newName;
                            Console.WriteLine("Nama task berhasil dirubah.");
                        }
                        break;
                    
                    case 2:
                        Console.Write("Masukan prioritas baru (1=High, 2=Medium, 3=Low): ");
                        if (int.TryParse(Console.ReadLine(), out int newPriority) && 
                            newPriority >= 1 && newPriority <= 3)
                        {
                            taskTree.Delete(task);
                            task.Priority = newPriority;
                            taskTree.Insert(task);
                            Console.WriteLine("Prioritas task berhasil dirubah.");
                        }
                        else
                        {
                            Console.WriteLine("Prioritas gak valdi. Harus 1, 2, atau 3.");
                        }
                        break;
                    
                    case 3:
                        task.IsCompleted = !task.IsCompleted;
                        string status = task.IsCompleted ? "Tamat" : "Otw Menamatkan";
                        Console.WriteLine($"Task ditandai sebagai {status}.");
                        break;
                    
                    default:
                        Console.WriteLine("Ra valid.");
                        return;
                }

                TaskAction action = new TaskAction("UPDATE", oldTask);
                undoStack.Push(action);
                redoStack = new CustomStack<TaskAction>();
            }
            else
            {
                Console.WriteLine("Ra valid.");
            }
        }

        public void FilterTask()
        {
            Console.WriteLine("Filter tasks :");
            Console.WriteLine("1. Prioritas");
            Console.WriteLine("2. Status task");
            Console.WriteLine("3. Tanggal pembuatan");
            
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Ra valid.");
                return;
            }

            List<Task> filteredTasks = new List<Task>();

            switch (choice)
            {
                case 1:
                    Console.Write("Masukkan prioritas untuk filter (1=High, 2=Medium, 3=Low): ");
                    if (int.TryParse(Console.ReadLine(), out int priority) && priority >= 1 && priority <= 3)
                    {
                        filteredTasks = taskList.Where(t => t.Priority == priority).ToList();
                        Console.WriteLine($"\n=== Tasks dengan prioritas sebesar {GetPriorityString(priority)}  ===");
                    }
                    else
                    {
                        Console.WriteLine("Prioritas ra valid.");
                        return;
                    }
                    break;
                
                case 2:
                    Console.Write("Tunjukkan task yang udah tamat? (y/n): ");
                    bool showCompleted = Console.ReadLine()?.ToLower().StartsWith("y") == true;
                    filteredTasks = taskList.Where(t => t.IsCompleted == showCompleted).ToList();
                    Console.WriteLine($"\n=== {(showCompleted ? "Completed" : "Pending")} Tasks ===");
                    break;
                
                case 3:
                    Console.Write("Tunjukkan task hari ini (y/n): ");
                    bool todayOnly = Console.ReadLine()?.ToLower().StartsWith("y") == true;
                    if (todayOnly)
                    {
                        filteredTasks = taskList.Where(t => t.CreatedAt.Date == DateTime.Today).ToList();
                        Console.WriteLine("\n=== Today's Tasks ===");
                    }
                    else
                    {
                        filteredTasks = taskList.OrderByDescending(t => t.CreatedAt).ToList();
                        Console.WriteLine("\n=== All Tasks (Sesuai tanggal pembuatan) ===");
                    }
                    break;
                
                default:
                    Console.WriteLine("Ra valid.");
                    return;
            }

            if (filteredTasks.Count == 0)
            {
                Console.WriteLine("Tidak ada task yang sesuai.");
            }
            else
            {
                for (int i = 0; i < filteredTasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {filteredTasks[i]}");
                }
                Console.WriteLine($"\nFound {filteredTasks.Count} task(s).");
            }
        }

        private string GetPriorityString(int priority)
        {
            return priority switch
            {
                1 => "HIGH",
                2 => "MEDIUM",
                3 => "LOW",
                _ => "UNKNOWN"
            };
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            bool running = true;

            Console.WriteLine("=== Daily Task Management Application ===");
            Console.WriteLine("Aplikasi ini menggunakan jawa/indo dikarenakan pembuatnya sudah gila.");

            while (running)
            {
                Console.WriteLine("\n" + new string('=', 40));
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Add Task");
                Console.WriteLine("2. Remove Task");
                Console.WriteLine("3. Undo Task");
                Console.WriteLine("4. Redo Task");
                Console.WriteLine("5. Print Task History");
                Console.WriteLine("6. Clear Task History");
                Console.WriteLine("7. Insert Task (Update)");
                Console.WriteLine("8. Search Task");
                Console.WriteLine("9. Print List Task");
                Console.WriteLine("10. Complete Task");
                Console.WriteLine("0. Exit");
                Console.WriteLine(new string('=', 40));
                Console.Write("Your choice: ");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();

                    switch (choice)
                    {
                        case 1:
                            Console.Write("Masukkan nama task: ");
                            string addTask = Console.ReadLine();
                            program.AddTask(addTask);
                            break;
                        
                        case 2:
                            Console.Write("Masukkan nama task yang mau dihapus: ");
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
                            program.PrintListTask();
                            break;
                        
                        case 10:
                            Console.Write("Masukkan nama task yang sudah ditamatkan: ");
                            string completeTask = Console.ReadLine();
                            program.CompleteTask(completeTask);
                            break;
                        
                        case 0:
                            running = false;
                            Console.WriteLine("Sekian dari pembuat");
                            Console.WriteLine("Sayonara");
                            break;
                        
                        default:
                            Console.WriteLine("Ra valid, lebokno angka 0-10.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input e ra valid og, lebokno angka.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}