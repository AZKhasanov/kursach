using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ARM.Forms.Base
{
    public class WindowBase : Window
    {
        private Dictionary<Type, Window> _windows = new();

        protected virtual Window ShowWindow<TWindow>(object[] args = null) where TWindow : Window
        {
            var winType = typeof(TWindow);

            if (!_windows.TryGetValue(winType, out var window))
            {
                var ctors = winType.GetConstructors();
                foreach (var ctor in ctors)
                    if (ctor.GetParameters().Length == (args?.Length ?? 0))
                    {
                        window = (Window)ctor.Invoke(args);
                        window.Closed += (sender, args) =>
                        {
                            _windows.Remove(winType);
                            this.Show();
                        };
                        _windows.Add(winType, window);

                        break;
                    }
            }
            window?.Show();
            return window;
        }
    }
}
