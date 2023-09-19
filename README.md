# Task-Management
TASK MANAGEMENT API
Entities:
Users
Notification
Poject
Task

Project Architecture:
The project structure was designed using clean Architecture which consist of four layers(Presentation, Core, Application, Infrastructure)
Database:
The Database used in this project is the Microsoft SQL Server and the connection string is located in the App settings in the Presentation Layer of the project.
TO connect with the database, please run migration in the package manager console (Tools -> Nuget package Manager -> Package manager console) and run the following commands below.
1) Add-Migration {nameOfMigration}
2) Update-Database

The Notification Entity consist of Notification Type which is an ENUM and starts count from one instead of the default zero
None = 1,
Due_Date_Reminder = 2,
Status_Update = 3

The Task Entity contains Priority and status which are both Enums as demonstrated below:
PRIORITY:
low = 1,
high = 2,
medium = 3

STATUS:
pending = 1,
in_progress = 2,
completed = 3

The background services to notify the users of newly added task were demonstrated using console so when the application is running, the notifications log to the console




