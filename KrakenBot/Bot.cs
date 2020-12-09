using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KrakenBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            DiscordConfiguration config = new DiscordConfiguration
            {

            };

            Client = new DiscordClient(config);

            Client.Ready += ClientReady;

            CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration
            {

            };

            Commands = Client.UseCommandsNext(commandsConfig);

            await Client.ConnectAsync();

            await Task.Delay(1);    //Prevents early quit from bot
        }

        private Task ClientReady(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            return null;
        }
    }
}
