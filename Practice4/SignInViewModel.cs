using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Practice02
{
    class SignInViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateOfBirth;
        private RelayCommand _signInCommand;
        private readonly Action _signInSuccessAction;

        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ProceedCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand(SignUpImpl,
                           o => !String.IsNullOrWhiteSpace(_firstName) &&
                                !String.IsNullOrWhiteSpace(_lastName) &&
                                !String.IsNullOrWhiteSpace(_email) &&
                                IsDateValid(_dateOfBirth)));

            }
        }

        public SignInViewModel(Action signInSuccessAction)
        {
            _signInSuccessAction = signInSuccessAction;
        }

        private bool IsDateValid(DateTime date)
        {
            return date < DateTime.Today && date.Year > 1920;
        }

        private async void SignUpImpl(object o)
        {
            await Task.Run((() =>
            {
                MessageBox.Show("Loading");
                Thread.Sleep(2000);
                MessageBox.Show("Finished");
            }));
            
             _signInSuccessAction.Invoke();            
        }

        #region Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
