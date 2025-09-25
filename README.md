# Minimal APIs at Scale  
**Enterprise-Ready Minimal APIs in .NET: Scaling Simplicity**  
_A demo repo for my talk at CommitYourCode 2025_  

---

## 📌 Overview
This repository contains the **finished demo code** from my presentation **Minimal APIs at Scale**.

In the talk, I live-coded parts of the `CustomerEndpoints` to show how Minimal APIs evolve from a toy Weather example in `Program.cs` into a **structured, enterprise-ready API**.  
This repo includes the **complete, working version** of that code.

You’ll see:
- How to structure Minimal APIs with **extension methods**
- Using **DTOs** (`CustomerRequest`, `CustomerResponse`) to separate contracts from entities
- Built-in validation in .NET 10
- Consistent error handling with **endpoint filters**
- Returning strong responses with `TypedResults` and `CreatedAtRoute`
- Configuring **OpenAPI + Scalar** via extension methods

---

## 🗂 Project Structure

```text
.
├── Endpoints/
│   └── Customer/
│       └── CustomerEndpoints.cs
├── Extensions/
│   └── OpenApiExtensions.cs
├── Filters/
│   └── ExceptionHandlingFilter.cs
├── Models/
│   ├── Customer.cs
│   └── DTO/
│       ├── CustomerRequest.cs
│       └── CustomerResponse.cs
├── Services/
│   ├── ICustomerService.cs
│   └── CustomerService.cs
├── Program.cs
└── <project>.csproj
Endpoints/Customer/ – Customer-specific endpoints via extension methods (app.MapCustomerEndpoints()).

Extensions/ – Application configuration helpers (e.g., ConfigureOpenApi, MapScalar).

Filters/ – Cross-cutting concerns (e.g., exception handling).

Models/ – Entities and DTOs (request/response contracts).

Services/ – Business logic/data access behind an interface.

Program.cs – Clean entry point that wires everything together.
