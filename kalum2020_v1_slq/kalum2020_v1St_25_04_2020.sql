use kalum2020_v1
GO
insert into CarrerasTecnicas (NombreCarrera) values('INSTALACIONES ELECTRICAS GENERALES')
insert into CarrerasTecnicas (NombreCarrera) values('CONTROLES DE ELECTRICAS')
insert into Horarios (HorarioInicio,HorarioFinal) values('2020-04-25 13:00:00','2020-04-25 17:30:00')
insert into Horarios (HorarioInicio,HorarioFinal) values('2020-04-25 08:00:00','2020-04-25 12:30:00')
insert into Salones (NombreSalon,Descripcion,Capacidad) values('C23','Salon Electricidad',20)
insert into Salones (NombreSalon,Descripcion,Capacidad) values('C22','Salon Maquinas Electricas',20)
insert into Instructores (Apellidos,Nombres,Direccion,Telefono,Estatus,Foto)
values('Tumax','Edwin','Guatemala,Guatemala','33124365','Alta','Default.jpg')
insert into Instructores (Apellidos,Nombres,Direccion,Telefono,Estatus,Foto)
values('Cabrera','Edgar','Guatemala,Guatemala','33124365','Alta','Default.jpg')
insert into Clases (Descripcion,Ciclo,SalonId,HorarioId,InstructorId,CarreraTecnicaId,CupoMaximo,CupoMinimo,CantidadAsignaciones)
values('Carrera de Instalaciones Electricas',2020,1,1,1,1,20,10,0)
insert into Clases (Descripcion,Ciclo,SalonId,HorarioId,InstructorId,CarreraTecnicaId,CupoMaximo,CupoMinimo,CantidadAsignaciones)
values('Carrera de Instalaciones Electricas',2020,2,2,2,2,30,10,0)
GO
select * from Clases
GO
create view vw_ListaClases as
Select	C.Descripcion,
		C.ciclo,
		S.NombreSalon as 'Salon',
		dbo.fn_HorarioClase(H.HorarioId) as Horario,
		CT.NombreCarrera as 'Carrera',
		I.Nombres + ' ' + I.Apellidos as 'Instructor',
		C.CupoMaximo as 'Cupo Maximo',
		C.CupoMinimo as 'Cupo Minimo'
from Clases C
inner join Salones S  on C.SalonId = S.SalonId
inner join Horarios H on C.HorarioId = H.HorarioId
inner join CarrerasTecnicas CT on C.CarreraTecnicaId = CT.CarreraTecnicaId
inner join Instructores I on C.InstructorId = I.InstructorId
GO
select * from dbo.vw_ListaClases
GO
Select * from Horarios
GO
create function fn_HorarioClase(@HorarioId int)
returns nvarchar(32)
as
begin
	declare @Horario nvarchar(32)
	set @Horario = (select convert(nvarchar,H.HorarioInicio,108) + '  -  ' +  convert(nvarchar,H.HorarioFinal,108)
	from Horarios H
	where @HorarioId = H.HorarioId)
	if	@Horario = null
		set @Horario = 'NO_DEFINIDO';
	return @Horario
end
GO
select dbo.fn_HorarioClase(1) 
GO
create trigger tr_ActualizarAsignaciones on AsignacionAlumno after insert 
as
Begin
	declare @Asignaciones int 
	declare @ClaseId int
	set @ClaseId = (Select ClaseId from inserted)
	set @Asignaciones = (select CantidadAsignaciones from Clases C where C.ClaseId = @ClaseId)
	set @Asignaciones = @Asignaciones + 1
	update Clases 
	set CantidadAsignaciones = @Asignaciones
	where ClaseId = @ClaseId
End
GO
Insert into Religiones (Descripcion) values ('Catolico')
Insert into Religiones (Descripcion) values ('Evangelico')
GO
insert into Alumnos values('2020001','Juan Alberto','Perez Lopez','2000-01-15',1)
insert into Alumnos values('2020001','Francisco','Morales Salvajan','2000-12-01',2)
GO
insert into AsignacionAlumno (AlumnoId,ClaseId,FechaAsignacion,Observaciones) values(2,1,GETDATE(),'')
insert into AsignacionAlumno (AlumnoId,ClaseId,FechaAsignacion,Observaciones) values(3,2,GETDATE(),'')
insert into AsignacionAlumno (AlumnoId,ClaseId,FechaAsignacion,Observaciones) values(3,1,GETDATE(),'')
select * from AsignacionAlumno
select * from Alumnos
select * from vw_ListaClases
select * from Clases