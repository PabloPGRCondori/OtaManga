Aquí tienes un análisis detallado de la estructura y funcionamiento del proyecto `OtoMangaStore`.

Este proyecto sigue una **Arquitectura Limpia (Clean Architecture)** o **Arquitectura Cebolla (Onion Architecture)**, diseñada para separar las responsabilidades, facilitar el mantenimiento y desacoplar la lógica de negocio de la infraestructura externa (base de datos, API, etc.).

### Estructura General

El proyecto está dividido en 4 capas principales (proyectos en la solución):

#### 1. OtoMangaStore.Api (Capa de Presentación)
Es el punto de entrada de la aplicación. Se encarga de recibir las peticiones HTTP del cliente (Frontend o usuarios) y devolver respuestas. No contiene lógica de negocio compleja, solo orquesta las llamadas.

*   **Función:**
    *   **Configuración (`Program.cs`):** Configura la inyección de dependencias, conexión a base de datos (PostgreSQL), autenticación (JWT + Cookies para Admin), Swagger y CORS.
    *   **Controladores (`Controllers/`):** Exponen endpoints RESTful (e.g., `MangasController`, `OrdersController`) para que aplicaciones externas consuman los datos.
    *   **Áreas Admin (`Areas/Admin/`):** Contiene una interfaz de administración renderizada en el servidor usando **Razor Pages** para gestionar autores, categorías y dashboard.
    *   **Seguridad:** Maneja la autenticación de usuarios mediante `Microsoft.Identity` y tokens JWT.

#### 2. OtoMangaStore.Application (Capa de Aplicación)
Contiene la lógica de negocio de la aplicación. Define *qué* debe hacer el sistema sin preocuparse de *cómo* se guardan los datos.

*   **Función:**
    *   **Servicios (`Services/`):** Implementan la lógica principal.
        *   Ejemplo (`OrderService.cs`): Al crear una orden, valida el stock, calcula el precio total basado en el historial de precios, descuenta inventario y guarda la orden.
    *   **Interfaces (`Interfaces/`):** Define contratos para repositorios (`IMangaRepository`) y servicios (`IOrderService`). Esto permite que la capa de Aplicación no dependa directamente de la Infraestructura.
    *   **DTOs (`DTOs/`):** Objetos de Transferencia de Datos que definen qué información entra y sale de la API (e.g., `CreateOrderDto`, `MangaDto`), evitando exponer las entidades de dominio directamente.

#### 3. OtoMangaStore.Domain (Capa de Dominio)
Es el núcleo del proyecto. Contiene las entidades que modelan la base de datos y las reglas de negocio más puras y atómicas.

*   **Función:**
    *   **Entidades (`Models/`):** Clases que representan las tablas de la base de datos.
        *   `Content` (Manga): Representa el producto.
        *   `Order` / `OrderItem`: Representan las compras.
        *   `PriceHistory`: Historial de precios para auditoría.
        *   `ClickMetric`: Métricas de popularidad.
        *   `ApplicationUser`: Usuario del sistema (extiende Identity).

#### 4. OtoMangaStore.Infrastructure (Capa de Infraestructura)
Se encarga de la comunicación con sistemas externos, principalmente la base de datos. Implementa las interfaces definidas en la capa de Aplicación.

*   **Función:**
    *   **Persistencia (`Persistence/OtoDbContext.cs`):** Configuración de Entity Framework Core. Define las tablas y relaciones (Foreign Keys) entre entidades.
    *   **Repositorios (`Repositories/`):** Implementan el acceso directo a datos (CRUD). Por ejemplo, `MangaRepository` sabe cómo hacer un `SELECT` o `INSERT` de un manga en PostgreSQL.
    *   **UnitOfWork (`UnitOfWork/`):** Patrón que agrupa múltiples operaciones de repositorios en una sola transacción de base de datos (todo o nada).

### Resumen del Flujo de Datos

1.  **Usuario** hace una petición a la **API** (e.g., "Comprar Manga").
2.  **Controller** recibe la petición y llama al **Service** (Capa Application).
3.  **Service** valida reglas de negocio y usa el **Repository** (Interfaz).
4.  **Repository** (Implementado en Infrastructure) accede a la **Base de Datos** usando el **Domain**.
5.  La respuesta fluye de vuelta hacia arriba hasta el usuario.

Esta estructura garantiza que si mañana cambias de base de datos (e.g., a MySQL) o de framework web, la lógica de negocio (Application/Domain) permanezca intacta.