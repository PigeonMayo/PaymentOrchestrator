# PaymentOrchestrator

A full-stack payment management system built with **React** frontend and **ASP.NET Core** backend, featuring real-time payment processing and a clean, intuitive user interface.

## Project Structure

This project is split into **2 main components**:

- **Frontend**: React application for payment management UI
- **Backend**: ASP.NET Core REST API for payment processing

## Getting Started

### Backend Setup (ASP.NET Core)

#### Prerequisites
Ensure you have the following NuGet packages installed:

**Database Packages:**
```xml
Microsoft.EntityFrameworkCore v7.0.20
Microsoft.EntityFrameworkCore.InMemory v7.0.20
```

**Unit Testing Packages:**
```xml
xunit v2.4.2
xunit.runner.visualstudio v2.4.5
```

#### Running the Backend
1. Clone the repository
2. Navigate to the backend directory
3. Run the application - startup should land you on the **Swagger page**

> 💡 **Note**: The backend includes prepopulated data for testing. This can be commented out in `program.cs` if not needed.

### Frontend Setup (React)

#### Prerequisites & Installation
```bash
cd paymentfe
npm install
```

> **Important**: Ensure the backend is running before starting the frontend!

The frontend will automatically display all prepopulated data upon startup.

##  Architecture

### Frontend Architecture
- **Simple unidirectional data flow** - child to parent communication only
- **No data crossover** between components
- **React** chosen for its versatility and real-time update capabilities

**Component Layout:**
<img width="1298" height="539" alt="ReactLayout" src="https://github.com/user-attachments/assets/fecc2ea2-a722-42ff-b2ad-52a5c7843077" />

### Backend Architecture
```
├── Controllers/     
├── Models/         
└── Data/           
```

**Key Design Decisions:**
- **Direct Controller → DbContext** calls for maximum performance
- **Smart defaults** in models to reduce user errors and app communication breakdowns
- **Clean separation of concerns** without over-engineering

## API Reference

### Sample JSON Request
```json
{
  "customerId": "JOHN",
  "amount": 222.22
}
```

### Endpoints
- `POST /api/payments/create` - Create new payment
- `GET /api/payments/get/{id}` - Get payment by ID  
- `GET /api/payments/getall` - Get all payments
- `POST /api/payments/edit` - Update existing payment
- `DELETE /api/payments/delete/{id}` - Delete payment

## User Interface Showcase

### Landing Page
Clean, intuitive interface displaying all payments
<img width="689" height="686" alt="Landing_Page" src="https://github.com/user-attachments/assets/07e37356-ee0f-4994-b833-756ce7c16889" />

### Payment Confirmation
Successful payment confirmation for John
<img width="687" height="664" alt="JOHN_Confirmed" src="https://github.com/user-attachments/assets/5ce90dd7-85c1-4b58-b660-c4776d9be534" />

### Input Validation
**Missing Amount Field:**
<img width="683" height="679" alt="MARY_No_Amount" src="https://github.com/user-attachments/assets/876e5e11-a73a-4f04-aa0d-06462eac51db" />

**Invalid Amount Format:**
<img width="685" height="683" alt="MARY_Wrong Amnt" src="https://github.com/user-attachments/assets/57dac568-5359-4f0b-b15b-ec0832c50f27" />

### Successful Operations
**Adding New Payment:**
<img width="686" height="790" alt="MARY_Added" src="https://github.com/user-attachments/assets/a6408e24-aa26-4f3b-abff-027b6bb35446" />

**Payment Status Update:**
<img width="683" height="744" alt="MARY_Confirmed" src="https://github.com/user-attachments/assets/731411b2-2535-4a7e-8404-38e215ca3219" />
