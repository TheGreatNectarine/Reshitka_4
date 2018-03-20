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
            DataContext = new AllUsersViewModel(UpdateGrid, GetSelectedIndex);
        }

        public delegate object GetSelectedIndexDelegate();

        public delegate void UpdateGridDelegate(List<User> users);

        private object GetSelectedIndex()
        {
            return UsersGrid.SelectedItem;
        }

        private void UpdateGrid(List<User> users)
        {
            UsersGrid.ItemsSource = null;
            UsersGrid.ItemsSource = users;
        }
    }
}
