using System.Windows;
using ToDoApp.model;
using ToDoApp.service;
using NHibernate;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for UpdatePopup.xaml
    /// </summary>
    public partial class TodoPopup : Window
    {
        private ISession session;
        private TodoService todoService;
        private Window parent;

        private Todo? todo;

        public TodoPopup(ISession session, TodoService todoService, Window parent, Todo? todo)
        {
            this.session = session;
            this.todoService = todoService;
            this.parent = parent;
            this.todo = todo;
            InitializeComponent();

            if (todo == null)
            {
                // Create mode
                HeaderLabel.Content = "Create new Todo";
            }
            else
            {
                // Update mode
                HeaderLabel.Content = $"Update Todo with ID {todo.ID}";
                NameTextBox.Text = todo.Name;
                TextTextBox.Text = todo.Text;
            }
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            Todo newTodo = new Todo() { Name = NameTextBox.Text, Text = TextTextBox.Text };
            if (todo == null)
            {
                // Create mode
                this.todoService.saveTodo(this.session, newTodo);
            }
            else
            {
                // Update mode
                newTodo.ID = todo.ID;
                this.todoService.mergeTodo(this.session, newTodo);
            }
            // Little hack, in more complex window structure pub/sub pattern needed
            // https://stackoverflow.com/questions/18164178/refresh-a-wpf-page-from-a-child-window
            ((MainWindow) parent).RefreshDataGrid();
            this.Close();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
