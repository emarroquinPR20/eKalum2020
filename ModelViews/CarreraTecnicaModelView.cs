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
    public class CarreraTecnicaModelView : INotifyPropertyChanged, ICommand
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
        private CarreraTecnica _ElementNotChanged;
        private int _IndexElementNotChange;
        private CarreraTecnicaModelView _Instancia;
        public CarreraTecnicaModelView Instancia
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
        private CarreraTecnica _ElementoSeleccionado;
        public CarreraTecnica ElementoSeleccionado
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
        private ObservableCollection<CarreraTecnica> _ListaCarreraTecnica;
        public ObservableCollection<CarreraTecnica> ListaCarreraTecnica
        {
            get
            {
                if (_ListaCarreraTecnica == null)
                {
                    _ListaCarreraTecnica = new ObservableCollection<CarreraTecnica>(DbContext.CarrerasTecnicas.ToList());
                }
                return _ListaCarreraTecnica;
            }
            set
            {
                _ListaCarreraTecnica = value;
            }
        }

        public CarreraTecnicaModelView(IDialogCoordinator instance)
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
                ElementoSeleccionado = new CarreraTecnica();
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
                            this.DbContext.CarrerasTecnicas.Remove(this.ElementoSeleccionado);
                            this.DbContext.SaveChanges();
                            this.ListaCarreraTecnica.Remove(this.ElementoSeleccionado);
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
                            string rsp = validateDataCarreraTecnica();
                            if (rsp == "OK")
                            {
                                try
                                {
                                    this.DbContext.CarrerasTecnicas.Add(this.ElementoSeleccionado);
                                    this.DbContext.SaveChanges();
                                    this.ListaCarreraTecnica.Add(this.ElementoSeleccionado);
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
                            string rsp = validateDataCarreraTecnica();
                            if (rsp == "OK")
                            {
                                try
                                {
                                    this.DbContext.CarrerasTecnicas.Update(this.ElementoSeleccionado);
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
        public void NotifyChanged(String property)
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
        }
        public string validateDataCarreraTecnica()
        {
            bool hayError = false;
            string respuesta = "OK";
            // Nombre de Carrera Tecnica
            if (hayError == false)
            {
                if ((this.ElementoSeleccionado.NombreCarrera == null) || (this.ElementoSeleccionado.NombreCarrera.Equals("")))
                {
                    respuesta = "ERR001.- Debe ingresar nombre de Carrera Tecnica...";
                    hayError = true;
                }
            }
            return respuesta;
        }
        public void SaveDataChange()
        {
            _ElementNotChanged = new CarreraTecnica();
            if (ElementoSeleccionado != null)
            {
                _IndexElementNotChange = ListaCarreraTecnica.IndexOf(ElementoSeleccionado);
                _ElementNotChanged.CarreraTecnicaId = ElementoSeleccionado.CarreraTecnicaId;
                _ElementNotChanged.NombreCarrera = ElementoSeleccionado.NombreCarrera;
            }
            else if (ElementoSeleccionado == null)
            {
                _IndexElementNotChange = 0;
                _ElementNotChanged = null;
            }
        }
        public void ResetDataChage()
        {            
            ListaCarreraTecnica.RemoveAt(_IndexElementNotChange);
            ListaCarreraTecnica.Insert(_IndexElementNotChange, _ElementNotChanged);
            ElementoSeleccionado = ListaCarreraTecnica[_IndexElementNotChange];
        }
    }
}