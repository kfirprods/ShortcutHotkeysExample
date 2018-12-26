using NonInvasiveKeyboardHookLibrary;

namespace ShortcutHotkeysExample
{
    /// <summary>
    /// This static class turns the non-invasive KeyboardHookManager into a singleton, accessible by all modules
    /// </summary>
    public static class KeyboardHookManagerSingleton
    {
        private static KeyboardHookManager _keyboardHookManager;

        public static KeyboardHookManager Instance => _keyboardHookManager ?? (_keyboardHookManager = new KeyboardHookManager());
    }
}
