using System.Windows;
using kalum2020_v1.ModelViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2020_v1.Views
{
    public partial class CarreraTecnicaView : MetroWindow
    {
        private CarreraTecnicaModelView model;
        public CarreraTecnicaView()
        {
            InitializeComponent();
            model = new CarreraTecnicaModelView(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}