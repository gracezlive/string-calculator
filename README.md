# string-calculator

String Calculator is a console application that reads a string of delimited numbers and applies a math operator on them. Unlike the traditional calculator applications, this one makes integration easier by accepting inputs through command-line. When run from command prompt, it also gives user feedback in colors.

Another luxurious feature String Calculator has is its ability to support multiple versions of a string parser. During implementation, a developer can easily switch versions by modifying the Configuration class. To ensure quality, there are unit tests targetting every version of string parser based on features released with that version.

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
Add a string of delimited non-negative numbers: (available in parser v4 and up)
```sh
dotnet stringcalculator.dll -dn
```
```sh
dotnet stringcalculator.dll -denyNegative
```

### Features

Each version of the string parser has additional features than its predecessor.

| VERSION | FEATURES (ACCUMULATIVE) |
| ------ | ------ |
|    1    | Supports a maximum of 2 numbers using a comma delimiter |
|    2    | Supports an unlimited number of numbers |
|    3    | Supports a newline character as an alternative delimiter |
|    4    | Supports a property to deny negative numbers |
