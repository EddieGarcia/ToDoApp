﻿using System.Collections.Generic;
using System.Linq;
using ToDoApp.model;
using NHibernate;

namespace ToDoApp.service
{
    internal class TodoService
    {
        public IEnumerable<Todo> getAllTodos(ISession session)
        {
            return session.Query<Todo>().ToList();
        }

        public void deleteTodo(ISession session, Todo todo)
        {
            using (session.BeginTransaction())
            {
                session.Delete(todo);
                session.GetCurrentTransaction().Commit();
            }
        }

        public void createTodo(ISession session, Todo todo)
        {
            using (session.BeginTransaction())
            {
                session.Save(todo);
                session.GetCurrentTransaction().Commit();
            }
        }
    }
}
