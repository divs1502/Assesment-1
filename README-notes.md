# Avalpha Technologies – Commission Calculator API

## 📌 Overview

This project is a **.NET 8 ASP.NET Core Web API** that calculates commission amounts for:

- **Avalpha Technologies**
- **A Competitor**

based on:
- Local sales count
- Foreign sales count
- Average sale amount

The solution includes **xUnit unit tests** to validate business logic and error handling.

---

## 🧱 Solution Structure



# 🧮 Avalpha Technologies – Commission Calculator API

> Production-ready **Commission Calculator** for **Avalpha Technologies**  
> This project demonstrates a full-stack implementation using **.NET 8 Web API** and **React**, including validation, calculations, and automated tests.

---

## 🚀 What was built

- ✅ **Backend (C# / .NET 8 Web API)**
  - Commission calculation logic implemented
  - Input validation and error handling
  - Typed request/response DTOs
  - xUnit unit tests

- ✅ **Frontend (React)**
  - API integration with backend
  - User inputs for sales data
  - Commission results displayed with clear labels
  - Error handling and form validation
  - React Testing Library + Jest tests

🎯 **Focus:** correctness, clarity, clean structure, and testability


## 🔀 Repository Workflow Followed

1. Forked the repository
2. Worked in a feature branch (`feature/commission-implementation`)
3. Implemented backend logic + frontend wiring
4. Added unit tests (backend + frontend)
5. Maintained small, meaningful commits
6. Added documentation (this README-notes)


## 🧠 Business Rules

### Avalpha Technologies Commission Rates
- **Local Sales:** 20%
- **Foreign Sales:** 35%

### Competitor Commission Rates
- **Local Sales:** 2%
- **Foreign Sales:** 7.55%


## 📥 Inputs

| Field               | Description             |
|---------------------|-------------------------|
| `localSalesCount`   | Number of local sales   |
| `foreignSalesCount` | Number of foreign sales |
| `averageSaleAmount` | Average amount per sale |


## 📤 Output Example

**Input:**
- Local Sales: 10  
- Foreign Sales: 10  
- Average Sale Amount: £100  

**Avalpha Commission:**
- Local → 20% × 10 × 100 = **£200**
- Foreign → 35% × 10 × 100 = **£350**
- **Total = £550**

**Competitor Commission:**
- Local → 2% × 10 × 100 = **£20**
- Foreign → 7.55% × 10 × 100 = **£75.5**
- **Total = £95.5**

---

## 🧩 Completed Tasks Checklist

- [x] React frontend wired to backend API
- [x] Commission calculation logic implemented
- [x] Input validation (non-negative, upper bounds)
- [x] Typed request/response DTOs
- [x] Clear UI display with formatted results
- [x] Graceful error handling (API + UI)
- [x] Backend unit tests (xUnit)
- [x] Frontend tests (React Testing Library + Jest)
- [x] Documentation and clean commits

---

## 🧱 Tech Stack

### Backend
- .NET 8
- ASP.NET Core Web API
- xUnit
- Microsoft.NET.Test.Sdk
- Swagger

### Frontend
- React
- JavaScript
- Fetch API
- Jest
- React Testing Library

---

## ▶️ How to Run the Project

### Backend

```bash
cd api
dotnet restore
dotnet run

### Swagger UI:

https://localhost:5000/swagger/index.html


### Frontend

```bash
cd ui
npm install
npm start



🧠 Design Decisions & Notes

Decimal used for all financial calculations

Controller logic kept deterministic and testable

Validation handled centrally in the API

UI kept simple and readable

CRA warnings acknowledged but non-blocking

Focused on correctness over over-engineering


🐛 Known Limitations

Single Currency: Currently GBP only (easily extensible)

No Persistence: Calculations are stateless (by design)

Rate Configuration: Hard-coded rates (move to config/DB for dynamic updates)

Basic Rate Limiting: No distributed rate limiting yet


👤 Author

Divyesh Lahane
.NET & React Developer