# ASP Corenet (7.0) web api using Entity Framework

**Course link:** https://accelerate.udemy.com/course/build-rest-apis-with-aspnet-core-web-api-entity-framework/


- Create a new "Web API" project

```sh
 dotnet new webapi [--name <project_name>]
 ```

- Run project

```sh
dotnet run --launch-profile {http|https}
```

Open swagger at http://localhost:5292/swagger/index.html

The address could change depending on the port used by the project.

- Add a new controller

**For linux CLI**

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

- Create a model class using CLI
```sh
```
