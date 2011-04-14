#region License
// Copyright 2011 Jason Walker
// ungood@onetrue.name
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and 
// limitations under the License.
#endregion

using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using Omega.Client.FileSystem;

namespace Omega.BitmapViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadImage(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {
                Filter = "Image Files|*.jpg;*.png;*.bmp;*.gif"
            };

            if(dialog.ShowDialog() != true)
                return;

            var bmp = new BitmapImage(new Uri(dialog.FileName, UriKind.Absolute));

            //monochrome.Width = tricolor.Width = eightcolor.Width = 400;

            original.Source = bmp;
            monochrome.Source = BitmapHelper.SwapPalette(bmp, ColorFormat.Monochrome);
            tricolor.Source = BitmapHelper.SwapPalette(bmp, ColorFormat.TriColor);
            eightcolor.Source = BitmapHelper.SwapPalette(bmp, ColorFormat.EightColor);
        }
    }
}
