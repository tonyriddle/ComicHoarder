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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ComicHoarder.Common;
using ComicHoarder.Repository;

namespace ComicHoarder
{
    /// <summary>
    /// Interaction logic for PublisherSelector.xaml
    /// </summary>
    public partial class PublisherSelector : UserControl
    {
        public PublisherSelector()
        {
            IRepository repository = new MSSQLDatabase();
            ComicHoarder.ViewModels.PublisherSelectorViewModel Model = new ViewModels.PublisherSelectorViewModel();
            foreach (Publisher publisher in repository.GetPublishers())
            {
                Model.Publishers.Add(publisher);
            }
            InitializeComponent();
        }
    }
}
