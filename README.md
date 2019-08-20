# Parenthless
Write skip, take, distinct, reverse, ToList, ToHashSet fluently in LINQ statement without extra parentheses.

Instead of writing this:
```csharp
var pageTwo = (from e in employees
               where e.JoinDate.Year >= 2018
               orderby e.Name ascending
               select e).Skip(page * pageSize)
                        .Take(pageSize)
                        .ToList();
```

you can write it without parentheses like this:
```csharp
var pageTwo = from e in employees
              where e.JoinDate.Year >= 2018
              orderby e.Name ascending
              where skip(page * pageSize)
              where take(pageSize)
              group e by list into g
              select g;
```

# Installation
From Package Manager Console
```
Install-Package Parenthless
```

From .NET CLI
```
dotnet add package Parenthless
```

Using PackageReference
```xml
<PackageReference Include="Parenthless" Version="1.0.0" />
```

Add using directive
```csharp
using Parenthless;
using static Parenthless.Linq; // important alias to make all the clauses work
```

# Clauses

## skip(n)
Excludes first n items from IEnumerable
```csharp
var skipped = from i in items
              where skip(3)
              select i;
```

## take(n)
Includes only up to n first items from IEnumerable.
```csharp
var taken = from i in items
            where take(10)
            select i;
```

## distinct
Removes duplicate items from IEnumerable.
```csharp
var unique = from i in items
             orderby distinct
             select i;
```

## reverse
Reverses IEnumerable.
```csharp
var lastThree = from i in items
                orderby reverse
                where take(3)
                select i;
```

## list
Automatically converts return value to List<T>.
```csharp
var list = from i in items
           group i by list into g
           select g; // returns a List<T>
```

## hashset
Automatically converts return value to HashSet<T>.
```csharp
var set = from i in items
          orderby distinct
          group i by hashset into g
          select g; // returns a HashSet<T>
```
