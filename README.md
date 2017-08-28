<!---
Someone please help me format this better :(
--->

# XNB Exporter (xnbe)
> XNBE can be pronounced "zen-bee"

XNB Exporter (not to be confused with other, simpler XNB exporters) is a command line utility which converts XNB files back to their original formats. It's aim is to enable asset decompilation for all the formats that both XNA amd MonoGame support.

## Why use XNBE?
I've seen a few XNB decompilers around but they only seemed to do XNB to PNG (which is very simple). XNBE supports more than just simple image conversion. Some of these decompilers also seem quite low-effort so I decided to create my own. Quality and a good user experience is important to me as a developer so I try and make things practical but also nice to use.

## Supported Formats
- Images (output as PNG)
- Audio (output as WAV)

## Usage
### `xnbe <file>`

XNBE will convert the file and place the output in the same folder. Support for custom destination folders is in the works. Expect it soon.

Before:

```
-file.xnb
```

After:

```
-file.xnb
-file.png
```

### `xnbe <directory>`

XNBE will convert all the files in the given directory (non-recursive, but support is planned) and place them in the same directory.

Before:

```
-directory
|-subdirectory
||-file3.xnb
||-file4.xnb
|-file1.xnb
|-file2.xnb
```

After:

```
-directory
|-subdirectory
||-file3.xnb
||-file4.xnb
|-file1.xnb
|-file1.png
|-file2.xnb
|-file2.png
```