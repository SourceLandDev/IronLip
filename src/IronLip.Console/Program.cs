using System.CommandLine;

RootCommand rootCommand = new();

Command cacheCommand = new("cache", "Inspect and manage lip's cache.");
Command configCommand = new("config", "Manage configuration.");
Command installCommand = new("install", "Install a tooth.");
Command listCommand = new("list", "List installed teeth.");
Command showCommand = new("show", "Show information about installed teeth.");
Command toothCommand = new("tooth", "Maintain a tooth.");
Command uninstallCommand = new("uninstall", "Uninstall a tooth.");

Option<bool> verboseOption = new(["-v", "--verbose"], "Show verbose output.");
Option<bool> quietOption = new(["-q", "--quiet"], "Show only errors.");

Command purgeCacheCommand = new("purge", "Clear the cache.");
cacheCommand.AddCommand(purgeCacheCommand);

rootCommand.AddCommand(cacheCommand);
rootCommand.AddCommand(configCommand);
rootCommand.AddCommand(installCommand);
rootCommand.AddCommand(listCommand);
rootCommand.AddCommand(showCommand);
rootCommand.AddCommand(toothCommand);
rootCommand.AddCommand(uninstallCommand);

rootCommand.AddOption(verboseOption);
rootCommand.AddOption(quietOption);

rootCommand.SetHandler(async (verbose, quiet) =>
{
    // TODO
}, verboseOption, quietOption);

await rootCommand.InvokeAsync(args);