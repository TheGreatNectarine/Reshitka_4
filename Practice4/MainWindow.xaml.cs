using System;
using System.Windows;


namespace Practice02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new SignInViewModel(ShowResultView);
        }

        private void ShowResultView()
        {
            var user = ValidUser();
            if (user == null)
            {
                MessageBox.Show("Incorrect person was created. Try again");
                return;
            }
            var bday = user.IsBirthday ? "Happy Birthday" : "";
            MessageBox.Show(
                $"Name: {user.FirstName}\n" +
                $"Surname: {user.LastName}\n" +
                $"Email: {user.Email}\n" +
                $"Date of birth: {user.DateOfBirth}\n" +
                $"Adult: {user.IsAdult}\n" +
                $"Sign: {user.SunSign}\n" +
                $"Chinese Sign: {user.ChineseSign}\n" +
                $"{bday}"
            );
        }

        private Person ValidUser()
        {
            var data = DataContext as SignInViewModel;
            Person user = null;
            try
            {
                user = new Person(data.FirstName, data.LastName, data.Email, data.DateOfBirth);
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
