using System.Windows;
using ToDoApp.model;
using ToDoApp.service;
using NHibernate;
using System.Collections.Generic;
using System.Linq;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for UpdatePopup.xaml
    /// </summary>
    public partial class TodoPopup : Window
    {
        private ISession session;
        private TodoService todoService;
        private PersonService personService;
        private Window parent;

        private Todo? todo;

        public TodoPopup(ISession session, TodoService todoService, PersonService personService, Window parent, Todo? todo)
        {
            this.session = session;
            this.todoService = todoService;
            this.personService = personService;
            this.parent = parent;
            InitializeComponent();
            IEnumerable<Person> allPersons = this.personService.getAllPersons(this.session);
            PersonListBox.ItemsSource = allPersons;
            AssignedPersonComboBox.ItemsSource = allPersons;

            if (todo == null)
            {
                // Create mode
                HeaderLabel.Content = "Create new Todo";
                this.todo = new Todo();
                this.todo.SubTasks = new List<SubTask>();
            }
            else
            {
                // Update mode
                this.todo = todo;
                HeaderLabel.Content = $"Update Todo with ID {this.todo.ID}";
                NameTextBox.Text = this.todo.Name;
                TextTextBox.Text = this.todo.Text;
                // Set up persons list box
                HashSet<int> selectedPersons = this.todo.Persons.Select(i => i.ID).ToHashSet<int>();
                foreach (Person p in allPersons)
                {
                    if (selectedPersons.Contains(p.ID))
                    {
                        PersonListBox.SelectedItems.Add(p);
                    } 
                }
            }
            SubTasksDataGrid.ItemsSource = this.todo.SubTasks;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text;
            string text = TextTextBox.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(text))
            {
                MessageBox.Show("You have to fill in name and text");
                return;
            }

            if (todo != null)
            {
                this.todo.Name = name;
                this.todo.Text = text;
                this.todo.Persons = PersonListBox.SelectedItems.Cast<Person>().ToList();
                // Have to update owning side https://stackoverflow.com/questions/2749689/what-is-the-owning-side-in-an-orm-mapping
                foreach (SubTask task in this.todo.SubTasks)
                {
                    task.ParentTodo = this.todo;
                }
                this.todoService.mergeTodo(this.session, this.todo);

                // Little hack, in more complex window structure pub/sub pattern needed
                // https://stackoverflow.com/questions/18164178/refresh-a-wpf-page-from-a-child-window
                ((MainWindow)parent).RefreshDataGrid();
                this.Close();
            }
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
