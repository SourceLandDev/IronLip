using System.CommandLine;

#region Root command

RootCommand rootCommand = new();
Option<bool> verboseOption = new(["-v", "--verbose"], "Show verbose output.");
Option<bool> quietOption = new(["-q", "--quiet"], "Show only errors.");
rootCommand.AddOption(verboseOption);
rootCommand.AddOption(quietOption);

#endregion

#region Cache command

Command cacheCommand = new("cache", "Inspect and manage lip's cache.");
Command purgeCacheCommand = new("purge", "Remove all items from the cache.");
cacheCommand.AddCommand(purgeCacheCommand);
rootCommand.AddCommand(cacheCommand);

#endregion

#region Config command

Command configCommand = new("config", "Manage configuration.");
Argument<string?> keyConfigArgument = new("key", () => null, "Key of config.");
Argument<string?> valueConfigArgument = new("value", () => null, "Value of config.");
configCommand.AddArgument(keyConfigArgument);
configCommand.AddArgument(valueConfigArgument);
rootCommand.AddCommand(configCommand);

#endregion

#region Install command

Command installCommand = new("install", "Install a tooth.");
Argument<string[]> specifierInstallArgument = new("specifier", "Specifiers.");
Option<bool> upgradeInstallOption = new("--upgrade", "Upgrade the specified tooth to the newest available version.");
Option<bool> forceReinstallInstallOption =
    new("--force-reinstall", "Reinstall the tooth even if they are already up-to-date.");
Option<bool> yesInstallOption = new(["-y", "--yes"], "Assume yes to all prompts and run non-interactively.");
Option<bool> noDependenciesInstallOption =
    new("--no-dependencies", "Do not install dependencies. Also bypass prerequisite checks.");
installCommand.AddArgument(specifierInstallArgument);
installCommand.AddOption(upgradeInstallOption);
installCommand.AddOption(forceReinstallInstallOption);
installCommand.AddOption(yesInstallOption);
installCommand.AddOption(noDependenciesInstallOption);
rootCommand.AddCommand(installCommand);

#endregion

#region List command

Command listCommand = new("list", "List installed teeth.");
Option<bool> upgradableListOption = new("--upgradable", "List upgradable teeth.");
listCommand.AddOption(upgradableListOption);
rootCommand.AddCommand(listCommand);

#endregion

#region Show command

Command showCommand = new("show", "Show information about installed teeth.");
Argument<string> toothRepositoryUrlShowArgument = new("tooth repository URL", "Tooth repository URL.");
Option<bool> availableShowOption = new("--available", "Show the full list of available versions.");
showCommand.AddArgument(toothRepositoryUrlShowArgument);
showCommand.AddOption(availableShowOption);
rootCommand.AddCommand(showCommand);

#endregion

#region Tooth command

Command toothCommand = new("tooth", "Maintain a tooth.");
Command initToothCommand = new("init",
    "Initialize and writes a new tooth.json file in the current directory, in effect creating a new tooth rooted at the current directory.");
Command packToothCommand = new("pack", "Pack the tooth into a tooth archive.");
Argument<string> outputPathPackToothArgument = new("output path", "Output path.");
toothCommand.AddCommand(initToothCommand);
packToothCommand.AddArgument(outputPathPackToothArgument);
toothCommand.AddCommand(packToothCommand);
rootCommand.AddCommand(toothCommand);

#endregion

#region Uninstall command

Command uninstallCommand = new("uninstall", "Uninstall a tooth.");
Argument<string[]> toothRepositoryUrlUninstallArgument = new("tooth repository URL", "Tooth repository URL.");
uninstallCommand.AddArgument(toothRepositoryUrlUninstallArgument);
rootCommand.AddCommand(uninstallCommand);

#endregion

rootCommand.SetHandler(async (verbose, quiet) =>
{
    // TODO
}, verboseOption, quietOption);

await rootCommand.InvokeAsync(args);