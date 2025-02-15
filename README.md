🚀 WITMW - What Is This Made With?
Detect What Programming Language or Compiler Was Used to Create a File or Executable!

🔍 WITMW (What Is This Made With?) is a powerful open-source tool that analyzes files and executables (.exe, .dll, .so, .py, .js, etc.) to detect the programming language, compiler, or runtime used to build them.

✨ Features
✅ Detects Various Programming Languages

C#, C++, Rust, Go, Python, Java, Electron (JavaScript/Node.js), Delphi, PHP, and more!
✅ Identifies Compilers & Build Tools
GCC, MSVC, MinGW, Rust (Cargo), Go (Golang), PyInstaller, .NET CLR, and more!
✅ Scans Individual Files or Entire Directories
Analyze single files or scan an entire folder for multiple detections.
✅ Real-Time Loading Animation
Smooth loading effects while processing files.
✅ Explains Why It Thinks a File Was Made with a Certain Compiler
Provides reasoning behind the detected language/compiler.
✅ Lightweight Standalone .exe
No installation required! Just run the .exe.
✅ Works on Windows
Compatible with Windows 10/11 and built with .NET 8.
📥 Installation & Usage
1️⃣ Download & Run
Download the latest release
Extract the .zip and run WITMW.exe
2️⃣ Command Line Usage
You can run WITMW from the command line:

sh
Copy
Edit
WITMW.exe
Then enter the file or directory path to scan.

💻 Example Output
mathematica
Copy
Edit
========================================
|    🔍 WITMW - What Is This Made With?  |
========================================

📂 Enter the file or directory path: C:\Users\Bob\Downloads\MyApp.exe

🔹 [1/1] Analyzing: MyApp.exe
⏳ Processing | 
⏳ Processing / 
⏳ Processing - 
⏳ Processing \ 
✅ Processing Complete!
📌 Detected: ✅ C# / .NET Application
   (Detected via .NET CLR header and assembly metadata)
🔧 Supported File Types
File Type	Detection
.exe	Detects compiled Windows executables (.NET, C++, Rust, Go, Python, etc.)
.dll	Identifies possible .NET, C++, Rust, and Golang libraries
.py	Detects Python scripts and compiled .pyc files
.js	Identifies JavaScript and Node.js projects
.so	Analyzes Linux shared object binaries
.log/.txt	Ignores log and text files
.pdb	Recognizes debug symbols from Visual Studio
.json	Detects Electron-based applications
📜 How It Works
Extracts PE headers & metadata from Windows executables (.exe, .dll)
Scans for compiler/runtime signatures (GCC, MSVC, Rust, Go, Delphi, .NET, Python, Node.js)
Checks embedded strings & debug information for further detection
Uses a smart algorithm to explain the reasoning behind its decision
📌 Roadmap
🚀 Planned Features:

🏗️ Linux & macOS Support
🌍 Multi-language output support
🔬 Advanced disassembly & deeper binary analysis
📦 Support for more programming languages
📊 JSON output for automation & scripting
🤝 Contribute
WITMW is an open-source project! Contributions, feedback, and feature suggestions are welcome!

🔧 Want to help improve WITMW? Fork the repository and submit a Pull Request!

📄 License
📜 MIT License – Free for personal and commercial use.

🔗 Links
🔗 GitHub Repository: https://github.com/your-repo-name
📢 Report Issues: GitHub Issues
🚀 Download Releases: Latest Releases

🎉 Ready to Find Out What Your Software is Made With?
🔥 Download WITMW now and start analyzing! 🚀

📢 Let Me Know If You Want Any Modifications! 😊🚀
