using System.Windows;
using kalum2020_v1.ModelViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2020_v1.Views
{
    public partial class ReligionView : MetroWindow 
    {
        private ReligionModelView model;
        public ReligionView()
        {
            InitializeComponent();
            model = new ReligionModelView(DialogCoordinator.Instance);
            this.DataContext = model;
        }   
    }
}