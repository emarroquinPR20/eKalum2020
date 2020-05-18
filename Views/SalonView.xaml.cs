using System.Windows;
using kalum2020_v1.ModelViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2020_v1.Views
{
    public partial class SalonView : MetroWindow
    {
        private SalonModelView model;
        public SalonView()
        {
            InitializeComponent();
            model = new SalonModelView(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}