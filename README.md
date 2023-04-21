# 🗂 File Renamer Pro
## Command-Line Interface (CLI) File Renaming Tool

Easily rename files within a specified path, providing cleaner and more organized file names. To run this cross-platform utility, you'll need to install [dotnet](https://docs.microsoft.com/en-us/dotnet/core/install/).

![File Renamer Pro](https://github.com/3ee-Games/file-renamer-pro/blob/main/docs/screenshot.png)

- Rename all files within a directory, choosing from five available renaming filters:
    - Remove all spaces
    - Remove all spaces and parentheses (default)
    - Remove all spaces and special characters
    - Remove all numbers
    - Rename all files as GUIDs
- Prepend a custom text to the file name.
- Append a custom text to the file name.

### Usage:

| Command | Description |
|---------|-------------|
| `file-renamer-pro -p {path}` | Specify a path; if not provided, you'll be prompted to enter one |
| `file-renamer-pro -f {0, 1, 2, 3, 4}` | Select a renaming filter |
| `file-renamer-pro -n {name}` | Prepend a custom text to the file name |
| `file-renamer-pro -a {name}` | Append a custom text to the file name |
| `file-renamer-pro --keep-case` | Use this flag to maintain the original file name's case |

#### Basic Usage with Default Settings:

`file-renamer-pro -p c:\myfolder\assets`

Renames all files within `myfolder\assets` using the default renaming filter.

#### Specifying a Renaming Filter:

`file-renamer-pro -f 2 -p c:\myfolder\assets`

Renames all files within `myfolder\assets`, removing all special characters.

| Filter Command | Description |
|----------------|-------------|
| `file-renamer-pro -f 0` | Removes all spaces |
| `file-renamer-pro -f 1` | Removes all spaces and parentheses (default) |
| `file-renamer-pro -f 2` | Removes all spaces and special characters |
| `file-renamer-pro -f 3` | Removes all numbers |
| `file-renamer-pro -f 4` | Renames all files as GUIDs |

#### Prepend Custom Text:

`file-renamer-pro -f 3 -n hi_ -p c:\myfolder\assets`

Renames all files within `myfolder\assets`, removing all numbers. All file names are prepended with "hi_" (e.g., hi_testImage.png).

#### Append Custom Text:

`file-renamer-pro -f 3 -a _bye -p c:\myfolder\assets`

Renames all files within `myfolder\assets`, removing all numbers. All file names are appended with "_bye" (e.g., testImage_bye.png).

### Debugging in Visual Studio Code (VSCode):

In `launch.json`, change the `internalConsole` to `integratedTerminal`:
`"console": "integratedTerminal"`
