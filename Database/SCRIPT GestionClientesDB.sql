CREATE DATABASE GestionClientesDB;
GO

USE GestionClientesDB;
GO


CREATE TABLE Usuarios 
(
	IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
	Usuario NVARCHAR(50) NOT NULL UNIQUE,
	Contrasena NVARCHAR(250) NOT NULL,
	NombreCompleto NVARCHAR(100) NOT NULL,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
)
GO

CREATE TABLE Clientes 
(
	IdCliente INT IDENTITY(1,1) PRIMARY KEY,
	Nombre NVARCHAR(50) NOT NULL,
	Apellido NVARCHAR(50) NOT NULL,
	Correo NVARCHAR(50) NULL,
	Telefono NVARCHAR(20) NULL,
	Direccion NVARCHAR(50) NULL,
	FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
	FechaUltimaModificacion DATETIME NOT NULL DEFAULT GETDATE(),
	Activo BIT NOT NULL DEFAULT 1
)
GO

CREATE TABLE BitacoraGestionClientes
(
	IdBitacora INT IDENTITY(1,1) PRIMARY KEY,
	TablaAfectada NVARCHAR(50) NOT NULL,
	IDRegistroAfectado INT NOT NULL,
	Accion NVARCHAR(50) NOT NULL,
	Usuario NVARCHAR(50) NOT NULL,
	FechaRegistro DATETIME NOT NULL DEFAULT GETDATE()
)
GO

/****** Object:  StoredProcedure [dbo].[SP_CARGAR_CLIENTES]    Script Date: 5/10/2026 6:49:11 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marlene Barahona
-- Create date: 10 5 2026
-- Description:	Procedimiento almacenado que obtiene a todos los clientes registados que no han sido eliminados/desactivados
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[SP_CARGAR_CLIENTES]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT *
    FROM Clientes
    WHERE Activo = 1
    ORDER BY IdCliente DESC
END
GO

/****** Object:  StoredProcedure [dbo].[SP_DESACTIVAR_CLIENTES]    Script Date: 5/10/2026 6:49:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marlene Barahona
-- Create date: 10 5 2026
-- Description:	Procedimiento almacenado que desactiva a los clientes ya registados
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[SP_DESACTIVAR_CLIENTES]
	-- Add the parameters for the stored procedure here
	@IdCliente as int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE Clientes
        SET Activo = 0,
        FechaUltimaModificacion = GETDATE()
        WHERE IdCliente = @IdCliente
END
GO

/****** Object:  StoredProcedure [dbo].[SP_INSERT_CLIENTES]    Script Date: 5/10/2026 6:50:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marlene Barahona
-- Create date: 5 10 2026
-- Description:	Procedimiento almacenado que guarda a los clientes nuevos registados y devuelve ID del insert
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[SP_INSERT_CLIENTES]
	-- Add the parameters for the stored procedure here
	@Nombre as nvarchar(50),
	@Apellido as nvarchar(50),
	@Correo as nvarchar(50),
	@Telefono as nvarchar(20),
	@Direccion as nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Clientes
        (
            Nombre,
            Apellido,
            Correo,
            Telefono,
            Direccion
        )
        VALUES
        (
            @Nombre,
            @Apellido,
            @Correo,
            @Telefono,
            @Direccion
        )


        SELECT SCOPE_IDENTITY()
END
GO

/****** Object:  StoredProcedure [dbo].[SP_UPDATE_CLIENTES]    Script Date: 5/10/2026 6:50:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marlene Barahona
-- Create date: 5 10 2026
-- Description:	Procedimiento almacenado que actualiza a los clientes ya registados
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[SP_UPDATE_CLIENTES]
	-- Add the parameters for the stored procedure here
	@IdCliente as int,
	@Nombre as nvarchar(50),
	@Apellido as nvarchar(50),
	@Correo as nvarchar(50),
	@Telefono as nvarchar(20),
	@Direccion as nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	 UPDATE Clientes
        SET
            Nombre = @Nombre,
            Apellido = @Apellido,
            Correo = @Correo,
            Telefono = @Telefono,
            Direccion = @Direccion,
            FechaUltimaModificacion = GETDATE()
        WHERE IdCliente = @IdCliente
END
GO

/****** Object:  StoredProcedure [dbo].[SP_VALIDAR_LOGIN_USUARIO]    Script Date: 5/10/2026 6:50:51 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Marlene Barahona
-- Create date: 5 10 2026
-- Description:	Procedimiento almacenado verificar si usuario y contraseña coinciden
-- =============================================
CREATE OR ALTER PROCEDURE [dbo].[SP_VALIDAR_LOGIN_USUARIO]
	-- Add the parameters for the stored procedure here
	@Usuario as nvarchar(50),
	@Password as nvarchar(250)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Usuario
        FROM Usuarios
        WHERE Usuario = @Usuario
        AND Contrasena = @Password
END
GO

INSERT INTO dbo.Usuarios
(
    Usuario,
    Contrasena,
    NombreCompleto
)
VALUES
(
    'admin',
    '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',
    'Administrador'
);
GO


INSERT INTO [dbo].[Clientes]
           ([Nombre]
           ,[Apellido]
           ,[Correo]
           ,[Telefono]
           ,[Direccion]
           )
     VALUES
           ('Daniel'
           ,'Hernández'
           ,'daniel.hernandez98@gmail.com'
           ,'7894-5612'
           ,'Colonia Las Margaritas casa 15'
           )
GO

INSERT INTO Clientes
(
    Nombre,
    Apellido,
    Correo,
    Telefono,
    Direccion
)
VALUES
(
    'Carlos',
    'Martinez',
    'carlos.martinez@email.com',
    '7777-1111',
    'San Salvador'
);
GO
INSERT INTO Clientes
(
    Nombre,
    Apellido,
    Correo,
    Telefono,
    Direccion
)
VALUES
(
    'Andrea',
    'Lopez',
    'andrea.lopez@email.com',
    '7777-2222',
    'Santa Tecla'
);
GO
INSERT INTO Clientes
(
    Nombre,
    Apellido,
    Correo,
    Telefono,
    Direccion
)
VALUES
(
    'Ricardo',
    'Perez',
    'ricardo.perez@email.com',
    '7777-3333',
    'Antiguo Cuscatlán'
);
GO
INSERT INTO Clientes
(
    Nombre,
    Apellido,
    Correo,
    Telefono,
    Direccion
)
VALUES
(
    'Sofia',
    'Ramirez',
    'sofia.ramirez@email.com',
    '7777-4444',
    'Soyapango'
);