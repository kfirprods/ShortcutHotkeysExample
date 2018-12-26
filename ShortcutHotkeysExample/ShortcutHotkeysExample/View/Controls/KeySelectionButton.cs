using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShortcutHotkeysExample.View.Controls
{
    public class KeySelectionButton : Button
    {
        public static DependencyProperty ExcludedKeysProperty =
            DependencyProperty.Register(nameof(ExcludedKeys), typeof(int[]), typeof(KeySelectionButton), new PropertyMetadata(new int[0]));

        public int[] ExcludedKeys
        {
            get => (int[])this.GetValue(ExcludedKeysProperty);
            set => this.SetValue(ExcludedKeysProperty, value);
        }

        public static DependencyProperty SelectedKeyProperty =
            DependencyProperty.Register(nameof(SelectedKey), typeof(int), typeof(KeySelectionButton),
                new FrameworkPropertyMetadata(
                    (a, b) =>
                    {
                        var button = (KeySelectionButton)a;
                        button.SetTextToKey();
                    }));

        public int SelectedKey
        {
            get => (int)this.GetValue(SelectedKeyProperty);
            set => this.SetValue(SelectedKeyProperty, value);
        }

        private bool _isWaitingForKey;

        private const string ChooseKeyText = "Press key or ESC";

        public KeySelectionButton()
        {
            this.Click += KeySelectButton_Click;
            this.KeyUp += KeySelectButton_KeyUp;
            this.Content = ChooseKeyText;
        }

        void KeySelectButton_KeyUp(object sender, KeyEventArgs e)
        {
            var virtualKeyCode = KeyInterop.VirtualKeyFromKey(e.Key);

            if (this._isWaitingForKey)
            {
                if (this.ExcludedKeys.Contains(virtualKeyCode))
                {
                    return;
                }

                this._isWaitingForKey = false;

                if (e.Key == Key.Escape)
                {
                    this.SetTextToKey();
                    return;
                }
                else
                {
                    this.SelectedKey = virtualKeyCode;
                    this.SetTextToKey();
                }
            }
        }

        private void KeySelectButton_Click(object sender, EventArgs e)
        {
            this.Content = ChooseKeyText;
            this._isWaitingForKey = true;
        }

        public void SetTextToKey()
        {
            this.Content = KeyInterop.KeyFromVirtualKey(this.SelectedKey).ToString();
        }
    }
}
