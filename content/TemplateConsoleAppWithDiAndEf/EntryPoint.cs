using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TemplateConsoleAppWithDiAndEf
{
    public class EntryPoint : IHost
    {
        private Dictionary<string, Func<string[], Task>> commandRegistry = new();
        private readonly IHost _abstractHost;

        public EntryPoint()
        {
            _abstractHost = Host.CreateDefaultBuilder().Build();
            commandRegistry.Add("hi", (v) => Console.Out.WriteLineAsync("Hello!"));
        }

        public void Dispose()
        {
            _abstractHost.Dispose();
        }

        public async Task StartAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _abstractHost.StartAsync(cancellationToken);
            await Run();
        }

        public async Task StopAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await _abstractHost.StopAsync(cancellationToken);
            await Console.Out.WriteLineAsync("Stoped");
        }

        public IServiceProvider Services { get; }

        private async Task Run()
        {
            string input;
            do
            {
                input = Console.ReadLine();
                var tokens = input.Split(' ');
                var command = tokens[0];
                if (commandRegistry.ContainsKey(command))
                {
                    commandRegistry[command]?.Invoke(tokens.Skip(1).ToArray());
                }
                else
                {
                    Console.WriteLine($"Wrong usage. Available commands are: {string.Join(Environment.NewLine, commandRegistry.Keys)} \n Enter q - for Exit.");
                }
            } while (input != "q");

        }
    }
}