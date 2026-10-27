using System.Text.Json;

namespace to_do_app_c_sharp
{
    public class TodoList
    {
        public List<ThingToDo> ThingsToDo { get; set; } = new List<ThingToDo>();

        public void AddThingToDo(ThingToDo thingToDo)
        {
            ThingsToDo.Add(thingToDo);
        }

        public void RemoveThingToDo(ThingToDo thingToDo)
        {
            ThingsToDo.Remove(thingToDo);
        }

        public void DisplayThingsToDo()
        {
            foreach (var thing in ThingsToDo)
            {
                Console.WriteLine(thing.ToString());
            }
        }

        public void Save(string path)
        {
            File.WriteAllText(path, JsonSerializer.Serialize(ThingsToDo, new JsonSerializerOptions { WriteIndented = true }));
        }

        public static TodoList Load(string path)
        {
            var list = new TodoList();
            if (!File.Exists(path))
            {
                return list;
            }

            try
            {
                list.ThingsToDo = JsonSerializer.Deserialize<List<ThingToDo>>(File.ReadAllText(path)) ?? new List<ThingToDo>();
            }
            catch (JsonException)
            {
                Console.WriteLine("Could not read saved tasks; starting with an empty list.");
            }
            return list;
        }
    }
}
