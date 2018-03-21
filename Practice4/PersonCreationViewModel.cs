using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Practice4
{
    class PersonCreationViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateOfBirth;
        private RelayCommand _signInCommand;
        private AllUsersWindow.UpdateGridDelegate _update;
        private readonly AllUsersViewModel.DelegateAddUser _proceedAction;
        private readonly PersonCreationWindow.DelegateCloseWindow _closeAction;

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
                return _signInCommand ?? (_signInCommand = new RelayCommand(ProceedImpl,
                           o => !String.IsNullOrWhiteSpace(_firstName) &&
                                !String.IsNullOrWhiteSpace(_lastName) &&
                                !String.IsNullOrWhiteSpace(_email) &&
                                IsDateValid(_dateOfBirth)));

            }
        }

        public PersonCreationViewModel(PersonCreationWindow.DelegateCloseWindow closeWindow, AllUsersViewModel.DelegateAddUser addUser, AllUsersWindow.UpdateGridDelegate _update, User u=null)
        {
            _closeAction = closeWindow;
            _proceedAction = addUser;
            if (u != null)
            {
                FirstName = u.FirstName;
                LastName = u.LastName;
                Email = u.Email;
                DateOfBirth = u.DateOfBirth;
            }
        }

        private bool IsDateValid(DateTime date)
        {
            return date < DateTime.Today && date.Year > 1920;
        }

        private async void ProceedImpl(object o)
        {
            await Task.Run((() =>
            {
                //ЫЫЫЫЫЫ СУУКАААА
            }));
            _proceedAction(new User(_firstName, _lastName, _email, _dateOfBirth));
            _closeAction();
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
