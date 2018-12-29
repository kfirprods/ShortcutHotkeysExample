using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using NonInvasiveKeyboardHookLibrary;

namespace ShortcutHotkeysExample.ViewModels
{
    /// <summary>
    /// This view model represents a keyboard shortcut. Its data directly affects the keyboard hook
    /// </summary>
    public class KeyboardShortcutViewModel : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string ShortcutTarget { get; set; }

        public ObservableCollection<ModifierKeys> Modifiers { get; set; }

        public int StandardKey
        {
            get => this._standardKey;
            set
            {
                this._standardKey = value;

                if (this.IsActive)
                {
                    this.Unregister();
                    this.Register();
                }
            }
        }

        private Guid? _identifier;

        private bool _isActive;
        public bool IsActive
        {
            get => this._isActive;
            set
            {
                if (this._isActive == value) return;
                this._isActive = value;

                this.HandleActiveStatusChanged();
            }
        }

        private DateTime? _lastRunTime;

        public DateTime? LastRunTime
        {
            get => this._lastRunTime;
            set
            {
                this._lastRunTime = value;
                this.OnPropertyChanged();
            }
        }

        private readonly Action _action;
        private int _standardKey;

        public KeyboardShortcutViewModel()
        {
            this._action = () =>
            {
                this.LastRunTime = DateTime.Now;
                Process.Start(this.ShortcutTarget);
            };
            this.Modifiers = new ObservableCollection<ModifierKeys>();
            this.Modifiers.CollectionChanged += Modifiers_CollectionChanged;
        }

        private void Modifiers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.IsActive)
            {
                this.Unregister();
                this.Register();
            }
        }

        public KeyboardShortcutViewModel(string name, string target) : this()
        {
            this.Name = name;
            this.ShortcutTarget = target;
        }

        private void HandleActiveStatusChanged()
        {
            if (this.IsActive)
            {
                this.Register();
            }
            else
            {
                this.Unregister();
            }
        }

        private void Register()
        {
            this._identifier = KeyboardHookManagerSingleton.Instance.RegisterHotkey(this.Modifiers.ToArray(), this.StandardKey,
                this._action);
        }

        private void Unregister()
        {
            if (this._identifier.HasValue)
            {
                KeyboardHookManagerSingleton.Instance.UnregisterHotkey(this._identifier.Value);
            }
        }

        #region INotifyPropertyChanged interface implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
