using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using kalum2020_v1.DataContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using kalum2020_v1.Model;
using MahApps.Metro.Controls.Dialogs;


namespace kalum2020_v1.ModelViews
{
    public class LoginModelView : INotifyPropertyChanged, ICommand
    {
        private IDialogCoordinator dialogCoordinator;
        private MainModelView _MainModelView;
        public MainModelView MainModelView
        {
            get
            {
                return _MainModelView;
            }
            set
            {
                _MainModelView = value;
            }
        }
        private string _imgSystem = $"{Environment.CurrentDirectory}\\images\\System.png";
        public string imgSystem
        { get { return _imgSystem; } set { _imgSystem = value; NotifyChanged("imgSystem"); } }
        private string _imgEnter = $"{Environment.CurrentDirectory}\\images\\Enter.png";
        public string imgEnter
        { get { return _imgEnter; } set { _imgEnter = value; NotifyChanged("imgEnter"); } }

        private Usuario _Usuario;
        private KalumDbContext _Dbcontext;
        private string _Password;
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                NotifyChanged("Password");
            }
        }
        private string _UserName;
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
                NotifyChanged("UserName");
            }
        }

        private LoginModelView _Instancia;
        public LoginModelView Instancia
        {
            get
            {
                return _Instancia;
            }
            set
            {
                _Instancia = value;
                NotifyChanged("Instancia");
            }
        }
        public LoginModelView(IDialogCoordinator instance, MainModelView mainModelView)
        {           
            this.dialogCoordinator = instance;
            this.MainModelView = mainModelView;
            this.Instancia = this;
            this._Dbcontext = new KalumDbContext();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (parameter is Window)
            {
                Password =
                ((PasswordBox)((Window)parameter).FindName("txtPassword")).Password;
                var UserNameParameter = new SqlParameter("@UserName", UserName);
                var PasswordParameter = new SqlParameter("@Password", Password);
                try
                {
                    var Resultado = this._Dbcontext.UsuariosApp
                            .FromSqlRaw("sp_AutenticarUsuario @UserName,@Password",
                        UserNameParameter, PasswordParameter).ToList();
                    foreach (Object objeto in Resultado)
                    {
                        _Usuario = (Usuario)objeto;
                    }
                    if (_Usuario != null)
                    {                        
                        await this.dialogCoordinator.ShowMessageAsync(this,"Kalum 2,020!!!",$"Bienvenido {_Usuario.Apellidos} {_Usuario.Nombres}...");
                        this.MainModelView.EnabledLogin = false;
                        this.MainModelView.EnableOption = true;
                        ((Window)parameter).Close();
                    }
                    else if (_Usuario == null)
                    {                        
                        await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!","El usuario no existe");
                    }
                }
                catch (Exception e)
                {                    
                    await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!",e.Message);
                }
            }

        }
        public void NotifyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

    }
}