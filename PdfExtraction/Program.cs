using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo("pdfinfo.exe", "01.pdf");
            processStartInfo.CreateNoWindow = true;
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            Process process = Process.Start(processStartInfo);
            string info = process.StandardOutput.ReadToEnd();
            Console.WriteLine(info);
            Console.Read();
        }
    }
}
