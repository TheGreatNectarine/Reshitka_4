using System;
using System.Windows;


namespace Practice4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PersonCreationWindow : Window
    {

        public PersonCreationWindow(AllUsersViewModel.DelegateAddUser delegateAddPerson)
        {
           InitializeComponent();
           DataContext = new SignInViewModel(CloseWindow, delegateAddPerson);
        }

        public delegate void DelegateCloseWindow();

        private void CloseWindow()
        {
            this.Close();
        }

        private void ShowResultView()
        {
            //var user = ValidUser();
            //if (user == null)
            //{
            //    MessageBox.Show("Incorrect person was created. Try again");
            //    return;
            //}
            //var bday = user.IsBirthday ? "Happy Birthday" : "";
            //MessageBox.Show(
            //    $"Name: {user.FirstName}\n" +
            //    $"Surname: {user.LastName}\n" +
            //    $"Email: {user.Email}\n" +
            //    $"Date of birth: {user.DateOfBirth}\n" +
            //    $"Adult: {user.IsAdult}\n" +
            //    $"Sign: {user.SunSign}\n" +
            //    $"Chinese Sign: {user.ChineseSign}\n" +
            //    $"{bday}"
            //);
        }

        private User ValidUser()
        {
            var data = DataContext as SignInViewModel;
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
