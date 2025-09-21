# Peer-graded-Assignment

## User Management API (.NET)

### Features
- CRUD operations for users (GET, POST, PUT, DELETE)
- Input validation (name required, valid email, no <script> tags)
- In-memory user storage (no database required)
- Auto-increment user ID
- Logging middleware (logs every request method and path)
- Authentication middleware (requires `X-Api-Key: my-secret-key` header)
- Swagger/OpenAPI UI for API testing and documentation

### Endpoints
- `GET /api/users` — Get all users
- `GET /api/users/{id}` — Get user by ID
- `POST /api/users` — Create a new user (JSON body: name, email)
- `PUT /api/users/{id}` — Update user (JSON body: name, email)
- `DELETE /api/users/{id}` — Delete user

### Validation
- Name is required and cannot be empty or whitespace
- Email must be valid
- No field can contain `<script>` tags (basic XSS protection)

### Authentication
- All endpoints require header: `X-Api-Key: my-secret-key`

### Logging
- Every request is logged (method and path)

### Swagger
- Swagger UI available at `/swagger` when running in development mode

### How to Run
1. Restore packages: `dotnet restore`
2. Build: `dotnet build`
3. Run: `dotnet run`
4. Test endpoints using Swagger UI or `crud.http`

### Example `crud.http`
```http
GET http://localhost:5000/api/users
X-Api-Key: my-secret-key

POST http://localhost:5000/api/users
Content-Type: application/json
X-Api-Key: my-secret-key

{
  "name": "Test User",
  "email": "test@example.com"
}
```