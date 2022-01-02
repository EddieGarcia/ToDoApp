using System.Collections.Generic;

namespace ToDoApp.model
{
    public class Person
    {
        public virtual int ID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual IList<Todo> AssignedTodos { get; set; }
        public virtual IList<SubTask> AssignedSubtasks { get; set; }
        public virtual string FullName { get { return $"{this.FirstName} {this.LastName}"; } }
    }
}
