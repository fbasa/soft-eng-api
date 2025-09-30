## Application
Application layer contains business logic and types.  
Use-case workflows and policies that are specific to this system/bounded context.  

Here’s how that maps to **logic** and **types**.

## Application layer (business logic & types)

System-specific orchestration: commands/queries, use cases, transactions, integration ports. It coordinates domain objects but shouldn’t contain low-level tech concerns.

**Examples of application *types***

* `PlaceOrderCommand`, `EnrollStudentCommand`
* Handlers/use cases like `PlaceOrderHandler`
* Ports/interfaces: `IOrderRepository`, `IPaymentGateway`, `INotificationService`
* DTOs/view models used by this app’s use cases

**Examples of application *logic***

* Workflows and policies specific to this system:

  * “A student can enroll only if the section has seats and it’s before the deadline.”
  * “Place order -> reserve stock -> take payment -> emit `OrderPlaced`.”
  * “If order total ≥ ₱5,000, require manager approval.”
* Transaction boundaries and retries for this app
* Mapping in/out of DTOs specific to this app

**Tiny example**

```csharp
public sealed record PlaceOrderCommand(Guid CustomerId, IReadOnlyList<OrderLineDto> Lines);

public sealed class PlaceOrderHandler(IOrderRepository orders, IPaymentGateway payments)
{
    public async Task<Guid> Handle(PlaceOrderCommand cmd, CancellationToken ct)
    {
        var order = Order.Create(cmd.CustomerId);          // Domain entity
        foreach (var line in cmd.Lines)
            order.AddLine(ProductId.From(line.ProductId), Money.From(line.Price, line.Currency), line.Qty);

        order.ValidateBusinessRules();                     // Domain invariants
        await orders.SaveAsync(order, ct);                 // Port (interface)

        await payments.ChargeAsync(order.Id, order.Total); // Port (interface)
        return order.Id;
    }
}
```

Notice the **Application** layer orchestrates the steps and talks to **ports**; the money math and invariants live in **Domain**.

# Quick litmus tests

* **Could another system reuse this without change?** Put it in **Domain**.
* **Is it a use case/workflow for this app only?** Put it in **Application**.
* **Does it touch DB, web, files, Dapper/EF?** That’s **Infrastructure** (implementing app ports).

This separation keeps core concepts stable and reusable (Domain) while letting each system compose them into its own workflows (Application).
