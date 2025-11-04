# API REST - Sistema de Pagos de Servicios Básicos

Este proyecto es un backend desarrollado con .NET 8, aplicando los principios de Arquitectura Limpia (Clean Architecture) y buenas prácticas de desarrollo profesional.
Su objetivo es registrar y consultar pagos de servicios básicos (agua, electricidad, telecomunicaciones) de clientes internos de la empresa.

## Características principales

✅ **Arquitectura Limpia:** Separación clara por capas (Domain, Application, Infrastructure, Api).

✅ **Entity Framework Core (SQL Server):** ORM para acceso a datos.

✅ **FluentValidation:** Validaciones declarativas de los DTOs.

✅ **Inyección de dependencias:** Manejada por Microsoft.Extensions.DependencyInjection.

✅ **Patrón de diseño CQRS:** Separación de comandos y consultas.

✅ **Validaciones personalizadas:** Lógica de negocio para montos y moneda.

✅ **Mensajes y respuestas controladas:** Estructura uniforme de errores y resultados.

✅ **Escalabilidad:** Estructura preparada para añadir nuevos servicios y entidades fácilmente.

## Funcionalidades del sistema

✅ Permite registrar un nuevo pago realizado por un cliente.

✅ Permite obtener el listado de pagos registrados para un cliente específico.

✅ El sistema aplica reglas que garantizan la integridad y consistencia de los datos

## Posibles mejoras del sistema

1. Actualizar estado de pago: Endpoint para cambiar de “pendiente” a “pagado” o “rechazado”.
2. Eliminar pago: Endpoint DELETE /api/payments/{id} con soft delete.
3. Paginación y filtros: Mejorar el endpoint de consulta con page, pageSize, status, dateRange.
4. Auditoría y logging: Registrar quién crea o modifica los pagos.
5. Autenticación y roles: Añadir JWT y control de acceso.
6. Docker Compose: Contenedor para la API y SQL Server para despliegue rápido.

## Stack Tecnológico

- Framework:	.NET 8
- Base de datos:	SQL Server
- ORM:	Entity Framework Core
- Validaciones:	FluentValidation
- Inyección de dependencias:	Microsoft.Extensions.DependencyInjection
- Testing:	xUnit

## Instalación

Clonar el repositorio:

```bash
git clone https://github.com/ncolque/TestBackEsAppNet.git
cd TestBackEsAppNet
```

Configurar variables de entorno

```env
"ConectionDatabase": "Server=Server;Database=NameDataBase;Trusted_Connection=True;TrustServerCertificate=True;"
```

Restaurar dependencias

```bash
dotnet restore
```

Aplicar migraciones
Opción 1:
```bash
dotnet ef database update --project Infrastructure --startup-project Api
```
Opción 2:
```Consola del Administrador de paquetes (Package Manager Console)
add-migration InitialMigration
update-database
```

Ejecutar la API

```bash
dotnet run --project Api
```

La API estará disponible en:
http://localhost:5000/swagger (HTTP)
https://localhost:5001/swagger (HTTPS)

---

## Estructura del Proyecto

```
src/
├── Api/                           # Capa de presentación (controllers, middlewares)
│   ├── Controllers/
│   │   └── PaymentsController.cs
│   └── Program.cs / appsettings.json
│
├── Application/                   # Capa de lógica de negocio
│   ├── Abstractions/
│   ├── Behaviours/
│   ├── Commons/
│   ├── Constants/
│   ├── Dtos/
│   ├── Helpers/
│   ├── Interfaces/
│   ├── Mappings/
│   ├── UseCases/
│   └── DependencyInjection.cs
│
├── Infrastructure/                # Capa de acceso a datos
│   ├── Persistence/
│   ├── Services/
│   └── DependencyInjection.cs
│
├── Domain/                        # Entidades de negocio
│   └── Entities/
│       └── Payment.cs
│
└── README.md                      # Documentación del proyecto
```

---

## Endpoints principales

**Registrar un pago**

POST /api/payments

Request:

```json
{
  "customerId": "cfe8b150-2f84-4a1a-bdf4-923b20e34973",
  "serviceProvider": "SERVICIOS ELÉCTRICOS S.A.",
  "amount": 120.50
}
```

Validaciones:

- El monto no puede ser mayor a 1500 Bs.
- Solo se aceptan montos expresados en bolivianos (Bs), no en dólares.
- Estado inicial del pago: "pendiente".

**Consultar pagos por cliente**

GET /api/payments?customerId={GUID}

Response (200 OK):

```json
[
  {
    "paymentId": "a248ad43-1f44-4b32-b0a0-e1c725b9bb7d",
    "serviceProvider": "SERVICIOS ELÉCTRICOS S.A.",
    "amount": 120.50,
    "status": "pendiente",
    "createdAt": "2025-07-17T08:30:00Z"
  }
]
```

---

## Autor

- Desarrollador: Nicolás Colque
- Versión: 1.0.0
- Framework: .NET 8

## Licencia
Este proyecto se distribuye bajo la licencia MIT. Consulta el archivo [LICENSE](LICENSE) para más información.

---
© 2025 Nicolás Colque — Todos los derechos reservados.