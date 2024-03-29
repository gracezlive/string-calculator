# string-calculator

String Calculator is a console application that reads a string of delimited numbers and applies a math operator on them. Unlike the traditional calculator applications, this one makes integration easier by accepting inputs through command-line. When run from command prompt, it also gives user feedback in colors.

A bonus feature String Calculator has is its ability to support multiple versions of a string parser. During implementation, a developer can easily switch versions by modifying the Configuration class. To ensure quality, there are unit tests targetting every version of string parser based on features released with that version.

### Usage

Add a string of comma delimited numbers:
```sh
dotnet stringcalculator.dll
```
Subtract a string of comma delimited numbers:
```sh
dotnet stringcalculator.dll -op=-
dotnet stringcalculator.dll --operator=-
```
Multiply a string of comma delimited numbers:
```sh
dotnet stringcalculator.dll -op=*
dotnet stringcalculator.dll --operator=*
```
Divide a string of comma delimited numbers:
```sh
dotnet stringcalculator.dll -op=/
dotnet stringcalculator.dll --operator=/
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
dotnet stringcalculator.dll --denyNegative
```
Add a string of delimited numbers not exceeding an upper bound: (available in parser v5 and up)
```sh
dotnet stringcalculator.dll -ub=<integer>
```
```sh
dotnet stringcalculator.dll --upperBound=<integer>
```
Define a single-character inline delimiter with number input: (available in parser v6 and up)
```sh
dotnet stringcalculator.dll
Please enter numbers: //#,1,2#3,4
```
```sh
dotnet stringcalculator.dll "\n"
Please enter numbers: //;\n2;5
```
Define an inline delimiter of any length with number input: (available in parser v7 and up)
```sh
dotnet stringcalculator.dll
Please enter numbers: //[abc],1abc2,3abc4
```
```sh
dotnet stringcalculator.dll "\n"
Please enter numbers: //[***]\n11***22***33
```
Define multiple inline delimiters of any length with number input: (available in parser v8 and up)
```sh
dotnet stringcalculator.dll
Please enter numbers: //[abc][##],1abc2##3abc4
```
```sh
dotnet stringcalculator.dll "\n"
Please enter numbers: //[*][!!][r9r]\n11r9r22*33!!44
```

### Features

Each version of the string parser has additional features compared to its predecessor.

| VERSION | FEATURES (ACCUMULATIVE) |
| ------ | ------ |
|    1    | Supports a maximum of 2 numbers using a comma delimiter |
|    2    | Supports an unlimited number of numbers |
|    3    | Supports a newline character as an alternative delimiter |
|    4    | Supports a property to deny negative numbers |
|    5    | Supports a property to define the upper bound |
|    6    | Supports a single-character inline delimiter definition |
|    7    | Supports an inline delimiter of any length |
|    8    | Supports multiple inline delimiters of any length |