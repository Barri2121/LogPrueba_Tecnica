create database BD_Acceso

use BD_Acceso 

create table Usuario(
  IdUsuario int primary key identity(1,1),
  Nombre varchar(100),
  Correo varchar(100),
  clave varchar(500),
  Fecha_Nacimiento DATE
 )

create proc Registro_Usuarios(
@Nombre varchar(100),
@Correo varchar(100),
@clave varchar(100),
@Fecha_Nacimiento DATE,
@registrado bit output,
@Mensaje varchar(100) output
)
as 
begin
	if (not exists(select * from Usuario where Correo= @Correo))
	begin
		insert into Usuario(Nombre,Correo,clave,Fecha_Nacimiento) values(@Nombre,@Correo,@clave,@Fecha_Nacimiento)
		set @registrado=1
		set @Mensaje = 'Registrado Correctamente'
	end
	else
	begin
		set @registrado=0
		set @Mensaje = 'Correo ya existente'
	end
end

Create proc Validacion_Usuario(
@Correo varchar(100),
@Clave varchar(100)
)
as
begin
	if(exists(select * from Usuario where Correo=@Correo and clave=@Clave))
		select IdUsuario from Usuario where Correo=@Correo and clave=@Clave
	else
		select '0'
end
CREATE PROCEDURE [dbo].[ObtenerDato1](
    @Correo varchar(100),
    @Clave Varchar(100) OUTPUT
	)
AS
BEGIN

    -- Obtener el dato del campo
    SELECT @Clave = clave 
    FROM Usuario
    WHERE Correo = @Correo;

    -- Devolver el resultado
    SELECT @Clave AS clave;
END

--pruebas
declare @registrado bit, @mensaje varchar(100)
execute Registro_Usuarios 'Bryam21','bry@gmail.com','Guate123','1999-05-21', @registrado output, @mensaje output

select @registrado
select @mensaje

select * from Usuario
