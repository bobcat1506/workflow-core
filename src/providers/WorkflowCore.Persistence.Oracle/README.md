# SQL Server Persistence provider for Workflow Core

Provides support to persist workflows running on [Workflow Core](../../../README.md) to a SQL Server database.

## Installing

Install the NuGet package "WorkflowCore.Persistence.Oracle"

```
PM> Install-Package WorkflowCore.Persistence.Oracle
```

## Usage

Use the .UseOracle extension method when building your service provider.

```C#
services.AddWorkflow(x => x.UseOracle(@"Server=.;Database=WorkflowCore;Trusted_Connection=True;", true, true));
```
