# Dependency injection example

Implementation of dependency injection pattern with inversion of control, for education purpose.

## Requirements

* .NET Core 2.1

## Run tests

```bash
dotnet watch -p .\src\DependencyInjectionExample.UnitTests\ test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=./lcov.info
```

Or from VS Code, run test tasks.