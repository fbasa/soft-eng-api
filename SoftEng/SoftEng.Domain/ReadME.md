## Domain
The Domain layer contains enterprise logic and types.  
Core concepts and rules that are true across your whole company/enterprise (often across many systems).  

Here’s how that maps to **logic** and **types**.

## Domain layer (enterprise logic & types)

Reusable, pure, technology-agnostic, with strong invariants.

**Examples of domain *types* (Value Objects / Entities)**

* `Money`, `Currency`, `Percentage`
* `DateRange` (with `Overlaps`, `Contains`)
* `Email`, `PhoneNumber`, `Address`
* `CustomerId`, `TenantId`, `TaxId` (strongly typed IDs)
* Base DDD types like `Entity`, `AggregateRoot`, `DomainEvent`

**Examples of domain *logic***

* Arithmetic and validation that’s universally true:

  * `Money.Add/Subtract`, currency matching, rounding rules
  * `DateRange.Overlaps(other)`, `DateRange.Duration()`
  * `Email.Parse(string)`, `Address.Normalize()`
* Enterprise policies that apply everywhere in the org:

  * Company-wide tax/VAT rounding policy
  * Global credit limit rules (e.g., “credit limit cannot be negative”)
  * Common pricing primitives (e.g., percentage discounts never produce negative totals)

**Tiny example**

```csharp
public sealed record Currency(string Code);
public sealed record Money(decimal Amount, Currency Currency)
{
    public Money Add(Money other)
    {
        if (Currency != other.Currency) throw new CurrencyMismatchException();
        return this with { Amount = Amount + other.Amount };
    }
}

public sealed record DateRange(DateTime Start, DateTime End)
{
    public bool Overlaps(DateRange other) =>
        Start < other.End && other.Start < End;
}
```

These are useful in *any* system that handles prices or schedules.


# Quick litmus tests

* **Could another system reuse this without change?** Put it in **Domain**.
* **Is it a use case/workflow for this app only?** Put it in **Application**.
* **Does it touch DB, web, files, Dapper/EF?** That’s **Infrastructure** (implementing app ports).

This separation keeps core concepts stable and reusable (Domain) while letting each system compose them into its own workflows (Application).
