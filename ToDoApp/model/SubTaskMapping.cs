using FluentNHibernate.Mapping;

namespace ToDoApp.model
{
    public class SubTaskMapping : ClassMap<SubTask>
    {
        public SubTaskMapping()
        {
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.Text);
            Map(x => x.Done);
            References(x => x.ParentTodo).Cascade.None();
            Table("SubTask");
        }
    }
}
