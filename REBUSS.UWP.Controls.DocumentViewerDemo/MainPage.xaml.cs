using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using AnimationDirection = Windows.UI.Composition.AnimationDirection;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace REBUSS.UWP.Controls.DocumentViewerDemo
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //private void AnimatedImage_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        //{
        //    Storyboard.Begin();
        //    MainStoryboard.Begin();
        //}

        //private void MainAnimatedImage_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        //{
        //    StoryboardBack.Begin();
        //    MainStoryboardBack.Begin();
        //}
    }
}
