using DSharpPlus;
using DSharpPlus.CommandsNext;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace KrakenBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        private const string jsonPath = "config.json";

        public async Task RunAsync()
        {
            JsonConfig jsonConfig = GetJsonConfig().Result;
            AddDiscordConfig(jsonConfig);
            Client.Ready += ClientReady;
            AddCommandsConfig(jsonConfig);

            await Client.ConnectAsync();

            await Task.Delay(-1);    //Prevents early quit from bot
        }

        /// <summary>
        /// Gets the JsonConfig located at $jsonPath
        /// </summary>
        /// <returns>The deserialized JsonConfig</returns>
        private static async Task<JsonConfig> GetJsonConfig()
        {
            string json = string.Empty;

            using (FileStream fs = File.OpenRead(jsonPath))
            using (StreamReader sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<JsonConfig>(json);
        }

        /// <summary>
        /// Sets $Client to a new DiscordClient with a new DicordConfiguration
        /// </summary>
        /// <param name="jsonConfig">The JsonConfig containing the token of the bot</param>
        private void AddDiscordConfig(JsonConfig jsonConfig)
        {
            DiscordConfiguration config = new DiscordConfiguration
            {
                Token = jsonConfig.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(config);
        }

        /// <summary>
        /// Adds a new CommandsNextConfigurations to $Client and assigns it to $Commands
        /// </summary>
        /// <param name="jsonConfig">The JsonConfig containing the prefix to use for bot commands</param>
        private void AddCommandsConfig(JsonConfig jsonConfig)
        {
            CommandsNextConfiguration commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { jsonConfig.Prefix },
                EnableMentionPrefix = true,
                EnableDms = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);
        }

        private Task ClientReady(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
