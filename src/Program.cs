using System.Diagnostics;

namespace CowsayConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("What does the cow say?: ");
            string? userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
            {
                Console.WriteLine("No input :<");
                return;
            }

            string cowsayOutput = await RunCowsayAsync(userInput);

            Console.WriteLine(cowsayOutput);
        }

        static async Task<string> RunCowsayAsync(string message)
        {
            string output = string.Empty;

            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "cowsay",
                    Arguments = message,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = psi })
                {
                    process.Start();

                    output = await process.StandardOutput.ReadToEndAsync();

                    await process.WaitForExitAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while running cowsay: {ex.Message}");
            }

            return output;
        }
    }
}
