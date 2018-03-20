using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Practice4
{
    public class AllUsersViewModel : INotifyPropertyChanged
    {
        private RelayCommand _addNewUserCommand;

        private RelayCommand _deleteSelectedUser;

        private AllUsersWindow.GetSelectedIndexDelegate _getSelectedIndex;

        private AllUsersWindow.UpdateGridDelegate _update;

        public List<User> Users { get; }

        public RelayCommand AddNewUserCommand
        {
            get
            {
                return _addNewUserCommand ?? (_addNewUserCommand = new RelayCommand(AddNewUserImp, o => true));
            }
        }

        public RelayCommand DeleteSelectedUser
        {
            get
            {
                return _deleteSelectedUser ?? (_deleteSelectedUser = new RelayCommand(DeleteSelectedUserImp, o => _getSelectedIndex() != -1));
            }
        }

        private void AddNewUserImp(object o)
        {
            var userCreationWindow = new PersonCreationWindow(AddUser);
            userCreationWindow.Show();
        }

        private void DeleteSelectedUserImp(object o)
        {
            var index = _getSelectedIndex();
            if (index != -1)
            {
                Users.RemoveAt(index);
            }
            _update(Users);
        }

        public AllUsersViewModel(AllUsersWindow.UpdateGridDelegate updateDelegate, AllUsersWindow.GetSelectedIndexDelegate getSelectedIndexDelegate)
        {
            _update = updateDelegate;
            _getSelectedIndex = getSelectedIndexDelegate;
            Users = new List<User>();
            User.Preload(Users);
        }

        public delegate void DelegateAddUser(User u);

        private void AddUser(User u)
        {
            Users.Add(u);
        }
               
        #region Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
