//
//  Main.Android.cs
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
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Nostra13.Universalimageloader.Core;
using Microsoft.UI.Xaml.Media;

namespace Selkhound.Client.Droid
{
    /// <summary>
    /// Application bootstrapper for Android.
    /// </summary>
    [global::Android.App.ApplicationAttribute(
        Label = "@string/ApplicationName",
        Icon = "@mipmap/icon",
        LargeHeap = true,
        HardwareAccelerated = true,
        Theme = "@style/AppTheme"
    )]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name", Justification = "Name of file is required by build system.")]
    public class Application : Microsoft.UI.Xaml.NativeApplication
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        /// <param name="javaReference">The handle that started this application.</param>
        /// <param name="transfer">An ownership handle.</param>
        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(() => new AppHead(), javaReference, transfer)
        {
            ConfigureUniversalImageLoader();
        }

        private static void ConfigureUniversalImageLoader()
        {
            // Create global configuration and initialize ImageLoader with this config
            ImageLoaderConfiguration config = new ImageLoaderConfiguration
                .Builder(Context)
                .Build();

            ImageLoader.Instance.Init(config);

            ImageSource.DefaultImageLoader = ImageLoader.Instance.LoadImageAsync;
        }
    }
}
