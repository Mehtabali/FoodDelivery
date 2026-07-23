# 🍔 Food Delivery Microservices (.NET 10)

A production-oriented Food Delivery backend built using **.NET 10**, **Clean Architecture**, **Microservices**, **Azure Service Bus**, **Transactional Outbox Pattern**, **SQL Server**, and **Azure**.

This project is being developed to demonstrate enterprise-grade microservices architecture, resiliency patterns, and cloud-native development practices.

---

# Current Status

Currently Implemented

- Order Service
- Inventory Service (Skeleton)
- Clean Architecture
- Transactional Outbox Pattern
- Azure Service Bus Publisher
- Background Outbox Processor
- SQL Server Persistence
- EF Core 10
- Swagger
- Dependency Injection

Coming Next

- Inventory Consumer
- Inbox Pattern
- Idempotency
- Saga Pattern
- Payment Service
- Notification Service
- Authentication
- OpenTelemetry
- Application Insights
- Docker Compose
- Kubernetes

---

# Architecture

(Currently)

Client
      |
      v
Order API
      |
      v
CreateOrderHandler
      |
      v
SQL Transaction
   |          |
   |          |
Orders     OutboxMessages
               |
               |
        Background Worker
               |
               |
     Azure Service Bus Queue
               |
               |
      Inventory Service (Next)

---

# Tech Stack

Backend

- .NET 10
- ASP.NET Core Web API
- EF Core 10
- SQL Server

Architecture

- Clean Architecture
- Repository-free EF Core
- Dependency Injection
- Transactional Outbox Pattern

Messaging

- Azure Service Bus

Database

- SQL Server

Cloud

- Azure

---

# Solution Structure

src

BuildingBlocks
    Messaging.Contracts

OrderService
    Api
    Application
    Domain
    Infrastructure

InventoryService
    Api
    Application
    Domain
    Infrastructure

---

# Prerequisites

Install the following:

- .NET 10 SDK
- Visual Studio 2022 (Latest) OR Rider
- SQL Server 2022 / SQL Express
- Azure Service Bus Namespace
- Git

Verify installation

```bash
dotnet --version
```

Expected

```
10.x.x
```

---

# Clone Repository

```bash
git clone https://github.com/Mehtabali/FoodDelivery.git

cd FoodDelivery
```

---

# Restore Packages

```bash
dotnet restore
```

---

# Build

```bash
dotnet build
```

---

# Database Setup

Update

```
src/OrderService/OrderService.Api/appsettings.Development.json
```

Example

```json
{
  "ConnectionStrings": {
    "OrderDatabase": "Server=.;Database=FoodDeliveryOrders;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

---

# User Secrets

Initialize

```bash
dotnet user-secrets init --project src/OrderService/OrderService.Api
```

Set Azure Service Bus connection string

```bash
dotnet user-secrets set \
"AzureServiceBus:ConnectionString" \
"<YOUR_CONNECTION_STRING>" \
--project src/OrderService/OrderService.Api
```

---

# EF Core

Create Database

```bash
dotnet ef database update \
--project src/OrderService/OrderService.Infrastructure \
--startup-project src/OrderService/OrderService.Api
```

---

# Run API

```bash
dotnet run --project src/OrderService/OrderService.Api
```

Swagger

```
https://localhost:xxxx/swagger
```

---

# Current Workflow

POST /api/orders

↓

Order Created

↓

Order Items Saved

↓

ReserveInventoryCommand Created

↓

Outbox Message Saved

↓

Outbox Processor

↓

Azure Service Bus Queue

↓

Inventory Service
(Coming Next)

---

# Current Features

✔ Order Creation

✔ Transactional Outbox

✔ Azure Service Bus Publisher

✔ Background Outbox Processor

✔ SQL Transaction

✔ Clean Architecture

✔ Dependency Injection

---

# Future Roadmap

Phase 1

- Order Service
- Inventory Service
- Azure Service Bus

Phase 2

- Inbox Pattern
- Idempotency

Phase 3

- Payment Service

Phase 4

- Notification Service

Phase 5

- Authentication

Phase 6

- Observability

- OpenTelemetry
- Application Insights
- Serilog

Phase 7

Deployment

- Docker
- Azure Container Apps
- Azure App Service
- GitHub Actions

---

# Author

Mehtab Ali

Senior .NET / Azure Developer

GitHub

https://github.com/Mehtabali
<img width="1693" height="929" alt="InitialSystemDEsignDraft" src="https://github.com/user-attachments/assets/ef36d6c3-0e3f-4fac-ab54-6625c916d8b1" />
