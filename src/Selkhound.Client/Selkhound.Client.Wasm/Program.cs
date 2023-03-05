//
//  Program.cs
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
using Microsoft.UI.Xaml;

namespace Selkhound.Client.Wasm
{
    /// <summary>
    /// The main entry point for WASM applications.
    /// </summary>
    public sealed class Program
    {
        private static App _app = null!;

        private static int Main(string[] args)
        {
            Microsoft.UI.Xaml.Application.Start(_ => _app = new AppHead());

            return 0;
        }
    }
}
