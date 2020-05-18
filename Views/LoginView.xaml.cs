using System.Windows;
using kalum2020_v1.ModelViews;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2020_v1.Views
{    
    public partial class LoginView : MetroWindow
    {
        private LoginModelView _model;
        public LoginView()
        {
            InitializeComponent();
            _model = new LoginModelView();
            this.DataContext = _model;        
        }
    }
}