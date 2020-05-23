using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using kalum2020_v1.Views;
using Microsoft.EntityFrameworkCore;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2020_v1.ModelViews
{
    public class MainModelView : INotifyPropertyChanged, ICommand
    {     
        private bool _EnabledLogin;
        public bool EnabledLogin
        {
            get
            {
                return _EnabledLogin;                
            }
            set
            {
                _EnabledLogin = value;
                NotifyChanged("EnabledLogin");
            }
        }
        private bool _EnabledOption;
        public bool EnableOption
        {
            get
            {
                return _EnabledOption;
            }
            set
            {
                _EnabledOption = value;
                NotifyChanged("EnableOption");
            }
        }

        private string _imgFondo = $"{Environment.CurrentDirectory}\\images\\Fondo.jpg";
        public string imgFondo
        { get { return _imgFondo; } set { _imgFondo = value; NotifyChanged("imgFondo"); } }
        private string _imgSystem = $"{Environment.CurrentDirectory}\\images\\System.png";
        public string imgSystem
        { get { return _imgSystem; } set { _imgSystem = value; NotifyChanged("imgSystem"); } }
        private string _imgLogin = $"{Environment.CurrentDirectory}\\images\\Login.png";
        public string imgLogin
        { get { return _imgLogin; } set { _imgLogin = value; NotifyChanged("imgLogin"); } }
        private string _imgSalir = $"{Environment.CurrentDirectory}\\images\\Salir.png";
        public string imgSalir
        { get { return _imgSalir; } set { _imgSalir = value; NotifyChanged("imgSalir"); } }
        private string _imgAlumnos = $"{Environment.CurrentDirectory}\\images\\Alumnos.png";
        public string imgAlumnos
        { get { return _imgAlumnos; } set { _imgAlumnos = value; NotifyChanged("imgAlumnos"); } }
        private string _imgCarrerasTecnicas = $"{Environment.CurrentDirectory}\\images\\CarrerasTecnicas.png";
        public string imgCarrerasTecnicas
        { get { return _imgCarrerasTecnicas; } set { _imgCarrerasTecnicas = value; NotifyChanged("imgCarrerasTecnicas"); } }
        private string _imgHorarios = $"{Environment.CurrentDirectory}\\images\\Horarios.png";
        public string imgHorarios
        { get { return _imgHorarios; } set { _imgHorarios = value; NotifyChanged("imgHorarios"); } }
        private string _imgInstructores = $"{Environment.CurrentDirectory}\\images\\Instructores.png";
        public string imgInstructores
        { get { return _imgInstructores; } set { _imgInstructores = value; NotifyChanged("imgInstructores"); } }
        private string _imgReligiones = $"{Environment.CurrentDirectory}\\images\\Religiones.png";
        public string imgReligiones
        { get { return _imgReligiones; } set { _imgReligiones = value; NotifyChanged("imgAlumno"); } }
        private string _imgSalones = $"{Environment.CurrentDirectory}\\images\\Salones.png";
        public string imgSalones
        { get { return _imgSalones; } set { _imgSalones = value; NotifyChanged("imgSalones"); } }
        private MainModelView _Instancia;
        public MainModelView Instancia
        {
            get
            {
                return this._Instancia;
            }
            set
            {
                this._Instancia =value;
                NotifyChanged("Instancia");
            }
        }
        public MainModelView()
        {
            EnableOptions(false);
            this.Instancia = this;            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.Equals("Alumnos"))    
            {
                new AlumnoView().ShowDialog();
            }
            else if(parameter.Equals("CarrerasTenicas"))
            {
                new CarreraTecnicaView().ShowDialog();
            }
            else if(parameter.Equals("Horarios"))
            {
                new HorarioView().ShowDialog();
            }
            else if(parameter.Equals("Instructores"))
            {
                new InstructorView().ShowDialog();
            }
            else if(parameter.Equals("Religiones"))
            {
                new ReligionView().ShowDialog();                
            }
            else if(parameter.Equals("Salones"))
            {
                new SalonView().ShowDialog();
            }
            else if(parameter.Equals("Login"))
            {
                LoginView _LoginView = new LoginView(this);
                _LoginView.ShowDialog();
            }
            else if(parameter.Equals("Salir"))
            {
                Application.Current.Shutdown();
            }
        }
        public void NotifyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public void EnableOptions(bool enabled)
        {
            _EnabledLogin =  !enabled;
            _EnabledOption = enabled;
        }
    }
}