namespace to_do_app_c_sharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("-------------------------To-Do Application-----------------------------");
            Console.WriteLine("-----------------------------------------------------------------------");

            

            List taskList = new List();
            bool running = true;

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("                                                                       ");
            Console.WriteLine("1. Add a new task");
            Console.WriteLine("2. Edit a task");
            Console.WriteLine("3. Mark a task as complete");
            Console.WriteLine("4. Remove a task");
            Console.WriteLine("5. View all tasks");
            Console.WriteLine("6. Exit");
            Console.WriteLine("                                                                       ");

            Console.Write("Enter your choice (1-6): ");
            int choice = int.Parse(Console.ReadLine());

            while (running)
            {
                switch (choice)
                {
                    case 1:
                        // Add a new task
                        Console.Write("Enter the title of the task: ");
                        string newTitle = Console.ReadLine();

                        Console.Write("Enter the description of the task: ");
                        string newDescription = Console.ReadLine();


                        //checking if the due date is in the past and prompting the user to enter a valid due date if it is
                        DateTime newDueDate;
                        while (true)
                        {
                            Console.Write("Enter the due date of the task (yyyy-mm-dd): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out newDueDate))
                            {
                                Console.WriteLine("Invalid date format. Please use yyyy-mm-dd.");
                            }
                            else if (newDueDate.Date < DateTime.Today)
                            {
                                Console.WriteLine("Due date cannot be in the past. Please enter a valid due date.");
                            }
                            else
                            {
                                break;
                            }
                        }
      
                        
                        ThingToDo newTask = new ThingToDo(newTitle, newDescription, newDueDate);
                        taskList.AddThingToDo(newTask);

                        Console.WriteLine("                                                                       ");
                        //writing the task out
                        Console.WriteLine("Task added successfully!");
                        Console.WriteLine(newTask.ToString());
                        Console.WriteLine();
                        ShowMenu();
                        break;


                    case 2:
                        // Edit a task

                        if (taskList.ThingsToDo.Count == 0)
                        {
                            Console.WriteLine("No tasks available to edit.");
                            break;
                        }

                        Console.WriteLine("Select the task to edit:");
                        for (int i = 0; i < taskList.ThingsToDo.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {taskList.ThingsToDo[i]}");
                        }
                        Console.WriteLine("0. Go back");
                        if (!int.TryParse(Console.ReadLine(), out int taskNumber) || taskNumber < 0 || taskNumber > taskList.ThingsToDo.Count)
                        {
                            Console.WriteLine("Invalid task number.");
                            break;
                        }
                        if (taskNumber == 0)
                        {
                            ShowMenu();
                            break;
                        }
                      

                        Console.WriteLine("What would you like to edit?");
                        Console.WriteLine("1. Edit title");
                        Console.WriteLine("2. Edit description");
                        Console.WriteLine("3. Edit due date");
                        Console.WriteLine("4. Cancel");
                        int editChoice = int.Parse(Console.ReadLine());

                        switch(editChoice)
                        {
                            case 1:
                                // Edit title
                               

                                Console.Write("Enter the new title: ");
                                string editedTitle = Console.ReadLine();
                                taskList.ThingsToDo[taskNumber - 1].UpdateTitle(editedTitle);
                                Console.WriteLine("Title updated successfully!");
                                Console.WriteLine();
                                ShowMenu();
                                break;
                            case 2:
                                // Edit description


                                Console.Write("Enter the new description: ");
                                string editedDescription = Console.ReadLine();
                                taskList.ThingsToDo[taskNumber - 1].UpdateDescription(editedDescription);
                                Console.WriteLine("Description updated successfully!");
                                Console.WriteLine();
                                ShowMenu();

                                break;
                            case 3:
                                // Edit due date

                                Console.Write("Enter the new date: ");
                                DateTime editedDueDate = DateTime.Parse(Console.ReadLine());
                                taskList.ThingsToDo[taskNumber - 1].ChangeDueDate(editedDueDate);
                                Console.WriteLine("Due date updated successfully!");
                                Console.WriteLine();
                                ShowMenu();
                                break;

                            case 4:
                                // Cancel
                                ShowMenu();
                                break;
                            default:
                                Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                                break;
                        }

                        break;

                    case 3:
                        // Mark a task as complete
                        if (taskList.ThingsToDo.Count == 0)
                        {
                            Console.WriteLine("No tasks available to mark as complete.");
                            ShowMenu();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Select the task to mark as complete:");
                            for (int i = 0; i < taskList.ThingsToDo.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {taskList.ThingsToDo[i]}");
                            }
                            Console.WriteLine("0. Go back");
                            if (!int.TryParse(Console.ReadLine(), out int completeTaskNumber) || completeTaskNumber < 0 || completeTaskNumber > taskList.ThingsToDo.Count)
                            {
                                Console.WriteLine("Invalid task number.");
                                ShowMenu();
                                break;
                            }
                            if (completeTaskNumber == 0)
                            {
                                ShowMenu();
                                break;
                            }
                            taskList.ThingsToDo[completeTaskNumber - 1].MarkAsCompleted();
                            Console.WriteLine("Task marked as complete!");
                            Console.WriteLine();
                            ShowMenu();
                        }
                        break;


                    case 4:
                        // Remove a task

                        if (taskList.ThingsToDo.Count == 0)
                        {
                            Console.WriteLine("No tasks available to remove.");
                            ShowMenu();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Select the task to remove:");
                            for (int i = 0; i < taskList.ThingsToDo.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {taskList.ThingsToDo[i]}");
                            }
                            Console.WriteLine("0. Go back");
                            if (!int.TryParse(Console.ReadLine(), out int removeTaskNumber) || removeTaskNumber < 0 || removeTaskNumber > taskList.ThingsToDo.Count)
                            {
                                Console.WriteLine("Invalid task number.");
                                ShowMenu();
                                break;
                            }
                            if (removeTaskNumber == 0)
                            {
                                ShowMenu();
                                break;
                            }
                            taskList.RemoveThingToDo(taskList.ThingsToDo[removeTaskNumber - 1]);
                            Console.WriteLine("Task removed successfully!");
                            Console.WriteLine();
                            ShowMenu();
                        }

                        break;
                    case 5:
                        // View all tasks

                        if (taskList.ThingsToDo.Count == 0)
                        {
                            Console.WriteLine("No tasks available.");
                        }
                        else
                        {
                            Console.WriteLine("All tasks:");
                            taskList.DisplayThingsToDo();
                        }
                        break;
                    case 6:
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }
                Console.Write("Enter your choice (1-6): ");
                choice = int.Parse(Console.ReadLine());
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine();
            Console.WriteLine("1. Add a new task");
            Console.WriteLine("2. Edit a task");
            Console.WriteLine("3. Mark a task as complete");
            Console.WriteLine("4. Remove a task");
            Console.WriteLine("5. View all tasks");
            Console.WriteLine("6. Exit");
            Console.WriteLine();
        }
    }
}
