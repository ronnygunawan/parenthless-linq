# Parenthless
Write Skip, Take, Distinct, ToList, ToHashSet, ToDictionary, etc. fluently in LINQ statement without extra parentheses.

Instead of writing this:
```csharp
var paged = (from e in employees
             where e.JoinDate.Year >= 2018
             orderby e.Name ascending
             select e).Skip(page * pageSize)
                      .Take(pageSize)
                      .ToList();
```

you can write it without parentheses like this:
```csharp
var paged = from e in employees
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
<PackageReference Include="Parenthless" Version="1.0.3" />
```

Add using directive
```csharp
using Parenthless;
using static Parenthless.Linq; // important alias to make all the clauses work
```

# Clauses

## OfType&lt;T&gt;()
Filters the elements of the sequence based on a specified type.
```csharp
var filtered = from i in items
               where OfType<int>()
               select i;
```

## Skip(n)
Excludes first n elements from sequence.
```csharp
var skipped = from i in items
              where Skip(3)
              select i;
```

## Take(n)
Includes only up to n first elements from sequence.
```csharp
var taken = from i in items
            where Take(10)
            select i;
```

## SkipLast(n)
Excludes last n elements from sequence.
```csharp
var skipped = from i in items
              where SkipLast(3)
              select i;
```

## TakeLast(n)
Includes only up to n last elements from sequence.
```csharp
var taken = from i in items
            where TakeLast(10)
            select i;
```

## SkipWhile(condition)
Excludes elements from sequence as long as condition is true.
```csharp
var trimmed = from c in str
              where SkipWhile(char.IsWhiteSpace(c))
              select c;
```

## TakeWhile(condition)
Includes elements from sequence as long as condition is true.
``` csharp
var word = from c in str
           where SkipWhile(char.IsWhiteSpace(c))
           where TakeWhile(char.IsLetterOrDigit(c))
           select c;
```

## DefaultIfEmpty
Fills sequence with one default value if sequence is empty.
```csharp
var neverEmpty = from i in items
                 where DefaultIfEmpty
                 select i;
```

## Concat(sequence)
Concatenates second sequence to the end of current sequence.
```csharp
var concat = from i in items
             where Concat(moreItems)
             select i;
```

## Union(sequence)
Produces set union of current sequence and second sequence.
```csharp
var union = from i in items
            where Union(moreItems)
            select i;
```

## Except(sequence)
Produces set difference of current sequence and second sequence.
```csharp
var difference = from i in items
                 where Except(excludedItems)
                 select i;
```

## Intersect(sequence)
Produces set intersection of current sequence and second sequence.
```csharp
var commonItems = from i in items
                  where Intersect(otherSequence)
                  select i;
```

## Distinct
Removes duplicate elements from sequence.
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
Automatically converts return value to List&lt;T&gt;.
```csharp
var list = from i in items
           group i by ToList into g
           select g; // returns a List<T>
```

## ToArray
Automatically converts return value to T[].
```csharp
var array = from i in items
            group i by ToArray into g
            select g; // returns a T[]
```

## ToHashSet
Automatically converts return value to HashSet&lt;T&gt;.
```csharp
var set = from i in items
          orderby distinct
          group i by ToHashSet into g
          select g; // returns a HashSet<T>
```

## ToDictionary(key)
Automatically converts return value to Dictionary&lt;TKey, TValue&gt;.
```csharp
var ageById = from person in people
              group person.Age by ToDictionary(person.Id) into dict
              select dict;
```

## First
Automatically converts return value to first element in sequence. Throws exception if sequence is empty.
```csharp
var first = from i in items
            group i by First into g
            select g;
```

## FirstOrDefault
Automatically converts return value to first element in sequence. Returns default value if sequence is empty.
```csharp
var first = from i in items
            group i by FirstOrDefault into g
            select g;
```

## Last
Automatically converts return value to last element in sequence. Throws exception if sequence is empty.
```csharp
var last = from i in items
           group i by Last into g
           select g;
```

## LastOrDefault
Automatically converts return value to last element in sequence. Returns default value if sequence is empty.
```csharp
var last = from i in items
            group i by LastOrDefault into g
            select g;
```

## Single
Automatically converts return value to the only element in sequence. Throws exception if sequence doesn't contain exactly one element.
```csharp
var value = from i in items
            group i by Single into g
            select g;
```

## SingleOrDefault
Automatically converts return value to the only element in sequence. Returns default value if sequence is empty, throws exception if sequence contains more than one element.
```csharp
var value = from i in items
            group i by SingleOrDefault into g
            select g;
```

## Any()
Converts sequence to true if sequence is not empty; to false otherwise.
```csharp
var isNotEmpty = from i in items
                 group i by Any() into g
                 select g;
```

## Any(condition)
Converts sequence to true if any element satisfy the condition; to false otherwise.
```csharp
var hasZero = from i in items
              group i by Any(i == 0) into g
              select g;
```

## All(condition)
Converts sequence to true if all elements satisfy the condition; to false otherwise.
```csharp
var hasNoZero = from i in items
                group i by All(i != 0) into g
                select g;
```

## Count()
Converts sequence to number of elements.
```csharp
var count = from i in items
            group i by Count() into g
            select g;
```

## Count(condition)
Converts sequence to a number that represents how many elements in the sequence satisfy the condition.
```csharp
var countOfOddNumbers = from i in items
                        group i by Count(i % 2 == 1) into g
                        select g;
```

## Max
Converts sequence to maximum value in the sequence.
```csharp
var max = from i in items
          group i by Max into g
          select g;
```

## Min
Converts sequence to minimum value in the sequence.
```csharp
var min = from i in items
          group i by Min into g
          select g;
```

## Sum
Converts sequence to sum value of the sequence.
```csharp
var sum = from i in items
          group i by Sum into g
          select g;
```

## Average
Converts sequence to average value of the sequence.
```csharp
var avg = from i in items
          group i by Average into g
          select g;
```

## StringJoin(separator)
Concatenates sequence using specified separator.
```csharp
var str = from i in items
          group i by StringJoin(", ") into g
          select g;
```

## Contains(value)
Converts sequence to true if it contains specified value; to false otherwise.
```csharp
var containsZero = from i in items
                   group i by Contains(0) into g
                   select g;
```

## ContainsAny(values)
Converts sequence to true if it contains any of specified values; to false otherwise.
```csharp
var containsTwoOrFive = from i in items
                        group i by ContainsAny(2, 5) into g
                        select g;
```

## ContainsAll(values)
Converts sequence to true if it contains all of specified values; to false otherwise.
```csharp
var containsTwoAndFive = from i in items
                         group i by ContainsAll(2, 5) into g
                         select g;
```

## SequenceEqual(sequence)
Converts sequence to true if current sequence equals second sequence; to false otherwise.
```csharp
var equals = from i in items
             group i by SequenceEqual(expectedItems) into g
             select g;
```