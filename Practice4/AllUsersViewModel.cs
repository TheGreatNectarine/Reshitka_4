using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Practice4
{
    public class AllUsersViewModel : INotifyPropertyChanged
    {
        private RelayCommand _addNewUserCommand;

        private RelayCommand _deleteSelectedUser;

        private RelayCommand _serialize;

        private RelayCommand _editCommand;

        private AllUsersWindow.GetSelectedIndexDelegate _getSelectedUser;

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
                return _deleteSelectedUser ?? (_deleteSelectedUser = new RelayCommand(DeleteSelectedUserImp, o => true));
            }
        }

        public RelayCommand Serialize
        {
            get
            {
                return _serialize ?? (_serialize = new RelayCommand(SerializeImp, o => true));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return _editCommand ?? (_editCommand = new RelayCommand(EditImp, o => true));
            }
        }

        private void AddNewUserImp(object o)
        {
            var userCreationWindow = new PersonCreationWindow(AddUser);
            userCreationWindow.Show();
        }

        private void DeleteSelectedUserImp(object o)
        {
            var user = _getSelectedUser() as User;

            Users.Remove(user);

            _update(Users);
        }

        private void SerializeImp(object o)
        {
            User.SerializeAll(Users);
        }

        private void EditImp(object o)
        {
            var toEdit = _getSelectedUser() as User;
            var editWindow = new PersonCreationWindow(delegate (User edited)
            {
                toEdit.CopyUser(edited);
            }, toEdit);
            editWindow.Show();
        }

        public AllUsersViewModel(AllUsersWindow.UpdateGridDelegate updateDelegate, AllUsersWindow.GetSelectedIndexDelegate getSelectedIndexDelegate)
        {
            _update = updateDelegate;
            _getSelectedUser = getSelectedIndexDelegate;
            Users = new List<User>();
            User.Preload(Users);
        }

        public delegate void DelegateAddUser(User u);

        public delegate void DelegateEditUser(User u);

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
