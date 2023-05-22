# 🗂 File Renamer Pro
## Command-Line Interface (CLI) File Renaming Tool

This project is a powerful file renaming tool written in C#. It's capable of renaming all files in a given directory according to user-specified criteria, including removing spaces, parentheses, special characters, and numbers. 

![File Renamer Pro](https://github.com/3ee-Games/file-renamer-pro/blob/main/docs/screenshot.png)

## Installation

This project requires .NET Core to run:

1. Download the .NET SDK (Software Development Kit): https://dotnet.microsoft.com/download

2. Install the .NET SDK by following the instructions specific to your operating system.

After .NET is installed, you can run this application using Visual Studio Code or any other IDE of your choice that supports .NET development.

## Usage

You can pass in arguments to the program as follows:

```shell
file-renamer-pro -p [path] -n [prepend] -a [append] -f [filter] --keep-case
```

Here is the explanation of the arguments:

- `-p [path]` : Path to the files to be renamed.
- `-n [prepend]` : Name to prepend to the filename.
- `-a [append]` : Name to append to the filename.
- `-f [filter]` : Amount of filtering used in renaming files: 
    - `0` = Removes only spaces
    - `1` = Removes spaces and parentheses (DEFAULT)
    - `2` = Removes spaces and all special characters
    - `3` = Removes all numbers
    - `4` = Generates a GUID as filename
- `--keep-case` : This flag will keep the case of the file name.

### Detailed Usage

| Command | Description |
|---------|-------------|
| `file-renamer-pro -p {path}` | Specify a path; if not provided, you'll be prompted to enter one |
| `file-renamer-pro -f {0, 1, 2, 3, 4}` | Select a renaming filter |
| `file-renamer-pro -n {name}` | Prepend a custom text to the file name |
| `file-renamer-pro -a {name}` | Append a custom text to the file name |
| `file-renamer-pro --keep-case` | Use this flag to maintain the original file name's case |

#### Basic Usage with Default Settings

```shell
file-renamer-pro -p c:\myfolder\assets
```

This command renames all files within `myfolder\assets` using the default renaming filter.

#### Specifying a Renaming Filter

```shell
file-renamer-pro -f 2 -p c:\myfolder\assets
```

This command renames all files within `myfolder\assets`, removing all special characters.

Here is what each filter level does:

| Filter Command | Description |
|----------------|-------------|
| `file-renamer-pro -f 0` | Removes all spaces |
| `file-renamer-pro -f 1` | Removes all spaces and parentheses (default) |
| `file-renamer-pro -f 2` | Removes all spaces and special characters |
| `file-renamer-pro -f 3` | Removes all numbers |
| `file-renamer-pro -f 4` | Renames all files as GUIDs |

#### Prepend Custom Text

```shell
file-renamer-pro -f 3 -n hi_ -p c:\myfolder\assets
```

This command renames all files within `myfolder\assets`, removing all numbers. All file names are prepended with "hi_" (e.g., hi_testImage.png).

#### Append Custom Text

```shell
file-renamer-pro -f 3 -a _bye -p c:\myfolder\assets
```

This command renames all files within `myfolder\assets`, removing all numbers. All file names are appended with "_bye" (e.g., testImage_bye.png).

## Running Tests

This project uses NUnit for testing. To run the tests, you'll first need to install the NUnit and Microsoft.NET.Test.Sdk NuGet packages:

1. Open a terminal and navigate to the directory where your test project is located.
2. Run the following commands:

```shell
dotnet add package NUnit
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package NUnit3TestAdapter
```

To run the tests, simply navigate to the root directory of the project in your terminal and execute the `dotnet test` command.

## Contributing

Contributions are welcome! Please feel free to submit a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.