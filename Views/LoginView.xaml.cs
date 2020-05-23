using System.Windows;
using kalum2020_v1.Model;
using kalum2020_v1.ModelViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2020_v1.Views
{    
    public partial class LoginView : MetroWindow
    {
        private LoginModelView _modelView;
        public LoginView(MainModelView mainModelView)
        {
            InitializeComponent();
            _modelView = new LoginModelView(mainModelView);
            this.DataContext = _modelView;        
        }
    }
}