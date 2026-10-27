namespace to_do_app_c_sharp
{
    internal class Program
    {
        const string SavePath = "tasks.json";

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("--------------------To-Do Application--------------------");
            Console.WriteLine("---------------------------------------------------------");

            TodoList taskList = TodoList.Load(SavePath);
            bool running = true;

            while (running)
            {
                ShowMenu();
                int choice = ReadInt("Enter your choice (1-7): ");

                switch (choice)
                {
                    case 1: AddTask(taskList); break;
                    case 2: EditTask(taskList); break;
                    case 3: SetCompletion(taskList, true); break;
                    case 4: SetCompletion(taskList, false); break;
                    case 5: RemoveTask(taskList); break;
                    case 6: ViewTasks(taskList); break;
                    case 7: running = false; break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 7.");
                        break;
                }

                if (choice >= 1 && choice <= 5)
                {
                    taskList.Save(SavePath);
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1. Add a new task");
            Console.WriteLine("2. Edit a task");
            Console.WriteLine("3. Mark a task as complete");
            Console.WriteLine("4. Mark a task as incomplete");
            Console.WriteLine("5. Remove a task");
            Console.WriteLine("6. View all tasks");
            Console.WriteLine("7. Exit");
            Console.WriteLine();
        }

        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? line = Console.ReadLine();
                if (line == null)
                {
                    Environment.Exit(0);
                }
                if (int.TryParse(line, out int value))
                {
                    return value;
                }
                Console.WriteLine("Please enter a valid number.");
            }
        }

        static string ReadNonEmpty(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (input == null)
                {
                    Environment.Exit(0);
                }
                input = input.Trim();
                if (input.Length > 0)
                {
                    return input;
                }
                Console.WriteLine("This cannot be empty.");
            }
        }

        static DateTime ReadDueDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    Console.WriteLine("Invalid date format. Please use yyyy-mm-dd.");
                }
                else if (date.Date < DateTime.Today)
                {
                    Console.WriteLine("Due date cannot be in the past. Please enter a valid due date.");
                }
                else
                {
                    return date;
                }
            }
        }

        // Returns null if the list is empty, the user went back, or the number was invalid.
        static ThingToDo? PickTask(TodoList list, string action)
        {
            if (list.ThingsToDo.Count == 0)
            {
                Console.WriteLine($"No tasks available to {action}.");
                return null;
            }

            Console.WriteLine($"Select the task to {action}:");
            for (int i = 0; i < list.ThingsToDo.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {list.ThingsToDo[i]}");
            }
            Console.WriteLine("0. Go back");

            int number = ReadInt("Task number: ");
            if (number == 0)
            {
                return null;
            }
            if (number < 0 || number > list.ThingsToDo.Count)
            {
                Console.WriteLine("Invalid task number.");
                return null;
            }
            return list.ThingsToDo[number - 1];
        }

        static void AddTask(TodoList list)
        {
            string title = ReadNonEmpty("Enter the title of the task: ");
            Console.Write("Enter the description of the task: ");
            string description = Console.ReadLine() ?? "";
            DateTime dueDate = ReadDueDate("Enter the due date of the task (yyyy-mm-dd): ");

            var task = new ThingToDo(title, description, dueDate);
            list.AddThingToDo(task);
            Console.WriteLine("Task added successfully!");
            Console.WriteLine(task);
        }

        static void EditTask(TodoList list)
        {
            ThingToDo? task = PickTask(list, "edit");
            if (task == null)
            {
                return;
            }

            Console.WriteLine("What would you like to edit?");
            Console.WriteLine("1. Edit title");
            Console.WriteLine("2. Edit description");
            Console.WriteLine("3. Edit due date");
            Console.WriteLine("4. Cancel");

            switch (ReadInt("Your choice: "))
            {
                case 1:
                    task.UpdateTitle(ReadNonEmpty("Enter the new title: "));
                    Console.WriteLine("Title updated successfully!");
                    break;
                case 2:
                    Console.Write("Enter the new description: ");
                    task.UpdateDescription(Console.ReadLine() ?? "");
                    Console.WriteLine("Description updated successfully!");
                    break;
                case 3:
                    task.ChangeDueDate(ReadDueDate("Enter the new due date (yyyy-mm-dd): "));
                    Console.WriteLine("Due date updated successfully!");
                    break;
                case 4:
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    break;
            }
        }

        static void SetCompletion(TodoList list, bool completed)
        {
            ThingToDo? task = PickTask(list, completed ? "mark as complete" : "mark as incomplete");
            if (task == null)
            {
                return;
            }

            if (completed)
            {
                task.MarkAsCompleted();
                Console.WriteLine("Task marked as complete!");
            }
            else
            {
                task.MarkAsIncomplete();
                Console.WriteLine("Task marked as incomplete!");
            }
        }

        static void RemoveTask(TodoList list)
        {
            ThingToDo? task = PickTask(list, "remove");
            if (task == null)
            {
                return;
            }

            list.RemoveThingToDo(task);
            Console.WriteLine("Task removed successfully!");
        }

        static void ViewTasks(TodoList list)
        {
            if (list.ThingsToDo.Count == 0)
            {
                Console.WriteLine("No tasks available.");
                return;
            }

            Console.WriteLine("All tasks:");
            list.DisplayThingsToDo();
        }
    }
}
