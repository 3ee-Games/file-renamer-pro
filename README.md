# 🗂 File Renamer Pro
## CLI renaming utility

Cleans up file names by renaming within a given path.  Requires [dotnet](https://docs.microsoft.com/en-us/dotnet/core/install/) to run cross platform.

![File Renamer Pro](https://github.com/3ee-Games/file-renamer-pro/blob/main/docs/screenshot.png) 

- Rename all files within a directory and choose between four filter levels of renaming:
    - Remove all spaces
    - Remove all spaces and parentheses (default)
    - Remove all spaces and special characters
    - Remove all numbers
    - Rename all files as GUIDs
- Prepend a name before the file is renamed.
- Append a name at the end of the filename.

### Usage:

| Command | Description |
|---------|-------------|
| `file-renamer-pro -p {path}` | Sets a given path.  If not, you'll be able to input a path |
| `file-renamer-pro -f {0, 1, 2, 3, 4}` | Sets a renaming filter
| `file-renamer-pro -n {name}` | Prepends a given name to the file |
| `file-renamer-pro -a {name}` | Appends a given name to the file |
| `file-renamer-pro --keep-case` | This override flag will keep the case of the names of the files |

#### Basic usage with defaults:

`file-renamer-pro -p c:\myfolder\assets`

Renames all files under myfolder\assets and uses the default renaming filtering.

#### Set a renaming filter:

`file-renamer-pro -f 2 -p c:\myfolder\assets`

Renames all files under myfolder\assets and renames the files by removing all special characters.

| Filter Command | Description |
|----------------|-------------|
| `file-renamer-pro -f 0` | Removes all spaces |
| `file-renamer-pro -f 1` | Removes all spaces and parentheses (default).
| `file-renamer-pro -f 2` | Removes all spaces and special characters
| `file-renamer-pro -f 3` | Removes all numbers |
| `file-renamer-pro -f 4` | Renames all files as GUIDs |

#### Prepend a name:

`file-renamer-pro -f 3 -n hi_ -p c:\myfolder\assets`

Renames all files under myfolder\assets and renames the files by removing all numbers.  All filenames are prepended with "hi_". (hi_testImage.png)

#### Append a name:

`file-renamer-pro -f 3 -a _bye -p c:\myfolder\assets`

Renames all files under myfolder\assets and renames the files by removing all numbers.  All filenames are appended with "_bye". (testImage_bye.png)

### Debugging in VS:Code

Within launch.json, change internalConsole to integratedTerminal:
`"console": "integratedTerminal"`
