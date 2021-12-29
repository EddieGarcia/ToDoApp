using NHibernate;
using System.Windows;
using ToDoApp.model;
using ToDoApp.service;

namespace ToDoApp.view
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        private ISession session;
        private PersonService personService;

        public UserManagement(ISession session, PersonService personService)
        {
            this.session = session;
            this.personService = personService;
            InitializeComponent();
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            string firstName = FirstNameTextBox.Text;
            string lastName = LastNameTextBox.Text;

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Please fill in both values");
            }
            else
            {
                Person person = new Person() { FirstName = firstName, LastName = lastName };
                personService.savePerson(this.session, person);
                this.Close();
            }
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
