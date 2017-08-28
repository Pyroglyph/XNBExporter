<!---
Someone please help me format this better :(
--->

# XNB Exporter (xnbe)
> XNBE can be pronounced "zen-bee"

XNB Exporter (not to be confused with other, simpler XNB exporters) is a command line utility which converts XNB files back to their original formats. It's aim is to enable asset decompilation for all the formats that both XNA amd MonoGame support.

## Why use XNBE?
I've seen a few XNB decompilers around but they only seemed to decompile images which is very simple. XNBE supports more than that. Some of these decompilers also seem quite low-effort so I decided to create my own. Quality and a good user experience is important to me as a developer so I try and make things practical but also nice to use.

## Supported Formats
- Images (output as PNG)
- Audio (output as WAV)

## Usage
### `xnbe <file or directory> [output directory]`
XNBE will convert either single files or entire directories of files. It will automatically skip over any non-XNB files if a directory is given. If an output directory is specified, all output files will be placed there. If not, decompiled files will be placed in the same folder as their source files. Files with the same names as new output files will be replaced (more information in the Warnings section).

## Warnings
- Upon decompilation, new files will replaces ones with the same name. For example: If you had a folder with file.xnb and file.png from a previous decompilation (or just by coincidence) and you decompiled file.xnb again, file.png would be replaced without warning.
- If you pass any unsupported XNB files into XNBE, I can't guarantee what will happen. The chances are that XNBE will just crash anyway.