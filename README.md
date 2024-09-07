# bhank-customer-service

```bash
bhank-customer-service/

.
└── src/
    ├── Bhank.Customer.Api/
    │   ├── Application/
    │   │   ├── DTOs
    │   │   ├── Interfaces
    │   │   └── UseCases
    │   ├── Domain/
    │   │   ├── Entities
    │   │   ├── Exceptions
    │   │   ├── Interfaces/
    │   │   │   └── Repositories
    │   │   └── Services
    │   ├── Infra/
    │   │   ├── Configurations
    │   │   ├── Migrations
    │   │   ├── Repositories
    │   │   └── CustomerContext.cs
    │   ├── appsettings.json
    │   ├── Customer.csproj
    │   └── Program.cs
    └── Bhank.Customer.Api.Tests/
├── .gitignore
├── README.md
└── LICENSE
```

## API Endpoints

### 1. Create Customer

- **URL:** `/Customer`
- **Method:** `POST`
- **Description:** This endpoint creates a new customer in the system.

### 2. Get Customer By Id

- **URL:** `/Customer`
- **Method:** `GET`
- **Parameters:** `clientId : string (UUID)`
- **Description:** This endpoint retrieves a customer in the system by a unique ID.

### 3. Create Address

- **URL:** `/Address`
- **Method:** `POST`
- **Description:** This endpoint creates a new address in the system.

### 4. Get Address By Id

- **URL:** `/Address`
- **Method:** `GET`
- **Parameters:** `addressId : string (UUID)`
- **Description:** This endpoint retrieves a address in the system by a unique ID.
