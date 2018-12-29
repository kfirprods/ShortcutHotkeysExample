# ShortcutHotkeysExample
A C# WPF app that demonstrates uses of the NonInvasiveKeyboardHook package by letting users configure hotkeys that serve as customizable shortcuts

![Keyboard Shortcuts In-App Screenshot](https://i.imgur.com/wvEVjfJ.png)

This example app demonstrates concepts that may help you use the NonInvasiveKeyboardHook better:
1) A singleton to maintain one KeyboardHookManager instance
2) Re-registering keys to update their keys
3) Pausing and resuming the keyboard hook manager
