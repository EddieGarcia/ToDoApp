namespace ToDoApp.model
{
    public class SubTask
    {
        public virtual int ID { get; set; }
        public virtual string Text { get; set; }
        public virtual bool Done { get; set; }  
        public virtual Todo ParentTodo { get; set; }
        public virtual Person AssignedPerson { get; set; }
    }
}
