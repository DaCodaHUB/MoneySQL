# MoneySQL

MoneySQL is a Windows desktop money-management application built with C#,
WPF, .NET Framework, and MySQL. It lets each user maintain a running balance,
record categorized expenses, and review spending through charts.

> **Project status:** This is an older educational project and is not ready for
> production use. Database and SMTP credentials are currently stored in source
> code. Replace and rotate them before running or sharing the application.

## Features

- Create an account and sign in with a hashed password
- Recover an account through an email verification flow
- Add income and categorized expenses
- Display the current balance and an expense history table
- Select and delete expense records
- Visualize expenses by category, monthly totals, and previous-month daily spending
- Validate usernames, email addresses, passwords, and monetary input

## Repository layout

The repository contains two Visual Studio solutions:

| Path | Description |
| --- | --- |
| `Banker/Banker.sln` | Current WPF implementation; use this solution for development |
| `moneyManage/moneyManage.sln` | Earlier Windows Forms prototype kept for reference |

The main WPF application's source is organized as follows:

```text
Banker/Banker/
|-- Charts/       LiveCharts controls and chart calculations
|-- Database/     MySQL access, password hashing, email, and helpers
|-- Domain/       WPF validation rules and password binding behavior
|-- UI/           Sign-in, sign-up, reset, balance, expense, and chart windows
|-- App.xaml      Startup and shared Material Design resources
`-- packages.config
```

## Technology

- C# and .NET Framework 4.6.1
- WPF with MaterialDesignThemes
- MySQL using MySql.Data 8.0.11
- LiveCharts 0.9.7
- NuGet `packages.config` dependency management

## Prerequisites

- Windows
- Visual Studio 2017 or newer with the **.NET desktop development** workload
- .NET Framework 4.6.1 Developer Pack
- A reachable MySQL server
- NuGet package restore enabled in Visual Studio
- An SMTP account if the password-reset email flow is required

## Getting started

1. Clone the repository.
2. Open `Banker/Banker.sln` in Visual Studio.
3. Restore the NuGet packages when prompted, or select **Tools > NuGet Package Manager > Restore NuGet Packages**.
4. Create the MySQL tables described below.
5. Replace the connection string in `Banker/Banker/Database/SqlConnect.cs` with credentials for your database.
6. For password-reset email, replace the SMTP sender and credentials in `Banker/Banker/Database/MailCode.cs`.
7. Set `Banker` as the startup project and run it with `F5`.

The startup window is the sign-in screen. New users can open **Sign Up**.
Successful sign-in passes the database user ID to the money-management window,
which uses it to keep records separated by user.

## Database setup

The application expects a schema containing `User`, `Expense`, and `Total`
tables. The following definition matches the fields used by the current code:

```sql
CREATE DATABASE money_sql;
USE money_sql;

CREATE TABLE `User` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `username` VARCHAR(100) NOT NULL,
    `password` VARCHAR(255) NOT NULL,
    `email` VARCHAR(255) NOT NULL,
    `Timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `resetTimes` INT NOT NULL DEFAULT 0,
    PRIMARY KEY (`id`),
    UNIQUE KEY `uq_user_username` (`username`),
    UNIQUE KEY `uq_user_email` (`email`)
);

CREATE TABLE `Expense` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `Uid` INT NOT NULL,
    `Category` VARCHAR(100) NOT NULL,
    `$` DECIMAL(12,2) NOT NULL,
    `Timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    CONSTRAINT `fk_expense_user`
        FOREIGN KEY (`Uid`) REFERENCES `User` (`id`) ON DELETE CASCADE
);

CREATE TABLE `Total` (
    `id` INT NOT NULL AUTO_INCREMENT,
    `Uid` INT NOT NULL,
    `$` DECIMAL(12,2) NOT NULL,
    `Timestamp` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (`id`),
    CONSTRAINT `fk_total_user`
        FOREIGN KEY (`Uid`) REFERENCES `User` (`id`) ON DELETE CASCADE
);
```

If you use a different database name, host, port, or account, update the
connection string accordingly. The unusual `$` column name is retained because
the existing SQL queries refer to it directly.

## Configuration and security

The project currently has no external configuration layer. Before use:

- Move the MySQL connection string out of `SqlConnect.cs` into ignored local configuration, environment variables, or a secrets manager.
- Move the SMTP username and password out of `MailCode.cs` in the same way.
- Rotate the credentials already present in the repository; treat them as compromised.
- Use a least-privilege database account and TLS for remote connections.

Password values are salted and hashed by `SecurePasswordHasher`, and most of
the current WPF database operations use query parameters. This does not make
the hard-coded service credentials safe.

## Known limitations

- There are no automated tests or database migrations.
- The app depends on a live MySQL connection and handles connection failures inconsistently.
- Configuration is embedded in source rather than loaded per environment.
- The password-reset implementation uses legacy SMTP APIs and should be reviewed before deployment.
- The Windows Forms solution is a prototype with older database code and should not be used as the production entry point.

## Contributing

Open the WPF solution, keep changes focused in the existing `UI`, `Domain`,
`Database`, or `Charts` areas, and verify sign-in, income, expense, deletion,
and chart flows against a test database. Do not commit real credentials or
personal financial data.
