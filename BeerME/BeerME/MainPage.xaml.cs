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
using Windows.Devices.Gpio;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BeerME
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.InitGPIO();
            this.IsPinAvailable();
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            if (gpio == null)
            {
                IsGPIOAvail.Text = "There is no GPIO controller on this device.";
                return;
            }
            else
            {
                IsGPIOAvail.Text = "The GPIO controller is available.";
            }
        }

        private void IsPinAvailable()
        {
            var TempPin = GpioOpenStatus.PinOpened;

            if (TempPin == 0)
            {
                IsPinAvail.Text = "The GPIO pin is available.";
                ReadPinData();
            }
            else
            {
                IsPinAvail.Text = "The GPIO pin is not available.";
                return;
            }
        }

        private void ReadPinData()
        {
            const int PinNumber = 4;

            var gpio = GpioController.GetDefault();
            GpioPin TempPinInput = gpio.OpenPin(PinNumber);
            
            if (TempPinInput != null)
            {
                IsPinOpenDisplay.Text = "The pin was able to be opened.";

                if (TempPinInput.Read() == GpioPinValue.High)
                {
                    PinReadDisplay.Text = "The pin is being read.";
                    PinOutputDisplay.Text = GpioPinValue.High.ToString();
                }

                else
                {
                    PinReadDisplay.Text = "The pin is not being read.";
                }
                
            }
            
            else
            {
                IsPinOpenDisplay.Text = "The pin was not able to be opened.";
            }


        }
    }
}
