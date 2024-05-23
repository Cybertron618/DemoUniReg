# DemoUniReg API

## Description
DemoUniReg API is an ASP.NET Core application developed for managing student information in an educational institution.

## Features
- CRUD operations for Student Details and Student Management Information System (MIS)
- PostgreSQL database integration using Entity Framework Core
- Redis caching for improved performance
- API documentation with Swagger
- Error handling and HTTPS redirection configured

## Setup Instructions
1. Clone the repository: `git clone https://github.com/your/repository.git`
2. Navigate to the project directory: `cd DemoUniReg`
3. Restore dependencies: `dotnet restore`
4. Update database connection string in `appsettings.json`
5. Run migrations to create the database: `dotnet ef database update`
6. Start the application: `dotnet run`

## Endpoints
- GET `/api/studentdetail` - Get all student details
- GET `/api/studentdetail/{id}` - Get student detail by ID
- POST `/api/studentdetail` - Create a new student detail
- PUT `/api/studentdetail/{id}` - Update an existing student detail
- DELETE `/api/studentdetail/{id}` - Delete a student detail

- GET `/api/studentmis` - Get all student MIS
- GET `/api/studentmis/{id}` - Get student MIS by ID
- POST `/api/studentmis` - Create a new student MIS
- PUT `/api/studentmis/{id}` - Update an existing student MIS
- DELETE `/api/studentmis/{id}` - Delete a student MIS

## Technologies Used
- ASP.NET Core
- C#
- Entity Framework Core
- PostgreSQL
- Redis
- Swagger

## License
This project is licensed under the [MIT License](LICENSE).

## Author
Prince Kwakye(https://github.com/Cybertron618)
