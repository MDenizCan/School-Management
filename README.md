# 🎓 School Management API
My second project that i developed during my internship

A RESTful Web API built with **.NET 8** and **Clean Architecture** for managing students, teachers, subjects, and grades.

## 🏗️ Architecture

```
SchoolManagement/
├── School.API            → Controllers, Middleware, DI Configuration
├── School.Application    → Business Logic, Interfaces, AutoMapper Profiles
├── School.Domain         → Entities, Base Classes
├── School.Infrastructure → EF Core DbContext, Repository Implementations
└── School.Contracts      → DTOs (Data Transfer Objects)
```

**Dependency Flow:**

```
API → Application → Domain
API → Contracts
Application → Contracts
Infrastructure → Application → Domain
```

## 🛠️ Tech Stack

- **.NET 8** Web API
- **Entity Framework Core** with **SQLite**
- **AutoMapper** for object mapping
- **Swagger / Swashbuckle** for API documentation

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Run

```bash
cd School.API
dotnet run
```

Swagger UI: [http://localhost:5017/swagger](http://localhost:5017/swagger)

## 📡 API Endpoints

### Students

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/students` | Get all students |
| GET | `/api/students/paged?page=1&pageSize=10` | Get students with pagination |
| GET | `/api/students/{id}` | Get student by ID |
| POST | `/api/students` | Create a new student |
| PUT | `/api/students/{id}` | Update a student |
| DELETE | `/api/students/{id}` | Delete a student |

### Subjects

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/subjects` | Get all subjects |
| GET | `/api/subjects/paged?page=1&pageSize=10` | Get subjects with pagination |
| GET | `/api/subjects/{id}` | Get subject by ID |
| POST | `/api/subjects` | Create a new subject |
| PUT | `/api/subjects/{id}` | Update a subject |
| DELETE | `/api/subjects/{id}` | Delete a subject |

### Teachers

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/teacher` | Get all teachers (with subject info) |
| GET | `/api/teacher/paged?page=1&pageSize=10` | Get teachers with pagination |
| GET | `/api/teacher/{id}` | Get teacher by ID |
| POST | `/api/teacher` | Create a new teacher |
| PUT | `/api/teacher/{id}` | Update a teacher |
| DELETE | `/api/teacher/{id}` | Delete a teacher |

### Grades

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/grades` | Get all grades (with student/teacher details) |
| GET | `/api/grades/paged?page=1&pageSize=10` | Get grades with pagination |
| GET | `/api/grades/{id}` | Get grade by ID |
| POST | `/api/grades` | Create a new grade |
| PUT | `/api/grades/{id}` | Update a grade |
| DELETE | `/api/grades/{id}` | Delete a grade |
| GET | `/api/grades/transcript/{studentId}` | Get student transcript (per-subject averages) |

## 📦 Sample Requests

### Create a Subject

```json
POST /api/subjects
{
  "name": "Mathematics"
}
```

### Create a Teacher

```json
POST /api/teacher
{
  "firstName": "Ahmet",
  "lastName": "Yılmaz",
  "subjectId": 1
}
```

### Create a Student

```json
POST /api/students
{
  "firstName": "Ali",
  "lastName": "Kaya",
  "studentNumber": "STD001"
}
```

### Create a Grade

```json
POST /api/grades
{
  "studentId": 1,
  "teacherId": 1,
  "examName": "Midterm",
  "score": 85.5,
  "examDate": "2026-02-20T00:00:00"
}
```

### Pagination Response

```json
GET /api/students/paged?page=1&pageSize=2

{
  "items": [...],
  "totalCount": 5,
  "page": 1,
  "pageSize": 2,
  "totalPages": 3,
  "hasPrevious": false,
  "hasNext": true
}
```

### Transcript Response

```json
GET /api/grades/transcript/1

{
  "studentId": 1,
  "studentFullName": "Ali Kaya",
  "subjectAverages": [
    { "subjectName": "Mathematics", "average": 82.5 },
    { "subjectName": "Physics", "average": 90.0 }
  ]
}
```

## ⚙️ Key Features

- **Generic Repository Pattern** — Reusable CRUD operations for all entities
- **Specialized Repositories** — `TeacherRepository` and `GradeRepository` with EF Core `Include` for navigation properties
- **AutoMapper** — Automatic Entity ↔ DTO mapping
- **Global Exception Middleware** — Centralized error handling (`404`, `400`, `409`, `500`)
- **Pagination** — `PagedResultDto<T>` with metadata (totalCount, totalPages, hasNext, hasPrevious)
- **DB-Side Aggregation** — Transcript averages calculated via SQL (`GroupBy` + `Average`)
- **Unique Constraints** — Composite unique index on `(StudentId, TeacherId, ExamName)` prevents duplicate grades

## 📁 Project Structure Details

| Layer | Responsibility |
|-------|---------------|
| **Domain** | Entity definitions (`Student`, `Teacher`, `Subject`, `Grade`) and `BaseEntity` |
| **Contracts** | DTOs for API input/output — keeps entities hidden from external consumers |
| **Application** | Service interfaces, service implementations, AutoMapper profiles, repository interfaces |
| **Infrastructure** | EF Core `AppDbContext`, `GenericRepository<T>`, specialized repositories, migrations |
| **API** | Controllers, `ExceptionMiddleware`, DI registration (`Program.cs`) |
=======
