# 🗂 File Renamer Pro
## A CLI file renaming utility by 3ee Games
### Have a bunch of assets but they all use different naming conventions (or none at all)?  Look no further!

Cleans up file names by renaming within a given path.

- Rename all files within a directory.
- Prepend a name before the file is renamed.

### Examples:
`-p: path to files to be renamed.`
example: -p c:\myfolder\assets

`-n: name to prepend to filename.`
example: '-n hi_ will result in: hi_filename.txt.

### Deugging
Within launch.json, change internalConsole to integratedTerminal:
`"console": "integratedTerminal"`
