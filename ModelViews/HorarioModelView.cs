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
    public class HorarioModelView : INotifyPropertyChanged, ICommand
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
        private Horario _ElementNotChanged;
        private int _IndexElementNotChange;
        private HorarioModelView _Instancia;
        public HorarioModelView Instancia
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
        private Horario _ElementoSeleccionado;
        public Horario ElementoSeleccionado
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
        private ObservableCollection<Horario> _ListaHorario;
        public ObservableCollection<Horario> ListaHorario
        {
            get
            {
                if (_ListaHorario == null)
                {
                    _ListaHorario = new ObservableCollection<Horario>(DbContext.Horarios.ToList());
                }
                return _ListaHorario;
            }
            set
            {
                _ListaHorario = value;
            }
        }

        public HorarioModelView(IDialogCoordinator instance)
        {
            DbContext = new KalumDbContext();
            dialogCoordinator  = instance;
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
                this.ElementoSeleccionado = new Horario();
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
                            this.DbContext.Horarios.Remove(this.ElementoSeleccionado);
                            this.DbContext.SaveChanges();
                            this.ListaHorario.Remove(this.ElementoSeleccionado);
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
                            string rsp = validateDataHorario();
                            if (rsp == "OK")
                            {
                                try
                                {
                                    this.DbContext.Horarios.Add(this.ElementoSeleccionado);
                                    this.DbContext.SaveChanges();
                                    this.ListaHorario.Add(this.ElementoSeleccionado);
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
                            string rsp = validateDataHorario();
                            if (rsp == "OK")
                            {

                                try
                                {
                                    this.DbContext.Horarios.Update(this.ElementoSeleccionado);
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
        public string validateDataHorario()
        {
            bool hayError = false;
            string respuesta = "OK";
            // Carne con valor 0
            if (hayError == false)
            {
            }
           return respuesta;
        }
        public void SaveDataChange()
        {
            _ElementNotChanged = new Horario();
            if (ElementoSeleccionado != null)
            {
                _IndexElementNotChange = ListaHorario.IndexOf(ElementoSeleccionado);
                _ElementNotChanged.HorarioId = ElementoSeleccionado.HorarioId;
                _ElementNotChanged.HorarioInicio = ElementoSeleccionado.HorarioInicio;
                _ElementNotChanged.HorarioFinal = ElementoSeleccionado.HorarioFinal;
            }
            else if (ElementoSeleccionado == null)
            {
                _IndexElementNotChange = 0;
                _ElementNotChanged = null;
            }
        }
        public void ResetDataChage()
        {            
            ListaHorario.RemoveAt(_IndexElementNotChange);
            ListaHorario.Insert(_IndexElementNotChange, _ElementNotChanged);
            ElementoSeleccionado = ListaHorario[_IndexElementNotChange];
        }
    }
}