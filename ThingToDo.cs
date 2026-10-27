using System.Text.Json.Serialization;

namespace to_do_app_c_sharp
{
    public class ThingToDo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        [JsonConstructor]
        public ThingToDo(string title, string description, DateTime dueDate, bool isCompleted = false)
        {
            Title = title;
            Description = description;
            DueDate = dueDate;
            IsCompleted = isCompleted;
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }

        public void MarkAsIncomplete()
        {
            IsCompleted = false;
        }

        public void ChangeDueDate(DateTime newDueDate)
        {
            DueDate = newDueDate;
        }

        public void UpdateDescription(string newDescription)
        {
            Description = newDescription;
        }

        public void UpdateTitle(string newTitle)
        {
            Title = newTitle;
        }

        public override string ToString()
        {
            return $"{Title} - {Description} (Due: {DueDate.ToShortDateString()}) - Completed: {IsCompleted}";
        }
    }
}
