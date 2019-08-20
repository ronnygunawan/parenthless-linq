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
              where Skip(page * pageSize)
              where Take(pageSize)
              group e by ToList into g
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

## Skip(n)
Excludes first n items from sequence.
```csharp
var skipped = from i in items
              where Skip(3)
              select i;
```

## Take(n)
Includes only up to n first items from sequence.
```csharp
var taken = from i in items
            where Take(10)
            select i;
```

## Distinct
Removes duplicate items from sequence.
```csharp
var unique = from i in items
             orderby Distinct
             select i;
```

## Reverse
Reverses sequence.
```csharp
var lastThree = from i in items
                orderby Reverse
                where take(3)
                select i;
```

## ToList
Automatically converts return value to List<T>.
```csharp
var list = from i in items
           group i by ToList into g
           select g; // returns a List<T>
```

## ToHashSet
Automatically converts return value to HashSet<T>.
```csharp
var set = from i in items
          orderby distinct
          group i by ToHashSet into g
          select g; // returns a HashSet<T>
```

## First
Automatically converts return value to first item in sequence. Throws exception if sequence is empty.
```csharp
var first = from i in items
            group i by First into g
            select g;
```

## FirstOrDefault
Automatically converts return value to first item in sequence. Returns default value if sequence is empty.
```csharp
var first = from i in items
            group i by FirstOrDefault into g
            select g;
```

## Last
Automatically converts return value to last item in sequence. Throws exception if sequence is empty.
```csharp
var last = from i in items
           group i by Last into g
           select g;
```

## LastOrDefault
Automatically converts return value to last item in sequence. Returns default value if sequence is empty.
```csharp
var last = from i in items
            group i by LastOrDefault into g
            select g;
```

## Single
Automatically converts return value to the only item in sequence. Throws exception if sequence doesn't contain exactly one item.
```csharp
var value = from i in items
            group i by Single into g
            select g;
```

## SingleOrDefault
Automatically converts return value to the only item in sequence. Returns default value if sequence is empty, throws exception if sequence contains more than one item.
```csharp
var value = from i in items
            group i by SingleOrDefault into g
            select g;
```

## Any
Coverts sequence to true if sequence is not empty; to false otherwise.
```csharp
var isNotEmpty = from i in items
                 group i by Any into g
                 select g;
```
