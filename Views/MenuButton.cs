using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

using MapControl;

using DependencyPropertyHelper = MapControl.DependencyPropertyHelper;

namespace WpfMapPlayground.Views
{
    [ContentProperty(nameof(Items))]
    public class MenuButton : Button
    {
        public static readonly DependencyProperty MapProperty =
            DependencyPropertyHelper.Register<MenuButton, MapBase>(nameof(Map), null,
                                                                   async (button, oldValue, newValue) => await button.Initialize());

        static MenuButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MenuButton), new FrameworkPropertyMetadata(typeof(MenuButton)));
        }

        public MenuButton()
        {
            ContextMenu = new ContextMenu();
            DataContextChanged += (s, e) => ContextMenu.DataContext = e.NewValue;
            Loaded += async (s,       e) => await Initialize();
            Click += (s,              e) => ContextMenu.IsOpen = true;
        }

        public string Icon
        {
            get => Content as string;
            set => Content = value;
        }

        public ContextMenu Menu => ContextMenu;

        public ItemCollection Items => ContextMenu.Items;

        public MapBase Map
        {
            get => (MapBase)GetValue(MapProperty);
            set => SetValue(MapProperty, value);
        }

        private async Task Initialize()
        {
            if (Map != null)
            {
                DataContext = Map;

                if (Items.Count > 0 && Items[0] is MapMenuItem item)
                {
                    await item.Execute(Map);
                }
            }
        }
    }

    public abstract class MapMenuItem : MenuItem
    {
        protected MapMenuItem()
        {
            Loaded += (s, e) =>
            {
                if (DataContext is MapBase map)
                {
                    IsChecked = GetIsChecked(map);
                }
            };

            Click += async (s, e) =>
            {
                if (DataContext is MapBase map)
                {
                    await Execute(map);

                    foreach (MapMenuItem item in ParentMenuItems)
                    {
                        item.IsChecked = item.GetIsChecked(map);
                    }
                }
            };
        }

        public string Text
        {
            get => Header as string;
            set => Header = value;
        }

        protected IEnumerable<MapMenuItem> ParentMenuItems => ((ItemsControl)Parent).Items.OfType<MapMenuItem>();

        protected abstract bool GetIsChecked(MapBase map);

        public abstract Task Execute(MapBase map);
    }

    public static class HyperlinkText
    {
        private static readonly Regex regex = new Regex(@"\[([^\]]+)\]\(([^\)]+)\)");

        /// <summary>
        /// Converts text containing hyperlinks in markdown syntax [text](url)
        /// to a collection of Run and Hyperlink inlines.
        /// </summary>
        public static IEnumerable<Inline> TextToInlines(this string text)
        {
            var inlines = new List<Inline>();

            while (!string.IsNullOrEmpty(text))
            {
                var match = regex.Match(text);

                if (match.Success &&
                    match.Groups.Count == 3 &&
                    Uri.TryCreate(match.Groups[2].Value, UriKind.Absolute, out Uri uri))
                {
                    inlines.Add(new Run { Text = text.Substring(0, match.Index) });
                    text = text.Substring(match.Index + match.Length);

                    var link = new Hyperlink { NavigateUri = uri };
                    link.Inlines.Add(new Run { Text = match.Groups[1].Value });
#if !WINUI && !UWP
                    link.ToolTip = uri.ToString();

                    link.RequestNavigate += (s, e) =>
                    {
                        try
                        {
                            Process.Start(new ProcessStartInfo(e.Uri.ToString()) { UseShellExecute = true });
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"{e.Uri}: {ex}");
                        }
                    };
#endif
                    inlines.Add(link);
                }
                else
                {
                    inlines.Add(new Run { Text = text });
                    text = null;
                }
            }

            return inlines;
        }

        public static readonly DependencyProperty InlinesSourceProperty = DependencyProperty.RegisterAttached(
            "InlinesSource", typeof(string), typeof(HyperlinkText), new PropertyMetadata(null, InlinesSourcePropertyChanged));

        public static string GetInlinesSource(DependencyObject element)
        {
            return (string)element.GetValue(InlinesSourceProperty);
        }

        public static void SetInlinesSource(DependencyObject element, string value)
        {
            element.SetValue(InlinesSourceProperty, value);
        }

        private static void InlinesSourcePropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            InlineCollection inlines = null;

            if (obj is TextBlock block)
            {
                inlines = block.Inlines;
            }
            else if (obj is Paragraph paragraph)
            {
                inlines = paragraph.Inlines;
            }

            if (inlines != null)
            {
                inlines.Clear();

                foreach (var inline in TextToInlines((string)e.NewValue))
                {
                    inlines.Add(inline);
                }
            }
        }
    }
}