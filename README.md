# 🗂 File Renamer Pro
## A CLI renaming utility

Cleans up file names by renaming within a given path.

- Rename all files within a directory and choose between four filter levels of renaming:
    - Remove all spaces
    - Remove all spaces and parentheses (default)
    - Remove all spaces and special characters
    - Remove all numbers
- Prepend a name before the file is renamed.

### Usage:

#### Set a path:

`-p: path to files to be renamed.`

example: -p c:\myfolder\assets

#### Set a renaming filter:

`-p c:\myfolder\assets -f 2`

Renames all files under myfolder\assets and renames the files by removing all special characters.

#### Prepend a name:

`-n: name to prepend to filename.`

example: '-n hi_ will result in: hi_filename.txt.

### Debugging in VS:Code

Within launch.json, change internalConsole to integratedTerminal:
`"console": "integratedTerminal"`
