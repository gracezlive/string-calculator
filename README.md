# string-calculator

String Calculator is a console application that reads a string of delimited numbers and applies a math operator on them.

### Syntax

Add a string of comma delimited numbers:
```sh
dotnet stringcalculator.dll
```
Add a string of comma or any specified delimiter delimited numbers: (available in parser v3 and up)
```sh
dotnet stringcalculator.dll "\n"
```
```sh
dotnet stringcalculator.dll "\n" "|" "#"
```

### Parsers
| VERSION | FEATURES |
| ------ | ------ |
| 1 | Supports a maximum of 2 numbers using a comma delimiter |
| 2 | Supports an unlimited number of numbers |
| 3 | Support a newline character as an alternative delimiter |