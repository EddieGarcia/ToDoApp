using FluentNHibernate.Mapping;

namespace ToDoApp.model
{
    public class PersonMapping : ClassMap<Person>
    {
        public PersonMapping()
        {
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            HasManyToMany(x => x.AssignedTodos).Cascade.None().Inverse().Table("TodoPerson");
            HasMany(x => x.AssignedSubtasks).Cascade.None().Inverse();
            Table("Person");
        }
    }
}
