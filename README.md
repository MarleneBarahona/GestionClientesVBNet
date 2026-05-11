# Gestión de clients ASP.NET (Visual Basic)
Aplicación web desarrollada en ASP.NET Web Forms con Visual Basic.NET para la gestión de clientes, con autenticación de usuarios y registro de bitácora de cambios.

## Tecnologías utilizadas:
- ASP.NET Web Forms
- Visual Basic.NET
- SHA256
- Stored Procedures
- SQL Server
- Git

## Funcionalidades implementadas:
- Login de usuarios
- Inicio y cierre de sesión
- Control de sesiones
	- Cierre de sesión automática a los 20 minutos 
	- Redirección a pagina Login si no detecta sesión activa
- CRUD de Clientes 
	- Ver
	- Crear
	- Editar
	- Eliminar
- Soft Delete
	- Función de Eliminar clientes no borra fisicamente, únicamente lo desactiva a la vista del usuario 
- Registro de bitácora de cambios
- Hashing SHA256 para contraseñas
- Validación de campos
	- No se permiten vacíos
	- Correo debe contener "@"
	- Teléfono debe ser formato ####-####
- Prevención inyecciones SQL
  - Uso de Stored Procedure con parametros
## Usuario de pruebas:
User: admin
Pass: admin123
