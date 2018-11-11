namespace FileContentQuery.Core
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using FileContentQuery.Models;

    public class DiskIoOperation
    {
        private QueryParameters _parameters;
        public DiskIoOperation(QueryParameters parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException(nameof(parameters));

            if (String.IsNullOrWhiteSpace(parameters.Directory))
                throw new ArgumentNullException(nameof(parameters.Directory));

            if (String.IsNullOrWhiteSpace(parameters.Match))
                throw new ArgumentNullException(nameof(parameters.Match));

            this._parameters = parameters;
        }

        public void Print()
        {
            this.Print(new DirectoryInfo(this._parameters.Directory));
        }

        private void Print(DirectoryInfo directory)
        {
            if (!directory.Exists) return;

            directory.GetDirectories().ToList().ForEach(this.Print);
            directory.GetFiles().ToList().ForEach(this.Print);
        }
        private void Print(FileInfo file)
        {
            if (!String.IsNullOrWhiteSpace(this._parameters.Include) && !Regex.IsMatch(file.FullName, this._parameters.Include)) return;
            if (!String.IsNullOrWhiteSpace(this._parameters.Exclude) && Regex.IsMatch(file.FullName, this._parameters.Exclude)) return;

            if (file.Length > 10 * 1024 * 1024) return;

            String[] lines = File.ReadAllLines(file.FullName);
            int index = 1;
            foreach (String line in lines)
            {
                if (line.Contains(this._parameters.Match) || Regex.IsMatch(line, this._parameters.Match))
                {
                    Console.WriteLine($"[{file.FullName}#{index}] {line}");
                }
                index++;
            }
        }
    }
}