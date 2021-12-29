using System.Windows;
using NHibernate;
using NHibernate.Cfg;
using ToDoApp.model;
using ToDoApp.service;
using FluentNHibernate.Cfg;
using System.ComponentModel;
using ToDoApp.view;

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
        private PersonService personService;

        public MainWindow()
        {
            InitializeComponent();
            Configuration config = new Configuration();
            config.Configure();

            ISessionFactory sessionFactory = Fluently.Configure(config)
                .Mappings(m =>
                {
                    // Add nhibernate fluent mappings here
                    m.FluentMappings.AddFromAssemblyOf<TodoMapping>();
                    m.FluentMappings.AddFromAssemblyOf<PersonMapping>();
                    m.FluentMappings.AddFromAssemblyOf<SubTaskMapping>();
                })
                // Uncomment following line to allow DB schema creation from model
                //.ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false))
                .BuildSessionFactory();

            session = sessionFactory.OpenSession();
            todoService = new TodoService();
            personService = new PersonService();
            RefreshDataGrid();
        }

        public void RefreshDataGrid()
        {
            System.Diagnostics.Debug.WriteLine("Loading data and refreshing grid");
            todoDataGrid.ItemsSource = todoService.getAllTodos(session);
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Persistintg new item");
            TodoPopup popup = new TodoPopup(session, todoService, personService, this, null);
            popup.Show();
        }

        private void OnUpdateClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Updating item");
            Todo toUpdate = (Todo)todoDataGrid.SelectedItem;
            if (toUpdate == null)
            {
                MessageBox.Show("Please select row to update todo");
            }
            else
            {
                TodoPopup popup = new TodoPopup(session, todoService, personService, this, toUpdate);
                popup.Show();
            }
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
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
                MessageBox.Show($"Todo {toDelete.ID} deleted");
            }
        }

        private void OnAddUser(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Adding user");
            UserManagement umWindow = new UserManagement(session, personService);
            umWindow.Show();
        }

        private void OnWindowClose(object sender, CancelEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Sayonara!");
            if (session != null)
            {
                session.Flush();
                session.Dispose();
            }
        }

    }
}
