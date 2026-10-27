using System;
using System.Collections.Generic;
using System.Text;

namespace to_do_app_c_sharp
{
    public class List
    {
        public List<ThingToDo> ThingsToDo { get; set; }
        public List()
        {
            ThingsToDo = new List<ThingToDo>();
        }
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
    }
}
