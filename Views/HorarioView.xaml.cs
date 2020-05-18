using System.Windows;
using kalum2020_v1.ModelViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2020_v1.Views
{
    public partial class HorarioView : MetroWindow
    {
        private HorarioModelView model;
        public HorarioView()
        {
            InitializeComponent();
            model = new HorarioModelView(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}