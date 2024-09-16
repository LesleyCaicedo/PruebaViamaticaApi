CREATE DATABASE [PruebaViamatica]
GO

USE [PruebaViamatica]
GO
/****** Object:  Table [dbo].[Opciones]    Script Date: 16/09/2024 0:02:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Opciones](
	[idOpcion] [int] IDENTITY(1,1) NOT NULL,
	[NombreOpcion] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idOpcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persona]    Script Date: 16/09/2024 0:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persona](
	[idPersona] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Apellido] [nvarchar](max) NULL,
	[Cedula] [nvarchar](max) NULL,
	[FechaNacimiento] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[idPersona] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 16/09/2024 0:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[idRol] [int] IDENTITY(1,1) NOT NULL,
	[NombreRol] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolOpciones]    Script Date: 16/09/2024 0:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolOpciones](
	[FK_idRol] [int] NOT NULL,
	[FK_idOpcion] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolUsuarios]    Script Date: 16/09/2024 0:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolUsuarios](
	[FK_idRol] [int] NOT NULL,
	[FK_idUsuario] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sesiones]    Script Date: 16/09/2024 0:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sesiones](
	[FechaIngreso] [datetime] NULL,
	[FechaCierre] [datetime] NULL,
	[FK_idUsuario] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 16/09/2024 0:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [nvarchar](max) NULL,
	[Clave] [nvarchar](max) NULL,
	[Correo] [nvarchar](max) NULL,
	[SesionActiva] [char](1) NULL,
	[FK_idPersona] [int] NOT NULL,
	[Estatus] [nvarchar](max) NULL,
	[IntentosTemp] [int] NULL,
	[IntentosTotal] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Opciones] ON 

INSERT [dbo].[Opciones] ([idOpcion], [NombreOpcion]) VALUES (1, N'MantenimientoUsuario')
INSERT [dbo].[Opciones] ([idOpcion], [NombreOpcion]) VALUES (2, N'Bienvenida')
INSERT [dbo].[Opciones] ([idOpcion], [NombreOpcion]) VALUES (3, N'Dashboard')
SET IDENTITY_INSERT [dbo].[Opciones] OFF
GO
SET IDENTITY_INSERT [dbo].[Persona] ON 

INSERT [dbo].[Persona] ([idPersona], [Nombre], [Apellido], [Cedula], [FechaNacimiento]) VALUES (1, N'Andrea', N'Caicedo', N'0938173812', CAST(N'2001-03-12' AS Date))
INSERT [dbo].[Persona] ([idPersona], [Nombre], [Apellido], [Cedula], [FechaNacimiento]) VALUES (5, N'Pepe', N'Cigarra', N'0938127381', CAST(N'2003-07-17' AS Date))
SET IDENTITY_INSERT [dbo].[Persona] OFF
GO
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([idRol], [NombreRol]) VALUES (1, N'Admin')
INSERT [dbo].[Rol] ([idRol], [NombreRol]) VALUES (2, N'Usuario')
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
INSERT [dbo].[RolOpciones] ([FK_idRol], [FK_idOpcion]) VALUES (1, 1)
INSERT [dbo].[RolOpciones] ([FK_idRol], [FK_idOpcion]) VALUES (1, 2)
INSERT [dbo].[RolOpciones] ([FK_idRol], [FK_idOpcion]) VALUES (1, 3)
INSERT [dbo].[RolOpciones] ([FK_idRol], [FK_idOpcion]) VALUES (2, 2)
GO
INSERT [dbo].[RolUsuarios] ([FK_idRol], [FK_idUsuario]) VALUES (1, 1)
INSERT [dbo].[RolUsuarios] ([FK_idRol], [FK_idUsuario]) VALUES (2, 5)
GO
INSERT [dbo].[Sesiones] ([FechaIngreso], [FechaCierre], [FK_idUsuario]) VALUES (CAST(N'2024-09-15T22:33:48.647' AS DateTime), CAST(N'2024-09-15T23:13:55.800' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([idUsuario], [Usuario], [Clave], [Correo], [SesionActiva], [FK_idPersona], [Estatus], [IntentosTemp], [IntentosTotal]) VALUES (1, N'Andreacg03', N'Administrador*', N'acaicedo@mail.com', N'I', 1, N'Activo', 0, 7)
INSERT [dbo].[Usuarios] ([idUsuario], [Usuario], [Clave], [Correo], [SesionActiva], [FK_idPersona], [Estatus], [IntentosTemp], [IntentosTotal]) VALUES (5, N'Pepeprueba1', N'Usuario*', N'pcigarra@mail.com', N'I', 5, N'Activo', 0, 0)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[RolOpciones]  WITH CHECK ADD FOREIGN KEY([FK_idRol])
REFERENCES [dbo].[Rol] ([idRol])
GO
ALTER TABLE [dbo].[RolOpciones]  WITH CHECK ADD FOREIGN KEY([FK_idOpcion])
REFERENCES [dbo].[Opciones] ([idOpcion])
GO
ALTER TABLE [dbo].[RolUsuarios]  WITH CHECK ADD FOREIGN KEY([FK_idRol])
REFERENCES [dbo].[Rol] ([idRol])
GO
ALTER TABLE [dbo].[RolUsuarios]  WITH CHECK ADD FOREIGN KEY([FK_idUsuario])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Sesiones]  WITH CHECK ADD FOREIGN KEY([FK_idUsuario])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD FOREIGN KEY([FK_idPersona])
REFERENCES [dbo].[Persona] ([idPersona])
GO
/****** Object:  StoredProcedure [dbo].[ControlSesion]    Script Date: 16/09/2024 0:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[ControlSesion]
	@TipoControl NVARCHAR(MAX), @idUsuario INT
AS
BEGIN
	IF (@TipoControl = 'SesionIniciada')
	BEGIN
	  IF EXISTS(SELECT 1 FROM Sesiones WHERE FK_idUsuario = @idUsuario) 
		BEGIN
			UPDATE Usuarios SET SesionActiva = 'A' WHERE idUsuario = @idUsuario;
			UPDATE Sesiones SET FechaIngreso = GETDATE() WHERE FK_idUsuario = @idUsuario; 
			--UPDATE Sesiones SET FechaCierre = NULL WHERE FK_idUsuario = @idUsuario;
		END
		ELSE
		BEGIN
			INSERT INTO Sesiones(FechaIngreso, FK_idUsuario) VALUES(GETDATE(), @idUsuario);
			UPDATE Usuarios SET SesionActiva = 'A' WHERE idUsuario = @idUsuario;
		END
	END;

	IF(@TipoControl = 'SesionCerrada')
	BEGIN
	  UPDATE Sesiones SET FechaCierre = GETDATE() WHERE FK_idUsuario = @idUsuario;
	  UPDATE Usuarios SET SesionActiva = 'I' WHERE idUsuario = @idUsuario;
	END;
END;
GO
/****** Object:  StoredProcedure [dbo].[RolUsuario]    Script Date: 16/09/2024 0:02:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[RolUsuario]
 @idRol INT, @idUsuario INT
AS
BEGIN
	INSERT INTO RolUsuarios(FK_idRol, FK_idUsuario) VALUES(@idRol, @idUsuario);
END;
GO
