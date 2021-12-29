using System.Collections.Generic;

namespace ToDoApp.model
{
    public class Todo
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Text { get; set; }
        public virtual IList<Person> Persons { get; set; }
        public virtual IList<SubTask> SubTasks { get; set; }
    }
}
