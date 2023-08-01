# Section 2: Create new ASP.NET Core Web API & Domain Models

Contains linux alternative commands to visual studio operations.



- Create a new "Web API" project

```sh
 dotnet new webapi [--name <project_name>]
 ```

- Run project

```sh
dotnet run --launch-profile {http|https} --project NZwalks
```

Open swagger at http://localhost:5292/swagger/index.html

The address could change depending on the port used by the project.

- Add a new controller

Add this tool to scafold controllers etc using CLI:
```sh
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

Then add a new API controller using
```sh
dotnet aspnet-codegenerator -p NZWalks.csproj controller -name StudentsController   -api
```

Add packages for Entity Framework
```sh
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

- Create a model class.

_No CLI command found for this operation. Create a file manually :D_


## Creating DB

Instead of installing MS Server on linux, lets just use docker.

There is a docker-compose file added to the project. Run it using

```sh
docker-compose up
```

Connections string would be something like:

`Server=localhost,1433;Database=NZWalksDb;Trusted_Connection=False;TrustServerCertificate=True;User Id=YOUR_USERNAME;password=YOUR_PASSWORD`

`TrustServerCertificate`  is required to connect to localhost DB.
`Trusted_Connection=False` is required to connect to Docker container DB.


- Add migrations

Run inside the `NZWalks` project directory.
```sh
dotnet ef migrations add "Initial Migration"
```

- Run migrations

```sh
dotnet ef database update
```
