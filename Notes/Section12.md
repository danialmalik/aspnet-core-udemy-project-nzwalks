# Consuming web API

- Create a new UI project

```sh
dotnet create --name NZWalks.UI
```

- Run command to add code generator in the new project
```
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
```

- Create controller for new project

```sh
dotnet aspnet-codegenerator -p NZWalks.UI view Views/RegionsController/Index Empty
```
