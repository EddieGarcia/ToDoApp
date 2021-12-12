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

            // initialize grid
            RefreshDataGrid();
        }

        public void RefreshDataGrid()
        {
            System.Diagnostics.Debug.WriteLine("Loading data and refreshing grid");
            todoDataGrid.ItemsSource = todoService.getAllTodos(session);
        }

        public void Save_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Persistintg new item");
            TodoPopup popup = new TodoPopup(session, todoService, this, null);
            popup.Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Updating item");
            Todo toUpdate = (Todo) todoDataGrid.SelectedItem;
            if (toUpdate == null)
            {
                MessageBox.Show("Please select row to update todo");
            }
            else
            {
                TodoPopup popup = new TodoPopup(session, todoService, this, toUpdate);
                popup.Show();
            }
        }

        public void Delete_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Deleting item");
            Todo toDelete = (Todo) todoDataGrid.SelectedItem;
            if (toDelete == null)
            {
                MessageBox.Show("Please select row to delete");
            }
            else
            {
                todoService.deleteTodo(session, toDelete);
                RefreshDataGrid();
                MessageBox.Show($"Todo {toDelete.ID} deleted");
            }
        }
    }
}
