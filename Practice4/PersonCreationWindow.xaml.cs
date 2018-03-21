using System;
using System.Windows;


namespace Practice4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PersonCreationWindow : Window
    {

        public PersonCreationWindow( AllUsersViewModel.DelegateAddUser delegateAddPerson, AllUsersWindow.UpdateGridDelegate _update, User u=null)
        {
            InitializeComponent();
            DataContext = new PersonCreationViewModel(CloseWindow, delegateAddPerson, _update, u);
        }

        public delegate void DelegateCloseWindow();

        private void CloseWindow()
        {
            this.Close();
        }

        private User ValidUser()
        {
            var data = DataContext as PersonCreationViewModel;
            User user = null;
            try
            {
                user = new User(data.FirstName, data.LastName, data.Email, data.DateOfBirth);
            } catch (FutureBirthdayException e)
            {
                MessageBox.Show(e.Message);
            } catch (DistantPastBirthdayException e)
            {
                MessageBox.Show(e.Message);
            } catch (InvalidEmailException e)
            {
                MessageBox.Show(e.Message);
            }
            return user;
        }

        private void ShowView(UIElement element)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(element);
        }
    }
}
