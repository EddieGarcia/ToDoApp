# ToDoApp
Simple Todo App WPF + NHibernate (Fluent) + MSSQL 2019

## ER diagram:

![diagram](https://user-images.githubusercontent.com/8282513/147873909-3faf228e-9f68-4580-a8fc-69c4279a3a1d.png)

## How to run it
1. Adjust connection string in App.config
2. Run it

OR

1. Open executable

## Dependencies
- FluentNHibernate
- NHibernate
- System.Data.SqlClient

# Concepts
https://www.red-gate.com/simple-talk/development/dotnet-development/some-nhibernate-best-practices/

## Fluent mappings over xml mappings
XML mappings vs. Fluent mappings vs. NHibernate 'Mapping by code'
https://www.tutorialspoint.com/nhibernate/nhibernate_fluent_hibernate.htm
https://stackoverflow.com/questions/13480543/difference-between-fluentnhibernate-and-nhibernates-mapping-by-code

## NHibernate session management
Recommended approach - session per presenter. Each form should have its own session
https://docs.microsoft.com/en-us/archive/msdn-magazine/2009/december/data-access-building-a-desktop-to-do-application-with-nhibernate
https://stackoverflow.com/questions/2013467/what-should-be-the-lifetime-of-an-nhibernate-session

## Implicit transactions
Dont use implicit transactions
https://hibernatingrhinos.com/products/nhprof/learn/#DoNotUseImplicitTransactions

