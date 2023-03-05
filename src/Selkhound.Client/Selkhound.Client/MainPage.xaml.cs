//
//  MainPage.xaml.cs
//
//  Author:
//       LuzFaltex Contributors
//
//  LGPL-3.0 License
//
//  Copyright (c) 2022 LuzFaltex
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Selkhound.Client.Pages;
using Windows.ApplicationModel.Core;
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409
namespace Selkhound.Client
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            // Hide default title bar
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            // Set caption buttons backgroudn to transparent
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            titleBar.ButtonBackgroundColor = Colors.Transparent;

            // Set XAML element as drag region
            Window.Current.SetTitleBar(AppTitleBar);

            // Register a handler for when the size of the overlaid caption control changes
            coreTitleBar.LayoutMetricsChanged += CoreTitleBar_LayoutMetricsChanged;

            // Register a handler for when the title bar visibiltiy changes.
            // For example, when the title bar is invoked in full screen mode.
            coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;

            // Register a handler for when the window activation changes.
            Window.Current.CoreWindow.Activated += CoreWindow_Activated;
        }

        private readonly List<(string Tag, Type Page)> _pages = new()
        {
            ("home", typeof(HomePage)),
            ("conversations", typeof(ConversationsPage)),
            ("communities", typeof(CommunitiesPage)),
            ("about", null!)
        };

        private void MainNavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                Navigate("settings", args.RecommendedNavigationTransitionInfo);
            }
            else if (args.SelectedItemContainer is not null)
            {
                var navItemTag = args.SelectedItemContainer.Tag.ToString();
                Navigate(navItemTag!, args.RecommendedNavigationTransitionInfo);
            }
        }

        private void MainNavContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new InvalidOperationException($"Failed to load {e.SourcePageType.FullName}: {e.Exception}");
        }

        private void MainNavView_Loaded(object sender, RoutedEventArgs e)
        {
            // NavView doesn't load any page by default. Load the home page.
            MainNavView.SelectedItem = MainNavView.MenuItems[0];
        }

        private void Navigate(string navItemTag, NavigationTransitionInfo transitionInfo)
        {
            Type page;

            if (navItemTag == "settings" && Window.Current.Content is Frame mainFrame)
            {
                mainFrame.Navigate(typeof(SettingsPage), null, transitionInfo);
            }
            else
            {
                var item = _pages.FirstOrDefault(p => p.Tag.Equals(navItemTag));
                page = item.Page;

                // Get the page type before navigation so you can prevent duplicate
                // entries in the backstack.
                if (page is not null && !page.Equals(MainNavContentFrame.SourcePageType))
                {
                    MainNavContentFrame.Navigate(page, null, transitionInfo);
                }
            }
        }

        private void CoreTitleBar_LayoutMetricsChanged(CoreApplicationViewTitleBar sender, object args)
        {
            // Get the size of the caption controls and set padding.
            LeftPaddingColumn.Width = new GridLength(sender.SystemOverlayLeftInset);
            RightPaddingColumn.Width = new GridLength(sender.SystemOverlayRightInset);
        }

        private void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar sender, object args)
        {
            AppTitleBar.Visibility = sender.IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void CoreWindow_Activated(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.WindowActivatedEventArgs args)
        {
            UISettings settings = new();
            if (args.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                AppTitleTextBlock.Foreground = new SolidColorBrush(settings.UIElementColor(UIElementType.GrayText));
            }
            else
            {
                AppTitleTextBlock.Foreground = new SolidColorBrush(settings.UIElementColor(UIElementType.WindowText));
            }
        }
    }
}
