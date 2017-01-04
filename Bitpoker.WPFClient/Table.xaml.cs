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

namespace Bitpoker.WPFClient
{
    /// <summary>
    /// Interaction logic for Table.xaml
    /// </summary>
    public partial class Table : Window
    {
        private ViewModels.TableViewModel viewModel;

        public Table(BitPoker.Models.Contracts.Table table)
        {
            viewModel = new ViewModels.TableViewModel(table);
            this.DataContext = viewModel;

            InitializeComponent();
        }
    }
}
