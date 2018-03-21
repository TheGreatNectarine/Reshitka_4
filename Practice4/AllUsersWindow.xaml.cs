using System.Collections.Generic;
using System.Windows;

namespace Practice4
{
    /// <summary>
    /// Interaction logic for AllUsersWindow.xaml
    /// </summary>
    public partial class AllUsersWindow : Window
    {
        public AllUsersWindow()
        {
            InitializeComponent();
            DataContext = new AllUsersViewModel(UpdateGrid, GetSelectedIndex, GetSelectedSortItem);
        }

        public delegate object GetSelectedIndexDelegate();

        public delegate void UpdateGridDelegate(List<User> users);

        public delegate string GetSelectedSortItemDelegate();

        private object GetSelectedIndex()
        {
            return UsersGrid.SelectedItem;
        }

        private string GetSelectedSortItem()
        {
            var name = SortItems.SelectedItem?.ToString();
            var spl = name?.Split(':');
            var res = spl?[1]?.TrimStart();
            return res;
        }

        private void UpdateGrid(List<User> users)
        {
            UsersGrid.ItemsSource = null;
            UsersGrid.ItemsSource = users;
        }     
    }
}
