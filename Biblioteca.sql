create database biblioteca 
go
use biblioteca
go

CREATE TABLE autor(
idAutor int identity(1,1) not null,
nombre varchar(25) not null,
apellido varchar (25) not null,
fechaNacimiento date
constraint PK_Autor primary key clustered (idAutor),
)

CREATE TABLE libro (
idLibro char(5) not null,
idAutor int not null,
titulo varchar(50) not null,
editorial varchar(20) not null, 
fechaPublicacion date not null,
numPaginas int,
numEjemplares int,
estado bit
constraint PK_Libro primary key clustered (idLibro),
constraint FK_Libro_Autor foreign key (idAutor) references autor (idAutor) on update cascade
)

CREATE TABLE usuario (
idUsuario int identity(1,1) not null,
cedula nchar(10) not null,
nombre varchar(20) not null,
apellido varchar (20) not null,
fecNacimiento date not null,
direccion nchar (50),
tipo varchar(10),
NumPrestamosDisponibles int not null,
constraint PK_Usuario primary key clustered (idUsuario),
constraint U_Usuario_Cedula unique nonclustered(cedula),
)

CREATE TABLE prestamoLibro (
idPrestamo int identity(1,1) not null,
idUsuario int, 
idLibro char(5),
fechaPrestamo date not null,
fechaEntrega date not null
constraint PK_PrestamoLibro primary key clustered (idPrestamo),
constraint FK_PrestamoLibro_Libro foreign key (idLibro) references libro (idLibro) on update cascade,
constraint FK_PrestamoLibro_Usuario foreign key (idUsuario) references usuario (idUsuario) on update cascade
)

insert into autor (nombre, apellido, fechaNacimiento) values ('Roger','Pressman','1950-05-04')
insert into autor (nombre, apellido, fechaNacimiento) values ('Gonzalo','Sanchez','1980-01-01')
insert into autor (nombre, apellido, fechaNacimiento) values ('Alberto','Camacho','1990-11-04')
insert into autor (nombre, apellido, fechaNacimiento) values ('Anna','Lopez','1990-12-04')
insert into autor (nombre, apellido, fechaNacimiento) values ('Ian','Sommerville ','1951-02-23')

insert into libro (idLibro, idAutor, titulo, editorial, fechaPublicacion, numPaginas, numEjemplares, estado) values ('L0001',1, 'Ingenieria del Software','The McGraw-Hill','2010-02-01',767,5,1)
insert into libro (idLibro, idAutor, titulo, editorial, fechaPublicacion, numPaginas, numEjemplares, estado) values ('L0002',2, 'Administracion de Empresas','Gonzalo Sanchez','2008-02-01',300,3,1)
insert into libro (idLibro, idAutor, titulo, editorial, fechaPublicacion, numPaginas, numEjemplares, estado) values ('L0003',3, 'Curso Diseño Grafico','Anaya Multimedia','2008-02-01',288,3,1)
insert into libro (idLibro, idAutor, titulo, editorial, fechaPublicacion, numPaginas, numEjemplares, estado) values ('L0004',4, 'Calculo Diferencial','Tapa blanda','2008-09-15',424,10,1)
insert into libro (idLibro, idAutor, titulo, editorial, fechaPublicacion, numPaginas, numEjemplares, estado) values ('L0005',5, 'Ingenieria del Software','Pearson Educacion','2005-01-15',677,2,1)

insert into usuario (cedula, nombre, apellido, fecNacimiento, direccion, tipo, NumPrestamosDisponibles) values ('1715706675','Josselyn', 'Rodriguez','1995-11-07', 'chillogallo', 'estudiante', 5)
insert into usuario (cedula, nombre, apellido, fecNacimiento, direccion, tipo, NumPrestamosDisponibles) values ('1711853434','Maria', 'Diaz','1985-02-07', 'el condado', 'docente', 7)

select * from prestamoLibro

insert into prestamoLibro (idUsuario, idLibro, fechaPrestamo, fechaEntrega) values(1, 'L0001', '2019-06-21', '2019-06-25')

create proc sp_consultarUsuario
@opc int,
@cedula nchar(10) = null,
@nombre varchar(20) = null,
@apellido varchar(20) = null,
@fecNacimiento date = null,
@direccion nchar(50) = null,
@tipo varchar(10) = null,
@NumPrestamosDisponibles int
as
if(@opc = 1)
begin
  select * from usuario
end

if(@opc = 2)
begin
  insert into usuario(cedula, nombre, apellido, fecNacimiento, direccion, tipo, NumPrestamosDisponibles) values (@cedula, @nombre, @apellido, @fecNacimiento, @direccion, @tipo, @NumPrestamosDisponibles)
end

alter proc sp_consultarPrestamo
@opc int,
@idUsuario int = null,
@idLibro char(5) = null,
@fechaPrestamo date = null,
@fechaEntrega date = null
as
if(@opc = 1)
begin
  select l.titulo, pl.fechaEntrega, pl.fechaPrestamo from prestamoLibro pl 
  inner join libro l 
  on pl.idLibro = l.idLibro 
  
  where idUsuario=@idUsuario
end

if(@opc = 2)
begin
  insert into prestamoLibro(idUsuario, idLibro, fechaPrestamo, fechaEntrega) values (@idUsuario,@idLibro,@fechaPrestamo, @fechaEntrega)
  update libro set numEjemplares=numEjemplares-1
  where idLibro = @idLibro
  update usuario set NumPrestamosDisponibles=NumPrestamosDisponibles-1
  where idUsuario = @idUsuario 
end

alter proc sp_consultarUsuarioID
@idUsuario int
as
select * from Usuario where idUsuario=@idUsuario


create proc sp_consultarUsuarioCedula
@cedula nchar(10)
as
select * from Usuario where cedula=@cedula
EXEC sp_consultarUsuarioCedula '1715706675'

alter proc sp_consultarLibroID
@idLibro char(5)
as
select idLibro, autor = nombre+ ' '+ apellido, titulo, numEjemplares from libro 
inner join autor 
on libro.idAutor = autor.idAutor
where idLibro = @idLibro

EXEC sp_consultarLibroID 'L0001'

select * from libro
 
select * from usuario

update usuario set NumPrestamosDisponibles=1 where idUsuario=1
update libro set numEjemplares=2 where idLibro='L0005'

delete  from prestamoLibro where idPrestamo = 4

select * from prestamoLibro