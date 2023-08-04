# Section 8: Authentication & Authorization

- Add these packages:

```sh
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.IdentityModel.Tokens
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
```


- For `Issuer` in `appsettings.json`->`Jwt`, get the value for `applicationUrl` from `Properties/launchSettings.json`

- Since there are two DB context classes, we need to tell the app which one to use. To create migration:

```sh
dotnet ef migrations add "Seed data for roles" --context NZWalksAuthDbContext
dotnet ef database update --context NZWalksAuthDbContext
```
