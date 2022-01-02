using System.Collections.Generic;
using System.Linq;

namespace ToDoApp.model
{
    public class Todo
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Text { get; set; }
        public virtual IList<Person> Persons { get; set; }
        public virtual IList<SubTask> SubTasks { get; set; }
        // computed properties
        public virtual string SubtasksDone { get { return $"{this.SubTasks.Where(st => st.Done).Count()}/{this.SubTasks.Count}"; } }
        public virtual string AssignedManagers { get { return this.Persons.Aggregate("", (acc, person) => $"{acc}, {person.LastName}"); } }
    }
}
