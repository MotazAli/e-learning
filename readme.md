# Configure Database (Database First)

Configure Database using (Database First) approduc there is two cases

case 1 - configure database first time when create project
case 2 - update your code DB if the database already configured before and the database has some changes

## Case 1 - Configure database first time

1- add those packages to your project dependances
* Microsoft.EntityFrameworkCore.SqlServer
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.Tools

2- download the dotnet EF Tool globaly using this command
```bash
dotnet tool install --global dotnet-ef
```

3- execute the Scaffold command with adding your connection string of your database and chose the output directory folder name
```bash
Scaffold-DbContext -Connection ["your connection string"] Microsoft.EntityFrameworkCore.SqlServer -OutputDir [out put folder name] -force
```
Example - here I chosed Models as an output directory 
```bash
Scaffold-DbContext -Connection "Server=.\SQLEXPRESS;Database=Aspire;User Id=sa;password=*****;Trusted_Connection=True;MultipleActiveResultSets=true;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
```


4- chnage the connection string of your context class to be mapped to the name of app settings connection string - steps:
* open your out put folder (for past example is Model folder) and open your context class (for past example is AspireContext)
* go to method OnConfiguring() and change the UseSqlServer from connection string to the name of the connection string in the app setting 

example 
change code from
```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Aspire;User Id=sa;password=*****;Trusted_Connection=True;MultipleActiveResultSets=true;");
            }
        }
```
to

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ELearningDB");
            }
        }
```

5- adding the dependancy of the DbContext in the program.cs
```csharp
services.AddDbContext<AspireContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("ELearningDB")));
```



## Case 2 - Update your code for any changes happend in Database

this case if the database already configured before and the database has some changes and need to update your models classes 

just execute that command with your database name in the app setting
```bash
Scaffold-DbContext -Connection Name=[app setting DB name]  Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
```

example 
```bash
Scaffold-DbContext -Connection Name=ELearningDB  Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
```





