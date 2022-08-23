# TradeCategoryQuestion

This repo was created to share the code with evaluators and anybody who want to contribute.
The design pattern used was _Chain of Responsibility_ in the algorithm to order categories and its validations.
The unit tests cover up to 95%, according to XUnit coverage.cobertura.xml file and HTML report, generated from these commands:
```sh
dotnet restore
cd .\TradeCategoryTest\
dotnet test --collect:"XPlat Code Coverage"
dotnet tool install -g dotnet-reportgenerator-globaltool
reportgenerator -reports:".\TradeCategoryTest\TestResults\{guid}\coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html
```
Access _coveragereport_ folder and open _index.html_ file.