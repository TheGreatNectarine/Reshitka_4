using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Practice4
{
    public class AllUsersViewModel : INotifyPropertyChanged
    {
        private int _sortOrder = 0;
        private RelayCommand _addNewUserCommand;
        private RelayCommand _deleteSelectedUser;
        private RelayCommand _serialize;
        private RelayCommand _editCommand;
        private RelayCommand _sortCommand;
        private RelayCommand _filterCommand;
        private AllUsersWindow.GetSelectedIndexDelegate _getSelectedUser;
        private AllUsersWindow.UpdateGridDelegate _update;
        private AllUsersWindow.GetSelectedSortItemDelegate _getSelectedSortItem;

        public List<User> DisplayableUsers { get; set; }

        private List<User> InnerUsers { get; set; }

        public string SortProperty
        {
            get
            {
                return _getSelectedSortItem() ?? "Select item";
            }
            set { }
        }

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

        public RelayCommand SortCommand
        {
            get
            {
                return _sortCommand ?? (_sortCommand = new RelayCommand(SortImp, o => _getSelectedSortItem() != null));
            }
        }

        public RelayCommand FilterCommand
        {
            get
            {
                return _filterCommand ?? (_filterCommand = new RelayCommand(FilterImp, o => false));
            }
        }

        private void AddNewUserImp(object o)
        {
            var userCreationWindow = new PersonCreationWindow(AddUser, _update);
            userCreationWindow.Show();
            _update(DisplayableUsers);
        }

        private void DeleteSelectedUserImp(object o)
        {
            var user = _getSelectedUser() as User;

            InnerUsers.Remove(user);
            DisplayableUsers.Remove(user);

            _update(DisplayableUsers);
        }

        private void SerializeImp(object o)
        {
            User.SerializeAll(InnerUsers);
        }

        private void EditImp(object o)
        {
            var toEdit = _getSelectedUser() as User;
            var editWindow = new PersonCreationWindow(delegate (User edited)
            {
                toEdit.CopyUser(edited);
            }, _update, toEdit);
            editWindow.Show();
        }

        private void SortImp(object o)
        {
           DisplayableUsers = SortHelper.Sorted(DisplayableUsers, _sortOrder++%2==0, _getSelectedSortItem());
           _update(DisplayableUsers);
            InnerUsers = DisplayableUsers;
        }

        private void FilterImp(object o)
        {

        }

        public AllUsersViewModel(AllUsersWindow.UpdateGridDelegate updateDelegate, AllUsersWindow.GetSelectedIndexDelegate getSelectedIndexDelegate, AllUsersWindow.GetSelectedSortItemDelegate getSelectedSortItem)
        {
            _update = updateDelegate;
            _getSelectedUser = getSelectedIndexDelegate;
            _getSelectedSortItem = getSelectedSortItem;
            DisplayableUsers = new List<User>();
            User.Preload(DisplayableUsers);
            _update(DisplayableUsers);
        }

        public delegate void DelegateAddUser(User u);

        public delegate void DelegateEditUser(User u);

        private void AddUser(User u)
        {
            DisplayableUsers.Add(u);
            InnerUsers = DisplayableUsers;
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
