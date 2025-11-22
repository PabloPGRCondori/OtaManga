📘 OtaManga Store — Backend (Proyecto Académico)

📝 Descripción General

OtaManga Store es un proyecto académico orientado al desarrollo de una plataforma backend profesional, aplicando Arquitectura Limpia, Patrón Repository, Unit of Work, JWT, y EF Core con Identity.
Su objetivo es simular el backend de una tienda especializada en:

📚 Mangas (venta)

📘 Novelas Ligeras (venta)

🎬 Animes (información, no venta)

El sistema gestiona catálogo, usuarios, precios, órdenes y métricas de interacción.
Todo el proyecto está diseñado con un enfoque realista, como si fuera un producto en desarrollo para un e-commerce especializado.

🎯 Propósito del Proyecto

El propósito principal del backend de OtaManga Store es construir una base sólida, escalable y mantenible, que permita:

- Administrar productos del catálogo (mangas, novelas ligeras y animes).
- Controlar precios, stock e imágenes.
- Registrar y gestionar órdenes de compra.
- Proveer autenticación segura mediante Identity + JWT.
- Registrar métricas de clics para analizar popularidad.
- Permitir predicciones y recomendaciones futuras basadas en comportamiento de usuarios.
- Contar con una vista de administrador capaz de ver:
  - Ventas
  - Clientes registrados
  - Gráficos de métricas
  - Historial de precio
  - Calendario de tareas
  - Recomendaciones de contenido

El proyecto sirve como práctica integral para un entorno real de desarrollo backend, aplicando buenas prácticas, separación de responsabilidades y trabajo colaborativo en equipo.

🏗 Alcance del Backend

Actualmente el backend soporta:

✔ Autenticación y Roles

- Registro y login vía Identity + JWT
- Roles: Admin, Editor y Client
- Seeder inicial automático

✔ Catálogo

- Entidad content para mangas, novelas y animes
- Authors
- Categories
- PriceHistory (historial de precios)
- ClickMetrics (métricas de interacción)

✔ Órdenes

- Orders
- OrderItems
- Control de stock y estados
- PriceHistory vinculado

✔ Infraestructura

- Base de datos PostgreSQL en Supabase
- Migraciones con EF Core
- Arquitectura Limpia
- Repositorios y Unit of Work
- Servicios en capa Application

🧱 Arquitectura

El backend se construye bajo Clean Architecture, dividido en:

- 📁 OtoMangaStore.Domain
- 📁 OtoMangaStore.Application
- 📁 OtoMangaStore.Infrastructure
- 📁 OtoMangaStore.Api

🧩 Domain

- Modelos puros y reglas de negocio.

🧠 Application

- Interfaces, casos de uso, servicios y DTOs.

🗄 Infrastructure

- EF Core, repositorios, migraciones, Supabase, Identity.

🌐 API

- Controladores, JWT, CORS, configuración general.

🚀 Objetivo Educativo

Este proyecto demuestra cómo construir un backend modular y escalable con buenas prácticas:

- Patrón Repository + UnitOfWork
- Arquitectura limpia
- Identity + JWT
- Entity Framework Core + PostgreSQL
- Inyección de dependencias (DI)
- Migraciones y despliegue en bases remotas

El propósito es que el equipo pueda trabajar de forma colaborativa, asignando responsabilidades por capas, servicios y controladores, replicando un flujo de trabajo profesional.

🌩 Estado Actual

El backend ya cuenta con:

- Migraciones ejecutadas en Supabase
- Seeder funcional
- Repositorios + UnitOfWork
- Autenticación y JWT
- Lecturas probadas correctamente
- Estructura limpia completa

Próximos pasos:

- Agregar controladores REST
- Integrar AutoMapper
- Añadir validaciones con FluentValidation
- Activar Swagger Documentation