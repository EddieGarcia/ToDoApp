using FluentNHibernate.Mapping;

namespace ToDoApp.model
{
    // use fluent mapping over xml mapping https://www.tutorialspoint.com/nhibernate/nhibernate_fluent_hibernate.htm

    public class TodoMapping : ClassMap<Todo>
    {
        public TodoMapping()
        {
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Text);
            HasManyToMany(x => x.Persons).Cascade.None().Table("TodoPerson");
            HasMany(x => x.SubTasks).Inverse().Cascade.All();
            Table("Todo");
        }
    }
}
