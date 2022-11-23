
USE [master]
GO

/****** Object:  Database [ML]    Script Date: 7/11/2022 12:12:14 ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ML1')
BEGIN
CREATE DATABASE [ML1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ML1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ML1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ML1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ML1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
END
GO
ALTER DATABASE [ML1] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ML1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ML1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ML1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ML1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ML1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ML1] SET ARITHABORT OFF 
GO
ALTER DATABASE [ML1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ML1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ML1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ML1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ML1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ML1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ML1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ML1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ML1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ML1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ML1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ML1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ML1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ML1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ML1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ML1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ML1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ML1] SET RECOVERY FULL 
GO
ALTER DATABASE [ML1] SET  MULTI_USER 
GO
ALTER DATABASE [ML1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ML1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ML1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ML1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ML1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ML1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ML1] SET QUERY_STORE = OFF
GO
USE [ML1]
GO
/****** Object:  UserDefinedFunction [dbo].[Disponibilidad]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Disponibilidad]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Disponibilidad](@FechaInicio [datetime], @FechaFinal [datetime], @ID_Servicio [int])
RETURNS [int] WITH INLINE = ON, EXECUTE AS CALLER
AS 
BEGIN
	DECLARE @Stock INT
	SELECT @Stock = Cantidad FROM dbo.Servicio
	WHERE ID_Servicio = 1
	DECLARE @Ocupados INT
	select TOP 1 @Ocupados = Cantidad from Detalle_Factura inner join Evento
	on Evento.ID_Evento = Detalle_Factura.ID_Evento
	WHERE Fecha_Evento BETWEEN @FechaInicio AND @FechaFinal
	AND Fecha_Final BETWEEN @FechaInicio AND @FechaFinal
	AND Fecha_Final IS NOT NULL
	AND ID_Servicio = @ID_Servicio
	ORDER BY Cantidad DESC
RETURN @Stock - @Ocupados
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[Disponible]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Disponible]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Disponible](@ID_Servicio [int], @Fecha [datetime])
RETURNS [int] WITH INLINE = ON, EXECUTE AS CALLER
AS 
BEGIN
Declare @Cantidad int
 select @Cantidad = sum(Cantidad)  from Detalle_Factura D
 inner join Evento E on E.ID_Evento = D.ID_Evento
 where Fecha_Evento = @Fecha and ID_Servicio = @ID_Servicio
 return @Cantidad
END
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetTotal]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTotal]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetTotal](@Cantidad [int], @Precio [float])
RETURNS [varchar](100) WITH INLINE = ON, EXECUTE AS CALLER
AS 
BEGIN
RETURN
  ''El total es: '' + convert(Varchar(20),(@Cantidad * @Precio))
END' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetTotal1]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTotal1]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[GetTotal1](@Cantidad [int], @Precio [float])
RETURNS [varchar](40) WITH INLINE = ON, EXECUTE AS CALLER
AS 
BEGIN
RETURN
  ''El total es:'' + @Cantidad * @Precio 
END
' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[Pagado]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pagado]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE FUNCTION [dbo].[Pagado](@id [int])
RETURNS [int] WITH INLINE = ON, EXECUTE AS CALLER
AS 
begin
declare @Pagado int
select @Pagado = sum(Monto) from Recibo R inner join Evento E
on E.ID_Evento = R.ID_Evento inner join Detalle_Factura D
ON D.ID_Evento = E.ID_Evento inner join Factura F
on F.ID_Factura = D.ID_Factura where E.ID_Evento = @id 
return @Pagado
end' 
END
GO
/****** Object:  Table [dbo].[Proforma]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proforma]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Proforma](
	[ID_Proforma] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NULL,
	[Impuesto] [float] NULL,
	[Total] [float] NULL,
	[Subtotal] [float] NULL,
 CONSTRAINT [PK__Proforma__E981C2F9AB268118] PRIMARY KEY CLUSTERED 
(
	[ID_Proforma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[ProformaV]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ProformaV]'))
EXEC dbo.sp_executesql @statement = N'create   view [dbo].[ProformaV]
as 
select Fecha, Impuesto, Total, ID_Proforma from Proforma 
' 
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cliente]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Cliente](
	[ID_Cliente] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [nvarchar](18) NULL,
	[Nombre] [nvarchar](25) NULL,
	[Apellido] [nvarchar](25) NULL,
	[Telefono] [int] NULL,
 CONSTRAINT [PK__Cliente__E005FBFFB407643E] PRIMARY KEY CLUSTERED 
(
	[ID_Cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Evento]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Evento]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Evento](
	[ID_Evento] [int] IDENTITY(1,1) NOT NULL,
	[ID_Cliente] [int] NULL,
	[ID_Empleado] [int] NULL,
	[Fecha_Evento] [datetime] NULL,
	[Direccion] [varchar](150) NULL,
	[Observaciones] [varchar](200) NULL,
	[Hora_Inicio] [varchar](10) NULL,
	[Hora_Fin] [varchar](10) NULL,
	[Adelanto] [float] NULL,
 CONSTRAINT [PK__Evento__929BD0C1CB1A8CE8] PRIMARY KEY CLUSTERED 
(
	[ID_Evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Factura]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Factura](
	[ID_Factura] [int] IDENTITY(1,1) NOT NULL,
	[Fecha_Factura] [datetime] NULL,
	[Impuesto] [int] NULL,
	[Total] [float] NULL,
	[Descuento] [float] NULL,
	[Subtotal] [float] NULL,
 CONSTRAINT [PK__Factura__E9D586A8CF5254BB] PRIMARY KEY CLUSTERED 
(
	[ID_Factura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Detalle_Factura]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Detalle_Factura](
	[ID_Detalle] [int] IDENTITY(1,1) NOT NULL,
	[ID_Factura] [int] NULL,
	[ID_Evento] [int] NULL,
	[ID_Servicio] [int] NULL,
	[Cantidad] [int] NULL,
	[subtotal] [float] NULL,
 CONSTRAINT [PK__Detalle___B3E0CED3A1FF7A6D] PRIMARY KEY CLUSTERED 
(
	[ID_Detalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[VistaF]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VistaF]'))
EXEC dbo.sp_executesql @statement = N'CREATE   view [dbo].[VistaF]
as
select F.ID_Factura, CONCAT(Apellido, '' '', Nombre) NombreC,
CONVERT(varchar,F.Fecha_Factura,11) Fecha, F.Descuento,  F.Impuesto, F.Total, E.Direccion, 
F.Subtotal, Telefono
from Evento E inner join Detalle_Factura D
on E.ID_Evento = D.ID_Evento inner join Cliente C
on C.ID_Cliente = E.ID_Cliente inner join Factura F
on F.ID_Factura = D.ID_Factura 
group by F.ID_Factura, Nombre, Apellido, F.Fecha_Factura, 
F.Impuesto, F.Total, E.Direccion , F.Descuento, F.Subtotal, Telefono
' 
GO
/****** Object:  Table [dbo].[Recibo]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Recibo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Recibo](
	[ID_Recibo] [int] IDENTITY(1,1) NOT NULL,
	[Monto] [float] NULL,
	[ID_Evento] [int] NULL,
	[Fecha] [datetime] NULL,
	[Banco] [varchar](30) NULL,
	[Cheque] [float] NULL,
	[Efectivo] [float] NULL,
	[Concepto] [varchar](100) NULL,
	[Forma_Pago] [varchar](25) NULL,
 CONSTRAINT [PK__Recibo__1820461A6150CA8B] PRIMARY KEY CLUSTERED 
(
	[ID_Recibo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[ReciboV]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ReciboV]'))
EXEC dbo.sp_executesql @statement = N'create   view [dbo].[ReciboV]
as
select E.ID_Evento, CONCAT(Apellido, '' '', Nombre) Nombre
,CONVERT(varchar,R.Fecha,11) as Fecha, R.Forma_Pago, R.Monto
from Recibo R inner join Evento E
on E.ID_Evento = R.ID_Evento inner join Cliente C
on C.ID_Cliente = E.ID_Cliente 
' 
GO
/****** Object:  View [dbo].[Contrato]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Contrato]'))
EXEC dbo.sp_executesql @statement = N'create   view [dbo].[Contrato]
as
select E.ID_Evento, CONCAT(Apellido, '' '', Nombre) Nombre
,CONVERT(varchar,E.Fecha_Evento,11) as Fecha, E.Direccion
, Adelanto, Hora_Inicio, Hora_Fin
, Observaciones, F.Total, CONVERT(varchar,F.Fecha_Factura,106) FechaF 
from Evento E inner join Cliente C
on C.ID_Cliente = E.ID_Cliente inner join Detalle_Factura D 
on D.ID_Evento = E.ID_Evento inner join Factura F
on F.ID_Factura = D.ID_Factura 
' 
GO
/****** Object:  View [dbo].[Debido]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Debido]'))
EXEC dbo.sp_executesql @statement = N'create   view [dbo].[Debido]
as
select R.ID_Evento, CONCAT(Apellido, '' '', Nombre) Nombre, 
dbo.Pagado(R.ID_Evento) Pagado,
(F.Total - dbo.Pagado(R.ID_Evento)) Debido ,
 F.Total, ''Pendiente'' Estado
from Recibo R inner join Evento E
on E.ID_Evento = R.ID_Evento inner join Detalle_Factura D
on D.ID_Evento = E.ID_Evento inner join Factura F
on F.ID_Factura = D.ID_Factura inner join Cliente
on Cliente.ID_Cliente = E.ID_Cliente
where dbo.Pagado(R.ID_Evento) < F.Total
group by R.ID_Evento, F.Total, Apellido, Nombre
' 
GO
/****** Object:  View [dbo].[ReciboR]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ReciboR]'))
EXEC dbo.sp_executesql @statement = N'create   view [dbo].[ReciboR]
as
select ID_Recibo,CONCAT(Apellido, '' '', Nombre) Nombre
,CONVERT(varchar,R.Fecha,11) as Fecha, R.Forma_Pago, 
R.Monto, Efectivo, Cheque, Banco, Concepto
from Recibo R inner join Evento E
on E.ID_Evento = R.ID_Evento inner join Cliente C
on C.ID_Cliente = E.ID_Cliente ' 
GO
/****** Object:  View [dbo].[Recibos]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Recibos]'))
EXEC dbo.sp_executesql @statement = N'create   view [dbo].[Recibos]
as
select ID_Recibo,CONVERT(varchar,Fecha,11) Fecha, 
CONCAT(Apellido, '' '', Nombre) Nombre, R.Forma_Pago, Monto, R.ID_Evento
from Recibo R inner Join Evento E
on R.ID_Evento = E.ID_Evento inner join Cliente
on Cliente.ID_Cliente = E.ID_Cliente' 
GO
/****** Object:  View [dbo].[FE]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[FE]'))
EXEC dbo.sp_executesql @statement = N'create view [dbo].[FE]
as
select E.ID_Evento, F.ID_Factura from Factura F join Detalle_Factura D
on F.ID_Factura = D.ID_Factura join Evento E
on E.ID_Evento = D.ID_Evento ' 
GO
/****** Object:  Table [dbo].[Cargo]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cargo]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Cargo](
	[ID_Cargo] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Cargo] [nvarchar](25) NULL,
	[Salario] [float] NULL,
 CONSTRAINT [PK__Cargo__8D69B95F8CF0E017] PRIMARY KEY CLUSTERED 
(
	[ID_Cargo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Empleado]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Empleado](
	[ID_Empleado] [int] IDENTITY(1,1) NOT NULL,
	[ID_Cargo] [int] NULL,
	[Cedula_E] [nvarchar](18) NULL,
	[Apellido_E] [nvarchar](25) NULL,
	[Nombre_E] [nvarchar](25) NULL,
	[Telefono] [int] NULL,
	[Direccion] [nvarchar](150) NULL,
 CONSTRAINT [PK__Empleado__B7872C906022271E] PRIMARY KEY CLUSTERED 
(
	[ID_Empleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[EmpleadosV]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[EmpleadosV]'))
EXEC dbo.sp_executesql @statement = N'CREATE view [dbo].[EmpleadosV]
as
select ID_Empleado, Nombre_E, Apellido_E, Cedula_E, 
C.Nombre_Cargo, Telefono, Direccion from Empleado E 
inner join Cargo C on C.ID_Cargo = E.ID_Cargo' 
GO
/****** Object:  View [dbo].[View_FacturaR]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_FacturaR]'))
EXEC dbo.sp_executesql @statement = N'create   view [dbo].[View_FacturaR]
as
select F.ID_Factura, CONCAT(C.Apellido, '' '' , C.Nombre) Nombre, 
convert(varchar,Fecha_Evento,101) Fecha, F.Total, E.Direccion
from Factura F inner Join Detalle_Factura D
on F.ID_Factura = D.ID_Factura inner join Evento E
on E.ID_Evento = D.ID_Evento inner join Cliente C
on C.ID_Cliente = E.ID_Cliente where Fecha_Evento between
CONVERT(datetime, getdate(), 112) and Fecha_Evento  
group by F.ID_Factura, CONCAT(C.Apellido, '' '' , C.Nombre), Fecha_Evento, 
F.Total, F.Impuesto, E.Direccion
' 
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categoria]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Categoria](
	[ID_Categoria] [int] IDENTITY(1,1) NOT NULL,
	[Nombre_Categoria] [nvarchar](30) NULL,
 CONSTRAINT [PK__Categori__02AA07850F3DB77A] PRIMARY KEY CLUSTERED 
(
	[ID_Categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Servicio]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Servicio]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Servicio](
	[ID_Servicio] [int] IDENTITY(1,1) NOT NULL,
	[ID_Categoria] [int] NULL,
	[Descripcion] [nvarchar](80) NULL,
	[Cantidad] [int] NULL,
	[Precio] [float] NULL,
 CONSTRAINT [PK__Servicio__1932F5849D792080] PRIMARY KEY CLUSTERED 
(
	[ID_Servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[VistaAlmacen]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VistaAlmacen]'))
EXEC dbo.sp_executesql @statement = N'
create view [dbo].[VistaAlmacen]
as
select S.ID_Servicio, S.Descripcion, S.Precio, Categoria.Nombre_Categoria, S.Cantidad
from Servicio S inner join Categoria
on Categoria.ID_Categoria = S.ID_Categoria
where S.ID_Categoria != 4
' 
GO
/****** Object:  View [dbo].[VistaTransporte]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VistaTransporte]'))
EXEC dbo.sp_executesql @statement = N'

create view [dbo].[VistaTransporte]
as
select S.ID_Servicio, S.Descripcion, S.Precio, Categoria.Nombre_Categoria, S.Cantidad
from Servicio S inner join Categoria
on Categoria.ID_Categoria = S.ID_Categoria
where S.ID_Categoria = 4
' 
GO
/****** Object:  View [dbo].[Prueba]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[Prueba]'))
EXEC dbo.sp_executesql @statement = N'create view [dbo].[Prueba]
as
select ID_Servicio, Descripcion, Cantidad from Servicio

' 
GO
/****** Object:  View [dbo].[View_Factura]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[View_Factura]'))
EXEC dbo.sp_executesql @statement = N'CREATE view [dbo].[View_Factura]
as
select F.ID_Factura, CONCAT(C.Apellido, '' '' , C.Nombre) Nombre, 
convert(varchar,Fecha_Evento,101) Fecha, F.Total, E.Direccion
from Factura F inner Join Detalle_Factura D
on F.ID_Factura = D.ID_Factura inner join Evento E
on E.ID_Evento = D.ID_Evento inner join Cliente C
on C.ID_Cliente = E.ID_Cliente 
group by F.ID_Factura, CONCAT(C.Apellido, '' '' , C.Nombre), Fecha_Evento, 
F.Total, F.Impuesto, E.Direccion' 
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Usuario]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Usuario](
	[ID_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[ID_Empleado] [int] NULL,
	[Usuario] [nvarchar](15) NULL,
	[Contraseña] [nvarchar](15) NULL,
 CONSTRAINT [PK__Usuario__DE4431C5CC7DF95D] PRIMARY KEY CLUSTERED 
(
	[ID_Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[ViewUsuario]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[ViewUsuario]'))
EXEC dbo.sp_executesql @statement = N'create   view [dbo].[ViewUsuario]
as
select U.ID_Usuario, CONCAT(Apellido_E, '' '', Nombre_E) Nombre, 
Nombre_Cargo, Usuario from Empleado E inner join Usuario U 
on U.ID_Empleado = E.ID_Empleado inner join Cargo C
on C.ID_Cargo = E.ID_Cargo ' 
GO
/****** Object:  Table [dbo].[Detalle_Proforma]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Detalle_Proforma]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Detalle_Proforma](
	[ID_Detalle] [int] IDENTITY(1,1) NOT NULL,
	[ID_Proforma] [int] NULL,
	[ID_Servicio] [int] NULL,
	[Cantidad] [int] NULL,
	[Subtotal] [float] NULL,
 CONSTRAINT [PK__Detalle___B3E0CED37A7C975F] PRIMARY KEY CLUSTERED 
(
	[ID_Detalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Detalle_F__ID_Ev__4F7CD00D]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura]  WITH CHECK ADD  CONSTRAINT [FK__Detalle_F__ID_Ev__4F7CD00D] FOREIGN KEY([ID_Evento])
REFERENCES [dbo].[Evento] ([ID_Evento])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Detalle_F__ID_Ev__4F7CD00D]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura] CHECK CONSTRAINT [FK__Detalle_F__ID_Ev__4F7CD00D]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Detalle_F__ID_Fa__4E88ABD4]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura]  WITH CHECK ADD  CONSTRAINT [FK__Detalle_F__ID_Fa__4E88ABD4] FOREIGN KEY([ID_Factura])
REFERENCES [dbo].[Factura] ([ID_Factura])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Detalle_F__ID_Fa__4E88ABD4]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura] CHECK CONSTRAINT [FK__Detalle_F__ID_Fa__4E88ABD4]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Detalle_F__ID_Se__5070F446]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura]  WITH CHECK ADD  CONSTRAINT [FK__Detalle_F__ID_Se__5070F446] FOREIGN KEY([ID_Servicio])
REFERENCES [dbo].[Servicio] ([ID_Servicio])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Detalle_F__ID_Se__5070F446]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Factura]'))
ALTER TABLE [dbo].[Detalle_Factura] CHECK CONSTRAINT [FK__Detalle_F__ID_Se__5070F446]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Detalle_P__ID_Pr__06CD04F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Proforma]'))
ALTER TABLE [dbo].[Detalle_Proforma]  WITH CHECK ADD  CONSTRAINT [FK__Detalle_P__ID_Pr__06CD04F7] FOREIGN KEY([ID_Proforma])
REFERENCES [dbo].[Proforma] ([ID_Proforma])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Detalle_P__ID_Pr__06CD04F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Proforma]'))
ALTER TABLE [dbo].[Detalle_Proforma] CHECK CONSTRAINT [FK__Detalle_P__ID_Pr__06CD04F7]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Detalle_P__ID_Se__07C12930]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Proforma]'))
ALTER TABLE [dbo].[Detalle_Proforma]  WITH CHECK ADD  CONSTRAINT [FK__Detalle_P__ID_Se__07C12930] FOREIGN KEY([ID_Servicio])
REFERENCES [dbo].[Servicio] ([ID_Servicio])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Detalle_P__ID_Se__07C12930]') AND parent_object_id = OBJECT_ID(N'[dbo].[Detalle_Proforma]'))
ALTER TABLE [dbo].[Detalle_Proforma] CHECK CONSTRAINT [FK__Detalle_P__ID_Se__07C12930]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Empleado__ID_Car__286302EC]') AND parent_object_id = OBJECT_ID(N'[dbo].[Empleado]'))
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK__Empleado__ID_Car__286302EC] FOREIGN KEY([ID_Cargo])
REFERENCES [dbo].[Cargo] ([ID_Cargo])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Empleado__ID_Car__286302EC]') AND parent_object_id = OBJECT_ID(N'[dbo].[Empleado]'))
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK__Empleado__ID_Car__286302EC]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Evento__ID_Clien__32E0915F]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evento]'))
ALTER TABLE [dbo].[Evento]  WITH CHECK ADD  CONSTRAINT [FK__Evento__ID_Clien__32E0915F] FOREIGN KEY([ID_Cliente])
REFERENCES [dbo].[Cliente] ([ID_Cliente])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Evento__ID_Clien__32E0915F]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evento]'))
ALTER TABLE [dbo].[Evento] CHECK CONSTRAINT [FK__Evento__ID_Clien__32E0915F]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Evento__ID_Emple__33D4B598]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evento]'))
ALTER TABLE [dbo].[Evento]  WITH CHECK ADD  CONSTRAINT [FK__Evento__ID_Emple__33D4B598] FOREIGN KEY([ID_Empleado])
REFERENCES [dbo].[Empleado] ([ID_Empleado])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Evento__ID_Emple__33D4B598]') AND parent_object_id = OBJECT_ID(N'[dbo].[Evento]'))
ALTER TABLE [dbo].[Evento] CHECK CONSTRAINT [FK__Evento__ID_Emple__33D4B598]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Recibo__ID_Event__6FE99F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[Recibo]'))
ALTER TABLE [dbo].[Recibo]  WITH CHECK ADD  CONSTRAINT [FK__Recibo__ID_Event__6FE99F9F] FOREIGN KEY([ID_Evento])
REFERENCES [dbo].[Evento] ([ID_Evento])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Recibo__ID_Event__6FE99F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[Recibo]'))
ALTER TABLE [dbo].[Recibo] CHECK CONSTRAINT [FK__Recibo__ID_Event__6FE99F9F]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Servicio__ID_Cat__300424B4]') AND parent_object_id = OBJECT_ID(N'[dbo].[Servicio]'))
ALTER TABLE [dbo].[Servicio]  WITH CHECK ADD  CONSTRAINT [FK__Servicio__ID_Cat__300424B4] FOREIGN KEY([ID_Categoria])
REFERENCES [dbo].[Categoria] ([ID_Categoria])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Servicio__ID_Cat__300424B4]') AND parent_object_id = OBJECT_ID(N'[dbo].[Servicio]'))
ALTER TABLE [dbo].[Servicio] CHECK CONSTRAINT [FK__Servicio__ID_Cat__300424B4]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Usuario__ID_Empl__2B3F6F97]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK__Usuario__ID_Empl__2B3F6F97] FOREIGN KEY([ID_Empleado])
REFERENCES [dbo].[Empleado] ([ID_Empleado])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Usuario__ID_Empl__2B3F6F97]') AND parent_object_id = OBJECT_ID(N'[dbo].[Usuario]'))
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK__Usuario__ID_Empl__2B3F6F97]
GO
/****** Object:  StoredProcedure [dbo].[Actualizar_Cliente]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actualizar_Cliente]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actualizar_Cliente] AS' 
END
GO
ALTER PROCEDURE [dbo].[Actualizar_Cliente]
	@ID_Cliente [int],
	@Nombre [varchar](40),
	@Apellido [varchar](40),
	@Cedula [varchar](20),
	@Telefono [int]
WITH EXECUTE AS CALLER
AS
begin
Update Cliente set Nombre = @Nombre, Apellido = @Apellido, Cedula = @Cedula,
Telefono = @Telefono where ID_Cliente = @ID_Cliente
end
GO
/****** Object:  StoredProcedure [dbo].[Actualizar_Empleado]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actualizar_Empleado]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actualizar_Empleado] AS' 
END
GO
ALTER PROCEDURE [dbo].[Actualizar_Empleado]
	@ID_Empleado [int],
	@Nombre [varchar](40),
	@Apellido [varchar](40),
	@Cedula [varchar](20),
	@Direccion [varchar](150),
	@Telefono [int]
WITH EXECUTE AS CALLER
AS
begin
Update Empleado set Nombre_E = @Nombre, Apellido_E = @Apellido, 
Cedula_E = @Cedula, Telefono = @Telefono, Direccion = @Direccion
where ID_Empleado = @ID_Empleado
end
GO
/****** Object:  StoredProcedure [dbo].[Actualizar_Producto]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Actualizar_Producto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Actualizar_Producto] AS' 
END
GO
ALTER PROCEDURE [dbo].[Actualizar_Producto]
	@ID_Servicio [int],
	@Descripcion [varchar](150),
	@Precio [int],
	@Cantidad [int]
WITH EXECUTE AS CALLER
AS
begin
Update Servicio set Descripcion = @Descripcion, Precio = @Precio,
Cantidad = @Cantidad where ID_Servicio = @ID_Servicio
end
GO
/****** Object:  StoredProcedure [dbo].[Agregar_Cliente]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Agregar_Cliente]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Agregar_Cliente] AS' 
END
GO
ALTER PROCEDURE [dbo].[Agregar_Cliente]
	@Nombre [varchar](40),
	@Apellido [varchar](40),
	@Cedula [varchar](20),
	@Telefono [int]
WITH EXECUTE AS CALLER
AS
BEGIN
INSERT INTO Cliente(Nombre,Apellido,Cedula,Telefono)
    VALUES (@Nombre,@Apellido,@Cedula,@Telefono)  
end
GO
/****** Object:  StoredProcedure [dbo].[Agregar_ClienteR]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Agregar_ClienteR]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Agregar_ClienteR] AS' 
END
GO
ALTER PROCEDURE [dbo].[Agregar_ClienteR]
	@Nombre [varchar](40),
	@Apellido [varchar](40),
	@Cedula [varchar](20),
	@Telefono [int]
WITH EXECUTE AS CALLER
AS
BEGIN
INSERT INTO Cliente(Nombre,Apellido,Cedula,Telefono)
    VALUES (@Nombre,@Apellido,@Cedula,@Telefono)
	select SCOPE_IDENTITY(); 
end
GO
/****** Object:  StoredProcedure [dbo].[Agregar_Empleado]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Agregar_Empleado]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Agregar_Empleado] AS' 
END
GO
ALTER PROCEDURE [dbo].[Agregar_Empleado]
	@Nombre [varchar](40),
	@Apellido [varchar](40),
	@Cedula [varchar](20),
	@ID_Cargo [int],
	@Telefono [int],
	@Direccion [varchar](150)
WITH EXECUTE AS CALLER
AS
BEGIN
INSERT INTO Empleado(Nombre_E,Apellido_E,Cedula_E,
Telefono,ID_Cargo,Direccion)
    VALUES (@Nombre,@Apellido,@Cedula,@Telefono, @ID_Cargo, @Direccion)  
end
GO
/****** Object:  StoredProcedure [dbo].[Agregar_Producto]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Agregar_Producto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Agregar_Producto] AS' 
END
GO
ALTER PROCEDURE [dbo].[Agregar_Producto]
	@Descripcion [varchar](150),
	@ID_Categoria [int],
	@Precio [int],
	@Cantidad [int]
WITH EXECUTE AS CALLER
AS
BEGIN
INSERT INTO Servicio(Descripcion,ID_Categoria,Precio,Cantidad)
    VALUES (@Descripcion,@ID_Categoria,@Precio,@Cantidad)  
end

exec Agregar_Producto'LUfjfj', 1, 900, 2
GO
/****** Object:  StoredProcedure [dbo].[Agregar_Servicio]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Agregar_Servicio]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Agregar_Servicio] AS' 
END
GO
ALTER PROCEDURE [dbo].[Agregar_Servicio]
	@Descripcion [varchar](40),
	@Precio [float],
	@ID_Categoria [int],
	@Cantidad [int]
WITH EXECUTE AS CALLER
AS
BEGIN
INSERT INTO Servicio(Descripcion,Precio,ID_Categoria,Cantidad)
    VALUES (@Descripcion,@Precio,@ID_Categoria,@Cantidad)  
end
GO
/****** Object:  StoredProcedure [dbo].[Agregar_Usu]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Agregar_Usu]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Agregar_Usu] AS' 
END
GO
ALTER PROCEDURE [dbo].[Agregar_Usu]
	@IDU [int],
	@User [varchar](100),
	@Pass [varchar](150)
WITH EXECUTE AS CALLER
AS
BEGIN
INSERT INTO Usuario(ID_Empleado,Usuario,Contraseña)
    VALUES (@IDU,@User,@Pass)  
end
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Cliente]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Eliminar_Cliente]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Eliminar_Cliente] AS' 
END
GO
ALTER PROCEDURE [dbo].[Eliminar_Cliente]
	@ID_Cliente [int]
WITH EXECUTE AS CALLER
AS
begin
delete from Cliente where ID_Cliente = @ID_Cliente
end
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Producto]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Eliminar_Producto]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Eliminar_Producto] AS' 
END
GO
ALTER PROCEDURE [dbo].[Eliminar_Producto]
	@ID_Servicio [int]
WITH EXECUTE AS CALLER
AS
begin
delete from Servicio where ID_Servicio = @ID_Servicio
end


GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Servicio]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Eliminar_Servicio]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Eliminar_Servicio] AS' 
END
GO
ALTER PROCEDURE [dbo].[Eliminar_Servicio]
	@ID_Servicio [int]
WITH EXECUTE AS CALLER
AS
begin
delete from Servicio where ID_Servicio = @ID_Servicio
end
GO
/****** Object:  StoredProcedure [dbo].[Eliminar_Usuario]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Eliminar_Usuario]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Eliminar_Usuario] AS' 
END
GO
ALTER PROCEDURE [dbo].[Eliminar_Usuario]
	@ID_Usuario [int]
WITH EXECUTE AS CALLER
AS
begin
delete from Usuario where ID_Usuario = @ID_Usuario
end
GO
/****** Object:  StoredProcedure [dbo].[Inicio_Sesion]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Inicio_Sesion]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Inicio_Sesion] AS' 
END
GO
ALTER PROCEDURE [dbo].[Inicio_Sesion]
	@Usuario [varchar](50),
	@Contraseña [varchar](100)
WITH EXECUTE AS CALLER
AS
begin 
if(exists (select * from Usuario Where Usuario = @Usuario and Contraseña = @Contraseña))
select C.ID_Cargo from Usuario U inner join Empleado E on U.ID_Empleado = E.ID_Empleado
inner join Cargo C on C.ID_Cargo = E.ID_Cargo
where Usuario = @Usuario and Contraseña = @Contraseña
else
select '0'
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Detalle]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Detalle]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_Detalle] AS' 
END
GO
ALTER PROCEDURE [dbo].[SP_Detalle]
	@ID_Factura [int]
WITH EXECUTE AS CALLER
AS
begin
select S.Descripcion,S.Precio,D.Cantidad, D.subtotal  
from Evento E inner join Detalle_Factura D
on E.ID_Evento = D.ID_Evento inner join Servicio S
on S.ID_Servicio = D.ID_Servicio inner join Factura F 
on F.ID_Factura = D.ID_Factura
where F.ID_Factura = @ID_Factura
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DetalleF]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DetalleF]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_DetalleF] AS' 
END
GO
ALTER PROCEDURE [dbo].[SP_DetalleF]
	@ID_Factura [int]
WITH EXECUTE AS CALLER
AS
begin
select S.ID_Servicio, S.Descripcion, D.Cantidad, S.Precio, 
D.subtotal from Factura F inner join Detalle_Factura D
on F.ID_Factura = D.ID_Factura inner join Servicio S
on S.ID_Servicio = D.ID_Servicio 
where F.ID_Factura = @ID_Factura
end
GO
/****** Object:  StoredProcedure [dbo].[SP_DetalleP]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DetalleP]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_DetalleP] AS' 
END
GO
ALTER PROCEDURE [dbo].[SP_DetalleP]
	@ID_Proforma [int]
WITH EXECUTE AS CALLER
AS
begin
select S.Descripcion, S.Precio, D.Cantidad, D.Subtotal from Detalle_Proforma D inner join Proforma P
on P.ID_Proforma = D.ID_Proforma inner join Servicio S
on S.ID_Servicio = D.ID_Servicio 
where P.ID_Proforma = @ID_Proforma
end
GO
/****** Object:  StoredProcedure [dbo].[SP_Disponible]    Script Date: 7/11/2022 12:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Disponible]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_Disponible] AS' 
END
GO
ALTER PROCEDURE [dbo].[SP_Disponible]
	@Fecha [varchar](14)
WITH EXECUTE AS CALLER
AS
begin
select ID_Servicio, Descripcion, Precio, Categoria.Nombre_Categoria, 
Cantidad - DBO.Disponible(Servicio.ID_Servicio, @Fecha) AS Disponible
from Servicio inner join Categoria on Categoria.ID_Categoria = Servicio.ID_Categoria
WHERE DBO.Disponible(Servicio.ID_Servicio, @Fecha) is not null and
Cantidad - DBO.Disponible(Servicio.ID_Servicio, @Fecha) != 0 
union
select ID_Servicio, Descripcion, Precio, Categoria.Nombre_Categoria, Cantidad from Servicio 
inner join Categoria on Categoria.ID_Categoria = Servicio.ID_Categoria
where not exists (select 1 from Detalle_Factura inner join Evento
on Evento.ID_Evento = Detalle_Factura.ID_Evento
where Detalle_Factura.ID_Servicio = Servicio.ID_Servicio
and Evento.Fecha_Evento = @Fecha) and Servicio.ID_Categoria != 4 
end
GO
USE [master]
GO
ALTER DATABASE [ML] SET  READ_WRITE 
GO
