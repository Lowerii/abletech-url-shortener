# AbleTech URL Shortener
## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

## Getting started

```bash
# from the repository root
dotnet restore
dotnet run
```

The API listens on **http://localhost:5066** (see `Properties/launchSettings.json`).

The base URL used to build the shortened links is set by `BaseUrl` in `appsettings.json`. If you change the port or host, update it there too.

## Endpoints

| Method | Path          | Description                                         |
|--------|---------------|-----------------------------------------------------|
| POST   | `/url`        | Creates a shortened URL                             |
| GET    | `/url/{code}` | Returns the details of a shortened URL              |
| GET    | `/{code}`     | Redirects to the original URL                       |

### `POST /url`

Creates a new shortened URL.

**Request body**

```json
{
  "originalUrl": "https://www.example.com/some/very/long/path"
}
```

`originalUrl` is required and must be a valid absolute URL.

**Responses**

- `201 Created`

  ```json
  {
    "code": "aB3xY9k",
    "shortenedUrl": "http://localhost:5066/aB3xY9k",
    "originalUrl": "https://www.example.com/some/very/long/path"
  }
  ```

- `400 Bad Request`: `originalUrl` is missing or not a valid URL.

### `GET /url/{code}`

Returns the details of the shortened URL with the given code.

**Responses**

- `200 OK`: same body as the `POST /url` response.
- `404 Not Found`

  ```json
  { "status": 404, "message": "Invalid URL code provided" }
  ```

### `GET /{code}`

Redirects the client to the original URL linked to the code. This is the endpoint the `shortenedUrl` points to.

**Responses**

- `302 Found`: the `Location` header holds the original URL.
- `404 Not Found`: the code does not exist (same body as above).