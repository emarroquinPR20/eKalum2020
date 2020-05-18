using System.Windows;
using kalum2020_v1.ModelViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2020_v1.Views
{
    public partial class AlumnoView : MetroWindow
    {
        private AlumnoModelView model;
        public AlumnoView()
        {
            
            InitializeComponent();            
            model = new AlumnoModelView(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}