using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using NonInvasiveKeyboardHookLibrary;
using ShortcutHotkeysExample.ViewModels;

namespace ShortcutHotkeysExample
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            
            this.Shortcuts = new ObservableCollection<KeyboardShortcutViewModel>(new []
            {
                new KeyboardShortcutViewModel("Test", "C:\\Windows\\System32\\cmd.exe")
            });
        }

        private ObservableCollection<KeyboardShortcutViewModel> _shortcuts;

        public ObservableCollection<KeyboardShortcutViewModel> Shortcuts
        {
            get => this._shortcuts;
            set
            {
                this._shortcuts = value;
                this.OnPropertyChanged();
            }
        }

        private bool _isHookActive;

        public bool IsHookActive
        {
            get => this._isHookActive;
            set
            {
                this._isHookActive = value;

                // Toggle the state of the keyboard hook
                if (value)
                {
                    KeyboardHookManagerSingleton.Instance.Start();
                }
                else
                {
                    KeyboardHookManagerSingleton.Instance.Stop();
                }
            }
        }

        // This array provides all available modifier keys to the modifier key combo boxes
        public ModifierKeys[] AllModifiers => Enum.GetValues(typeof(ModifierKeys)).Cast<ModifierKeys>().ToArray();

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
