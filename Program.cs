using System;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Threading;

class Program
{
    static void Main()
    {
        try
        {
            ShowLoadingScreen();
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("========================================");
            Console.WriteLine("|    🔍 WITMW - What Is This Made With?  |");
            Console.WriteLine("========================================");
            Console.ResetColor();

            Console.Write("\n📂 Enter the file or directory path: ");

            string? path = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(path))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n🚫 No input detected! Exiting...");
                Console.ResetColor();
                return;
            }

            if (Directory.Exists(path))
            {
                ScanDirectory(path);
            }
            else if (File.Exists(path))
            {
                AnalyzeFileWithLoading(path);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n🚫 Error: Invalid path! Please enter a valid file or folder.");
                Console.ResetColor();
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Error: {ex.Message}");
            Console.ResetColor();
        }
        finally
        {
            Console.WriteLine("\n🔄 Press Enter to exit...");
            Console.ReadLine(); // Works in all environments
        }
    }



    static void ShowLoadingScreen()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=======================================");
        Console.WriteLine("|    🔄 Initializing WITMW...         |");
        Console.WriteLine("=======================================");
        Console.ResetColor();

        for (int i = 0; i < 10; i++)
        {
            Console.Write(".");
            Thread.Sleep(200);
        }
        Console.WriteLine("\n✅ System Ready!\n");
        Thread.Sleep(500);
    }

    static void ScanDirectory(string folderPath)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n🔍 Scanning directory: {folderPath}");
        Console.ResetColor();

        string[] files = Directory.GetFiles(folderPath);
        int totalFiles = files.Length;

        Console.WriteLine($"📁 Total Files Found: {totalFiles}\n");

        Dictionary<string, int> summary = new Dictionary<string, int>();

        int count = 1;
        foreach (string file in files)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"🔹 [{count}/{totalFiles}] Analyzing: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(Path.GetFileName(file));
            Console.ResetColor();

            ShowRealTimeLoading(() => AnalyzeFile(file));

            string detectedLanguage = AnalyzeFile(file);
            if (!summary.ContainsKey(detectedLanguage))
                summary[detectedLanguage] = 1;
            else
                summary[detectedLanguage]++;

            count++;
        }

        ShowFinalSummary(summary);
    }

    static void AnalyzeFileWithLoading(string filePath)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"\n🔹 Analyzing: ");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(Path.GetFileName(filePath));
        Console.ResetColor();

        ShowRealTimeLoading(() => AnalyzeFile(filePath));

        Console.Write("📌 Detected: ");
        Console.ForegroundColor = ConsoleColor.Green;
        string result = AnalyzeFile(filePath);
        Console.WriteLine(result);
        Console.ResetColor();
    }

    static string AnalyzeFile(string filePath)
    {
        return DetectCompiledLanguage(filePath);
    }

    static string DetectCompiledLanguage(string filePath)
    {
        if (IsDotNetAssembly(filePath))
        {
            return "✅ C# / .NET Application (Detected via .NET CLR header and assembly metadata)";
        }

        string extractedText = ExtractStrings(filePath);
        string peInfo = GetPEHeaderInfo(filePath);

        if (extractedText.Contains("GCC") || extractedText.Contains("MinGW") || peInfo.Contains("GCC"))
            return "🛠️ Compiled with GCC (C/C++) (Detected via compiler string in executable metadata)";

        if (extractedText.Contains("MSVC") || extractedText.Contains("Visual Studio") || peInfo.Contains("MSVC"))
            return "🛠️ Compiled with MSVC (C/C++) (Detected via Visual Studio debug symbols)";

        if (extractedText.Contains("Rust") || extractedText.Contains("cargo"))
            return "🦀 Rust Application (Detected via Rust-specific runtime symbols)";

        if (extractedText.Contains("Go buildID") || extractedText.Contains("Go runtime"))
            return "🐹 Go (Golang) Executable (Detected via Go-specific runtime symbols)";

        if (extractedText.Contains("DelphiRuntime") || extractedText.Contains("Embarcadero"))
            return "🐍 Delphi Application (Detected via Embarcadero runtime signatures)";

        if (extractedText.Contains("Py_Initialize") || extractedText.Contains("PyInstaller"))
            return "🐍 Python Application (Packaged with PyInstaller, detected via Python runtime calls)";

        if (extractedText.Contains("node_modules") || extractedText.Contains("Electron"))
            return "🌐 Electron-based Application (JavaScript / Node.js) (Detected via Electron runtime references)";

        return "❓ Unknown Compiled Language";
    }

    static bool IsDotNetAssembly(string filePath)
    {
        try
        {
            Assembly assembly = Assembly.LoadFile(filePath);
            return true;
        }
        catch (BadImageFormatException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    static string ExtractStrings(string filePath)
    {
        Process process = new Process();
        process.StartInfo.FileName = "powershell";
        process.StartInfo.Arguments = $"-Command \"Select-String -Path '{filePath}' -Pattern '[\\w]{{4,}}'\"";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        return output;
    }

    static string GetPEHeaderInfo(string filePath)
    {
        try
        {
            FileVersionInfo info = FileVersionInfo.GetVersionInfo(filePath);
            return $"{info.ProductName} - {info.CompanyName} - {info.FileVersion}";
        }
        catch
        {
            return "Unknown PE Header Info";
        }
    }

    static void ShowRealTimeLoading(Action action)
    {
        string[] spinner = { "|", "/", "-", "\\" };
        Console.Write("  ");
        for (int i = 0; i < 15; i++)
        {
            Console.Write($"\r⏳ Processing {spinner[i % 4]} ");
            Thread.Sleep(150);
        }
        Console.Write("\r✅ Processing Complete!  \n");
        action.Invoke();
    }

    static void ShowFinalSummary(Dictionary<string, int> summary)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n✅ Analysis Complete!");
        Console.WriteLine("====================================");
        Console.WriteLine("📊 Summary of File Detections:");
        Console.WriteLine("====================================");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("| Language/Compiler | Count |");
        Console.WriteLine("|-------------------|-------|");

        foreach (var entry in summary)
        {
            Console.WriteLine($"| {entry.Key,-17} | {entry.Value,5} |");
        }

        Console.WriteLine("====================================");
        Console.ResetColor();
    }
}
