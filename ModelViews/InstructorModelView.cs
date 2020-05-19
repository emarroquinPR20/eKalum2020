using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using kalum2020_v1.DataContext;
using kalum2020_v1.Model;
using Microsoft.EntityFrameworkCore;
using MahApps.Metro.Controls.Dialogs;

namespace kalum2020_v1.ModelViews
{    
    public class InstructorModelView : INotifyPropertyChanged, ICommand
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
        private ACCION _Accion;
        private Instructor _ElementNotChanged;
        private int _IndexElementNotChange;
        private InstructorModelView _Instancia;
        public InstructorModelView Instancia
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
        private Instructor _ElementoSeleccionado;
        public Instructor ElementoSeleccionado
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
        private ObservableCollection<Instructor> _ListaInstructor;
        public ObservableCollection<Instructor> ListaInstructor
        {
            get
            {
                if (_ListaInstructor == null)
                {
                    _ListaInstructor = new ObservableCollection<Instructor>(DbContext.Instructores.ToList());
                }
                return _ListaInstructor;
            }
            set
            {
                _ListaInstructor = value;
            }
        }

        public InstructorModelView(IDialogCoordinator instance)
        {
            DbContext = new KalumDbContext();
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
                this.ElementoSeleccionado = new Instructor();
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
                            this.DbContext.Instructores.Remove(this.ElementoSeleccionado);
                            this.DbContext.SaveChanges();
                            this.ListaInstructor.Remove(this.ElementoSeleccionado);
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
                            string rsp = validateDataInstructor();
                            if (rsp == "OK")
                            {
                                try
                                {
                                    this.DbContext.Instructores.Add(this.ElementoSeleccionado);
                                    this.DbContext.SaveChanges();
                                    this.ListaInstructor.Add(this.ElementoSeleccionado);
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
                            string rsp = validateDataInstructor();
                            if (rsp == "OK")
                            {

                                try
                                {
                                    this.DbContext.Instructores.Update(this.ElementoSeleccionado);
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
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        public void EnableComponentsChange(bool enabled)
        {
            this.EnabledModify = !enabled;
            this.EnabledSaved = enabled;
        }
        public string validateDataInstructor()
        {
            bool hayError = false;
            string respuesta = "OK";
            // Apellidos
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.Apellidos == null) ||(this.ElementoSeleccionado.Apellidos.Equals("")))
                {
                    respuesta = "ERR001.- Debe ingresar Apellido(s) de Instructor...";
                    hayError = true;
                }
            }
            // Nombres
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.Nombres == null) ||(this.ElementoSeleccionado.Nombres.Equals("")))
                {
                    respuesta = "ERR002.- Debe ingresar Nombres(s) de Instructor...";
                    hayError = true;
                }
            }
            // Direccion
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.Direccion == null) ||(this.ElementoSeleccionado.Direccion.Equals("")))
                {
                    respuesta = "ERR003.- Debe ingresar Direccion de Instructor...";
                    hayError = true;
                }
            }
            // Telefono
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.Telefono == null) ||(this.ElementoSeleccionado.Telefono.Equals("")))
                {
                    respuesta = "ERR004.- Debe ingresar Telefono de Instructor...";
                    hayError = true;
                }
            }
            // Estatus
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.Estatus == null) ||(this.ElementoSeleccionado.Estatus.Equals("")))
                {
                    respuesta = "ERR005.- Debe ingresar Estatus de Instructor...";
                    hayError = true;
                }
            }
            // Estatus Valido
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.Estatus != "ALTA") && (this.ElementoSeleccionado.Estatus != "BAJA") && (this.ElementoSeleccionado.Estatus != "INGRESO"))
                {
                    respuesta = "ERR006.- Estatus del Instructor debe ser una de las siguientes opciones ALTA,BAJA,INGRESO...";
                    hayError = true;
                }
            }
            // Foto
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.Foto == null) ||(this.ElementoSeleccionado.Foto.Equals("")))
                {
                    respuesta = "ERR007.- Debe ingresar Foto de Instructor...";
                    hayError = true;
                }
            }           
            // Comentario
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.Comentario == null) ||(this.ElementoSeleccionado.Comentario.Equals("")))
                {
                    respuesta = "ERR008.- Debe ingresar Comentario para Instructor...";
                    hayError = true;
                }
            }           

           return respuesta;
        }
        public void SaveDataChange()
        {
            _ElementNotChanged = new Instructor();
            if (ElementoSeleccionado != null)
            {
                _IndexElementNotChange = ListaInstructor.IndexOf(ElementoSeleccionado);
                _ElementNotChanged.InstructorId = ElementoSeleccionado.InstructorId;
                _ElementNotChanged.Nombres = ElementoSeleccionado.Nombres;
                _ElementNotChanged.Apellidos = ElementoSeleccionado.Apellidos;
                _ElementNotChanged.Direccion = ElementoSeleccionado.Direccion;
                _ElementNotChanged.Telefono = ElementoSeleccionado.Telefono;
                _ElementNotChanged.Estatus = ElementoSeleccionado.Estatus;
                _ElementNotChanged.Foto = ElementoSeleccionado.Foto;
                _ElementNotChanged.Comentario = ElementoSeleccionado.Comentario;
            }
            else if (ElementoSeleccionado == null)
            {
                _IndexElementNotChange = 0;
                _ElementNotChanged = null;
            }
        }
        public void ResetDataChage()
        {            
            ListaInstructor.RemoveAt(_IndexElementNotChange);
            ListaInstructor.Insert(_IndexElementNotChange, _ElementNotChanged);
            ElementoSeleccionado = ListaInstructor[_IndexElementNotChange];
        }
    }
}