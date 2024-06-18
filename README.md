# VAProxy.MainMenuSettings

Simple version of SettingsAPI for the main menu.

No dependencies.

## minimal docs

You have two types of setting.

Toggles, and Buttons.

A mod can have multiple of both setting types.

For example, if you want to make a mod with a single toggle option:

```cs
ModOptions opts = new();
ToggleOption opt = new()
{
    Id = "tairasoul.toggleopt.test",
    Text = $"testOpt.TestMod",
    Toggled = (bool toggle) =>
    {
        Logger.LogInfo($"Toggled testOpt.TestMod to {toggle}");
    },
    defaultState = false
};
opts.toggles = new ToggleOption[] { opt };
MenuSettings.RegisterMod($"testMod", $"tairasoul.testmod}", opts);
```

This will create a mod entry with a single toggle option, logging `Toggled testOpt.TestMod to true/false` when toggled.

If you want a mod with a single button option:

```cs
ModOptions opts = new();
ButtonOption opt = new()
{
    Id = "tairasoul.buttonopt.test",
    Text = $"testOpt.TestMod",
    Clicked = () =>
    {
        Logger.LogInfo($"Clicked testOpt.TestMod");
    }
};
opts.buttons = new ButtonOption[] { opt };
MenuSettings.RegisterMod($"testMod", $"tairasoul.testmod}", opts);
```

This will create a mod with a single button option, logging `Clicked testOpt.TestMod` when interacted with.

You can also do additional processing upon creation of your mod's scrolling page.

ModOptions.CreationCallback gets called after setup of your mod and it's options, with your mod's scrolling page.

This is an optional field, and if you do not need any additional processing, you do not need to set it to anything.
