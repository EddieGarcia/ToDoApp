using System.Windows;
using NHibernate;
using NHibernate.Cfg;
using ToDoApp.model;
using ToDoApp.service;

namespace ToDoApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // One Db session for application lifetime
        private ISession session;
        private TodoService todoService;

        public MainWindow()
        {
            InitializeComponent();
            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory sessionFactory = config.BuildSessionFactory();
            session = sessionFactory.OpenSession();
            todoService = new TodoService();

            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            System.Diagnostics.Debug.WriteLine("Loading data and refreshing grid");
            todoDataGrid.ItemsSource = todoService.getAllTodos(session);
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Persistintg new item");
            string newName = newItemName.Text;
            string newText = newItemText.Text;
            if(newName.Length == 0)
            {
                MessageBox.Show("Please fill in name");
            }
            else
            {
                todoService.createTodo(session, new Todo { Name = newName, Text = newText });
                newItemName.Clear();
                newItemText.Clear();
                RefreshDataGrid();
                MessageBox.Show("New TODO saved");
            }
        }

        public void Delete_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Deleting item");
            Todo toDelete = (Todo)todoDataGrid.SelectedItem;
            if (toDelete == null)
            {
                MessageBox.Show("Please select row to delete");
            }
            else
            {
                todoService.deleteTodo(session, toDelete);
                RefreshDataGrid();
                MessageBox.Show("TODO deleted");
            }
        }
    }
}
