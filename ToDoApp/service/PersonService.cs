using NHibernate;
using System.Collections.Generic;
using System.Linq;
using ToDoApp.model;

namespace ToDoApp.service
{
    public class PersonService
    {
        public IEnumerable<Person> getAllPersons(ISession session)
        {
            return session.Query<Person>().ToList();
        }

        public void savePerson(ISession session, Person person)
        {
            using (session.BeginTransaction())
            {
                session.Save(person);
                session.GetCurrentTransaction().Commit();
            }
        }
    }
}
