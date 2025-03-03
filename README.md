# Simple Care

## Run locally

```bash
make
```

## Prerequisites

### .NET 8.0
See <https://dotnet.microsoft.com/en-us/download>

### EF Core Tools

```bash
dotnet tool install --global dotnet-ef
```

Run this command in the infrastructure project to build the database
```
dotnet ef database update
```


### GNU make

Install on Windows:

```powershell
# https://winget.run/pkg/GnuWin32/Make
winget install -e --id GnuWin32.Make
```

### Docker

Install on Windows:

```powershell
# https://winget.run/pkg/Docker/DockerDesktop
winget install -e --id Docker.DockerDesktop
```
