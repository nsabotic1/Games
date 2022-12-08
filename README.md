# Role-playing game
-------------------------
This application allows the user to add characters and give them certain skills and weapons. Also, characters can fight each other. Before that, the user must register. Then, when he logs in, he will receive a token that serves for authentication. After that, the user can access all CRUD operations for characters.

## Run and build

* Install .NET 6 SDK.
* Install the following packages:
  * Microsoft.EntityFrameworkCore (7.0.0)
  * Microsoft.EntityFrameworkCore.Tools (7.0.0)
  * Microsoft.EntityFrameworkCore.Design (7.0.0)
  * Microsoft.EntityFrameworkCore.SqlServer (7.0.0)
  * AutoMapper (12.0.0)
  * AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.0)
  * Microsoft.AspNetCore.Authentication.JwtBearer (6.0.11)
  * Swashbuckle.AspNetCore (6.4.0)
  * Swashbuckle.AspNetCore.Filters (7.0.6)
* Run `update-database` for a database to be made
