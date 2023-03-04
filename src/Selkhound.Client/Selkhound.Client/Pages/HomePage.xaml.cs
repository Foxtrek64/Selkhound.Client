using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Selkhound.Client.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        private readonly List<(string Tag, Type Page)> _pages = new()
        {
            ("home", typeof(HomePage)),
            ("conversations", typeof(ConversationsPage)),
            ("communities", typeof(CommunitiesPage)),
            ("about", null)
        };

        private void HomeNavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.SelectedItemContainer is not null)
            {
                var navItemTag = args.SelectedItemContainer.Tag.ToString();
                Navigate(navItemTag, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void HomeNavContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }

        private void HomeNavView_Loaded(object sender, RoutedEventArgs e)
        {
            // NavView doesn't load any page by default. Load the home page.
            HomeNavView.SelectedItem = HomeNavView.MenuItems[0];
        }

        private void Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type _page = null;

            if (navItemTag == "settings")
            {
                _page = typeof(SettingsPage);
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                _page = item.Page;
            }

            // Get the page type before navigation so you can prevent duplicate
            // entries in the backstack.
            if (_page is not null && !_page.Equals(HomeNavContentFrame.SourcePageType))
            {
                HomeNavContentFrame.Navigate(_page, null, transitionInfo);
            }
        }
    }
}
