using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using kalum2020_v1.DataContext;
using kalum2020_v1.Model;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2020_v1.ModelViews
{        
    enum ACCION
    {
        NINGUNO, NUEVO, MODIFICAR
    }
    public class AlumnoModelView : INotifyPropertyChanged, ICommand
    {
        private string _imgSystem = $"{Environment.CurrentDirectory}\\images\\System.png";
        public string imgSystem
        { get { return _imgSystem; } set { _imgSystem = value; NotifyChanged("imgSystem"); } }
        private string _imgNew = $"{Environment.CurrentDirectory}\\images\\New.png";
        public string imgNew
        { get { return _imgNew; } set { _imgNew = value; NotifyChanged("imgNew"); } }
        private string _imgEdit = $"{Environment.CurrentDirectory}\\images\\Edit.png";
        public string imgEdit
        { get { return _imgEdit; } set { _imgEdit = value; NotifyChanged("imgEdit"); } }
        private string _imgDelete = $"{Environment.CurrentDirectory}\\images\\Delete.png";
        public string imgDelete
        { get { return _imgDelete; } set { _imgDelete = value; NotifyChanged("Delete"); } }
        private string _imgSave = $"{Environment.CurrentDirectory}\\images\\Save.png";
        public string imgSave
        { get { return _imgSave; } set { _imgSave = value; NotifyChanged("Save"); } }
        private string _imgCancel = $"{Environment.CurrentDirectory}\\images\\Cancel.png";
        public string imgCancel
        { get { return _imgCancel; } set { _imgCancel = value; NotifyChanged("Cancel"); } }
        private IDialogCoordinator dialogCoordinator;
        private bool _EnabledModify;
        public bool EnabledModify
        {
            get
            {
                return _EnabledModify;
            }
            set
            {
                _EnabledModify = value;
                NotifyChanged("EnabledModify");
            }
        }
        private bool _EnabledSaved;
        public bool EnabledSaved
        {
            get
            {
                return _EnabledSaved;
            }
            set
            {
                _EnabledSaved = value;
                NotifyChanged("EnabledSaved");
            }
        }
        private bool _EnabledKeys;
        public bool EnabledKeys
        {
            get
            {
                return _EnabledKeys;
            }
            set
            {
                _EnabledKeys = value;
                NotifyChanged("EnabledKeys");
            }
        }
        private ObservableCollection<Religion> _LstReligion;
        public ObservableCollection<Religion> LstReligion
        {
            get
            {
                if (_LstReligion == null)
                {
                    _LstReligion = new ObservableCollection<Religion>(DbContext.Religiones.ToList());
                }
                return _LstReligion;
            }
            set
            {
                _LstReligion = value;
            }
        }
        private ACCION _Accion;
        private Alumno _ElementNotChanged;
        private int _IndexElementNotChange;
        private AlumnoModelView _Instancia;
        public AlumnoModelView Instancia
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
        private Alumno _ElementoSeleccionado;
        public Alumno ElementoSeleccionado
        {
            get
            {
                return _ElementoSeleccionado;
            }
            set
            {
                _ElementoSeleccionado = value;
                NotifyChanged("ElementoSeleccionado");
            }
        }
        private KalumDbContext DbContext;
        private ObservableCollection<Alumno> _ListaAlumno;
        public ObservableCollection<Alumno> ListaAlumno
        {
            get
            {
                if (_ListaAlumno == null)
                {
                    _ListaAlumno = new ObservableCollection<Alumno>(DbContext.Alumnos.ToList());
                }
                return _ListaAlumno;
            }
            set
            {
                _ListaAlumno = value;
            }
        }

        public AlumnoModelView(IDialogCoordinator instance)
        {            
            this.DbContext = new KalumDbContext();
            dialogCoordinator = instance;
            EnableComponentsChange(false);
            this.Instancia = this;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            if (parameter.Equals("Nuevo"))
            {
                SaveDataChange();
                this.ElementoSeleccionado = new Alumno();
                _Accion = ACCION.NUEVO;
                EnableComponentsChange(true);
            }
            else if (parameter.Equals("Modificar"))
            {
                if (this.ElementoSeleccionado != null)
                {
                    SaveDataChange();
                    _Accion = ACCION.MODIFICAR;
                    EnableComponentsChange(true);
                }
                else if (this.ElementoSeleccionado == null)
                {   
                    await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!","Debe Seleccionar un registro...");                    
                }
            }
            else if (parameter.Equals("Eliminar"))
            {
                if (this.ElementoSeleccionado != null)
                {
                    MessageDialogResult respuesta = await this.dialogCoordinator.ShowMessageAsync(this,
                        "Eliminar Alumno",
                        "Esta seguro de eliminar el registro?",
                        MessageDialogStyle.AffirmativeAndNegative);                    
                    if (respuesta == MessageDialogResult.Affirmative)
                    {
                        try
                        {
                            this.DbContext.Alumnos.Remove(this.ElementoSeleccionado);
                            this.DbContext.SaveChanges();
                            this.ListaAlumno.Remove(this.ElementoSeleccionado);
                            await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!","Datos Eliminados");                          
                        }
                        catch (Exception e)
                        {                            
                            await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!",e.Message);
                        }

                    }
                }
                else if (this.ElementoSeleccionado == null)
                {
                    await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!","Debe Seleccionar un registro...");
                }

            }
            else if (parameter.Equals("Guardar"))
            {
                switch (_Accion)
                {
                    case ACCION.NUEVO:
                        {
                            string rsp = validateDataAlumno();
                            if (rsp == "OK")
                            {
                                try
                                {
                                    this.DbContext.Alumnos.Add(this.ElementoSeleccionado);
                                    this.DbContext.SaveChanges();
                                    this.ListaAlumno.Add(this.ElementoSeleccionado);                                 
                                    await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!","Datos Almacenados...");
                                    _Accion = ACCION.NINGUNO;
                                    EnableComponentsChange(false);
                                }
                                catch (Exception e)
                                {
                                   await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!",e.Message);
                                }
                            }
                            else if (rsp != "OK")
                            {
                                await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!",rsp);
                            }
                            break;

                        }
                    case ACCION.MODIFICAR:
                        {
                            string rsp = validateDataAlumno();
                            if (rsp == "OK")
                            {

                                try
                                {
                                    this.DbContext.Alumnos.Update(this.ElementoSeleccionado);
                                    this.DbContext.SaveChanges();                                    
                                    await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!","Datos Actualizados...");
                                    _Accion = ACCION.NINGUNO;
                                    EnableComponentsChange(false);
                                }
                                catch (Exception e)
                                {
                                   await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!",e.Message);
                                }
                            }
                            else if (rsp != "OK")
                            {                                
                                await this.dialogCoordinator.ShowMessageAsync(this,"Atención!!!",rsp);
                            }
                            break;
                        }
                }
            }
            else if (parameter.Equals("Cancelar"))
            {
                ResetDataChage();
                _Accion = ACCION.NINGUNO;
                EnableComponentsChange(false);
            }
        }

        public void NotifyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public void EnableComponentsChange(bool enabled)
        {
            this.EnabledModify = !enabled;
            this.EnabledSaved = enabled;
            if (_Accion == ACCION.NUEVO)
            {
                this.EnabledKeys = this.EnabledModify;
            }
            else if (_Accion == ACCION.MODIFICAR)
            {
                this.EnabledKeys = this.EnabledSaved;
            }
            else if (_Accion == ACCION.NINGUNO)
            {
                this.EnabledKeys = this.EnabledModify;
            }
        }
        public string validateDataAlumno()
        {
            bool hayError = false;
            string respuesta = "OK";
            // Carne con valor 0
            if (hayError == false)
            {
                if (this.ElementoSeleccionado.Carne == 0)
                {
                    respuesta = "ERR001.- El Debe ingresar un número de carné...";
                    hayError = true;
                }
            }
            // Carné Repetido          
            if (hayError == false)
            {
                foreach (Alumno obj in ListaAlumno)
                {
                    if ((obj.AlumnoId != this.ElementoSeleccionado.AlumnoId) && (obj.Carne == this.ElementoSeleccionado.Carne))
                    {
                        respuesta = $"ERR002.- El número de carné ingresado ya esta asignado al Alumno : {obj.Apellidos} {obj.Nombres}...";
                        hayError = true;
                        break;
                    }
                }
            }
            // Numero de Expediente
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.NumeroExpediente == null) || (this.ElementoSeleccionado.NumeroExpediente.Equals("")))
                {
                    respuesta = "ERR003.- Debe ingresar Número de Expediente del Alumno...";
                    hayError = true;

                }
            }
            // Apellidos
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.Apellidos == null) || (this.ElementoSeleccionado.Apellidos.Equals("")))
                {
                    respuesta = "ERR004.- Debe ingresar Apellido(s) del Alumno...";
                    hayError = true;

                }
            }
            // Nombres
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.Nombres == null) || (this.ElementoSeleccionado.Nombres.Equals("")))
                {
                    respuesta = "ERR005.- Debe ingresar Nombre(s) del Alumno...";
                    hayError = true;
                }
            }
            // Fecha de Nacimiento
            if (hayError == false)
            {
                if (this.ElementoSeleccionado.FechaNacimiento == null)
                {
                    respuesta = "ERR006.- Debe ingresar Fecha de Nacimiento del Alumno...";
                    hayError = true;
                }
            }
            // Religion
            if (hayError == false)
            {
                if (this.ElementoSeleccionado.Religion == null)
                {
                    respuesta = "ERR007.- Debe ingresar Religión del Alumno...";
                    hayError = true;
                }
            }
            return respuesta;
        }
        public void SaveDataChange()
        {
            _ElementNotChanged = new Alumno();
            if (ElementoSeleccionado != null)
            {
                _IndexElementNotChange = ListaAlumno.IndexOf(ElementoSeleccionado);
                _ElementNotChanged.AlumnoId = ElementoSeleccionado.AlumnoId;
                _ElementNotChanged.Carne = ElementoSeleccionado.Carne;
                _ElementNotChanged.NumeroExpediente = ElementoSeleccionado.NumeroExpediente;
                _ElementNotChanged.Nombres = ElementoSeleccionado.Nombres;
                _ElementNotChanged.Apellidos = ElementoSeleccionado.Apellidos;
                _ElementNotChanged.FechaNacimiento = ElementoSeleccionado.FechaNacimiento;
                _ElementNotChanged.ReligionId = ElementoSeleccionado.ReligionId;
                _ElementNotChanged.Religion = ElementoSeleccionado.Religion;
            }
            else if (ElementoSeleccionado == null)
            {
                _IndexElementNotChange = 0;
                _ElementNotChanged = null;
            }
        }
        public void ResetDataChage()
        {            
            ListaAlumno.RemoveAt(_IndexElementNotChange);
            ListaAlumno.Insert(_IndexElementNotChange, _ElementNotChanged);
            ElementoSeleccionado = ListaAlumno[_IndexElementNotChange];
        }
    }
}