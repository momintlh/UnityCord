using UnityEditor;
using UnityEditor.PackageManager;
using System.Diagnostics;
using System.IO;

[InitializeOnLoad]
public static class UnityCordNpmInstaller
{
    static UnityCordNpmInstaller()
    {
        Events.registeredPackages += OnPackagesChanged;
    }

    private static void OnPackagesChanged(PackageRegistrationEventArgs args)
    {
        foreach (var added in args.added)
        {
            if (added.name == "com.momintlh.unitycord")
            {
                RunNpmInstall();
            }
        }
    }

    static void RunNpmInstall()
    {
        string editorPath = Path.Combine("Packages", "com.momintlh.unitycord", "Editor");
        string packageJson = Path.Combine(editorPath, "package.json");

        if (!File.Exists(packageJson))
        {
            UnityEngine.Debug.Log("No package.json found. Skipping npm install.");
            return;
        }

        var process = new Process();
        process.StartInfo.FileName = "npm";
        process.StartInfo.Arguments = "install";
        process.StartInfo.WorkingDirectory = editorPath;
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;

        try
        {
            process.Start();
            UnityEngine.Debug.Log("Running npm install...");
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode == 0)
                UnityEngine.Debug.Log("npm install completed:\n" + output);
            else
                UnityEngine.Debug.LogError("npm install failed:\n" + error);
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError("Failed to run npm install: " + ex.Message);
        }
    }
}