using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComicHoarder
{
    /// <summary>
    /// Interaction logic for PrimaryWindow.xaml
    /// </summary>
    public partial class PrimaryWindow : Window
    {
        public PrimaryWindow()
        {
            InitializeComponent();
        }

        private void PublishersButton_Click(object sender, RoutedEventArgs e)
        {
            PublishersControl.Visibility = Visibility.Visible;
            DashboardControl.Visibility = Visibility.Hidden;
            VolumesControl.Visibility = Visibility.Hidden;
            IssuesControl.Visibility = Visibility.Hidden;
            IssuesToCollectControl.Visibility = Visibility.Hidden;
            CollectControl.Visibility = Visibility.Hidden;
        }

        private void VolumesButton_Click(object sender, RoutedEventArgs e)
        {
            VolumesControl.Visibility = Visibility.Visible;
            PublishersControl.Visibility = Visibility.Hidden;
            DashboardControl.Visibility = Visibility.Hidden;
            IssuesControl.Visibility = Visibility.Hidden;
            IssuesToCollectControl.Visibility = Visibility.Hidden;
            CollectControl.Visibility = Visibility.Hidden;
        }

        private void IssuesButton_Click(object sender, RoutedEventArgs e)
        {
            IssuesControl.Visibility = Visibility.Visible;
            VolumesControl.Visibility = Visibility.Hidden;
            PublishersControl.Visibility = Visibility.Hidden;
            DashboardControl.Visibility = Visibility.Hidden;
            IssuesToCollectControl.Visibility = Visibility.Hidden;
            CollectControl.Visibility = Visibility.Hidden;
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            DashboardControl.Visibility = Visibility.Visible;
            VolumesControl.Visibility = Visibility.Hidden;
            PublishersControl.Visibility = Visibility.Hidden;
            IssuesControl.Visibility = Visibility.Hidden;
            IssuesToCollectControl.Visibility = Visibility.Hidden;
            CollectControl.Visibility = Visibility.Hidden;
        }

        private void IssuesToCollectButton_Click(object sender, RoutedEventArgs e)
        {
            IssuesToCollectControl.Visibility = Visibility.Visible;
            VolumesControl.Visibility = Visibility.Hidden;
            PublishersControl.Visibility = Visibility.Hidden;
            DashboardControl.Visibility = Visibility.Hidden;
            IssuesControl.Visibility = Visibility.Hidden;
            CollectControl.Visibility = Visibility.Hidden;
        }

        private void CollectButton_Click(object sender, RoutedEventArgs e)
        {
            CollectControl.Visibility = Visibility.Visible;
            VolumesControl.Visibility = Visibility.Hidden;
            PublishersControl.Visibility = Visibility.Hidden;
            DashboardControl.Visibility = Visibility.Hidden;
            IssuesControl.Visibility = Visibility.Hidden;
            IssuesToCollectControl.Visibility = Visibility.Hidden;
        }
    }
}
