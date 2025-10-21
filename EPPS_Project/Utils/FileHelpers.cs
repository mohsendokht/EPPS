using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductionPlanning.Utils
{
    internal static class FileHelpers
    {
        /// <summary>
        /// Returns a list of file names in the specified directory.
        /// </summary>
        public static List<string> GetFileNames(string directoryPath, bool includeSubdirectories = false, string searchPattern = "*.*", bool returnFullPath = false)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentException("directoryPath must be provided", nameof(directoryPath));

            if (!Directory.Exists(directoryPath))
                throw new DirectoryNotFoundException($"Directory not found: {directoryPath}");

            var option = includeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            try
            {
                var files = Directory.EnumerateFiles(directoryPath, searchPattern, option);
                return returnFullPath ? files.ToList() : files.Select(Path.GetFileName).ToList();
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new IOException($"Access denied enumerating files in '{directoryPath}'", ex);
            }
            catch (PathTooLongException ex)
            {
                throw new IOException($"Path too long while enumerating files in '{directoryPath}'", ex);
            }
        }

        /// <summary>
        /// Safe, non-throwing variant. Returns true and sets fileNames on success; logs and returns false on failure.
        /// </summary>
        public static bool TryGetFileNames(string directoryPath, out List<string> fileNames, bool includeSubdirectories = false, string searchPattern = "*.*", bool returnFullPath = false)
        {
            fileNames = new List<string>();

            try
            {
                fileNames = GetFileNames(directoryPath, includeSubdirectories, searchPattern, returnFullPath);
                return true;
            }
            catch (ArgumentException ex)
            {
                Logger.SaveError("FileHelpers.TryGetFileNames", ex.Message);
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                Logger.SaveError("FileHelpers.TryGetFileNames", ex.Message);
                return false;
            }
            catch (IOException ex)
            {
                Logger.SaveError("FileHelpers.TryGetFileNames", ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                // Catch-all: log unexpected errors
                Logger.SaveError("FileHelpers.TryGetFileNames", ex.Message);
                return false;
            }
        }
    }
}