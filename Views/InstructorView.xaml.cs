using System.Windows;
using kalum2020_v1.ModelViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
namespace kalum2020_v1.Views
{
    public partial class InstructorView : MetroWindow
    {
        private InstructorModelView model;
        public InstructorView()
        {
            InitializeComponent();
            model = new InstructorModelView(DialogCoordinator.Instance);
            this.DataContext = model;
        }
    }
}