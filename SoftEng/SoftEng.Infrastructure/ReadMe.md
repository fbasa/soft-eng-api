## Infrastructure
Infra should not be accessible inside Application layer. Application and Domain can be reference in here.

* **Implementation internal, interface public.**
  `IStudentRepository` (in **Application**) is public; `StudentRepository` (in **Infrastructure**) is `internal`. Presentation only sees the interface.

* **DI registration happens inside Infrastructure.**
  As long as you register the internal type **from the Infrastructure assembly** (e.g., in `AddInfrastructure`), the compiler sees it and the container can construct it.

* **Constructor visibility rule.**
  Microsoft’s DI needs a **public constructor** to activate the type. The class may be `internal`, but its constructor should be `public` (within the assembly). If you make the constructor non-public, activation will fail.

Quick template:

```csharp
// Application
public interface IStudentRepository
{
    Task<Student?> GetByIdAsync(Guid id);
}

// Infrastructure
internal sealed class StudentRepository(IDapperBaseservice dapper) : IStudentRepository
{
    public async Task<Student?> GetByIdAsync(Guid id) =>
        await dapper.QuerySingleOrDefaultAsync<Student>(
            "select * from Students where Id = @id", new { id });
}

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration cfg)
    {
   
       //Dapper service and connection factory has internal implem
       services.AddScoped<IDapperBaseService, DapperBaseService>();
       services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();

     // Register internal implem from within the Infrastructure project
       services.AddScoped<IStudentRepository, StudentRepository>();

        return services;
    }
}

// Presentation (Program.cs)
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
```

Tips & guardrails:

* Keep infra-only helpers (`IDapperBaseService, ISqlConnectionFactory`, etc.) **internal** too.
* Do **not** try to reference `StudentRepository` from Presentation; call only `AddInfrastructure(...)`.


With this setup you avoid exposing Dapper (or any infra detail) to Presentation/Application while keeping everything resolvable by DI.
