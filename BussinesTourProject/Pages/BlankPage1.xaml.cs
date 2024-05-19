using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace BussinesTourProject.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
        }
        bool isRadioButtonMode = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton clickedButton = sender as ToggleButton;

            if (isRadioButtonMode)
            {
                // Deselect all other buttons if in radio button mode
                foreach (var child in (clickedButton.Parent as Panel).Children)
                {
                    if (child is ToggleButton button && button != clickedButton)
                    {
                        button.IsChecked = false;
                    }
                }
            }

            // Perform any additional logic here if needed
        }

        private void SwitchModeButton_Click(object sender, RoutedEventArgs e)
        {
            isRadioButtonMode = !isRadioButtonMode;

            // Optionally update UI or perform other logic when switching modes
        }

    }
}
