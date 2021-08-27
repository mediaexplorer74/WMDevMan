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
using DeviceManager;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.System;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WMDevMan
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            //Run();
        }

        void SuperWriteLine(string onestring)
        {

            ResponseBox.Items.Add(onestring);
        }

        // RnD  
        async void Run()
        {
            var options = new LauncherOptions();

            // TargetApplicationPackageFamilyName
            options.TargetApplicationPackageFamilyName = "45240.QuickShutdown_2e7exxddxvejw";
            
            // app URI ?
            var launcherUri = new Uri("App:?AddUrl=http………com"); 

            // RnD: External thing
            await Launcher.LaunchUriAsync(launcherUri, options);

        }

        MobileDevice device;
        private void conn_btn_Click(object sender, RoutedEventArgs e)
        {
            // Prepare Response Box 
            ResponseBox.Items.Clear();

            if ((addr_tbx.Text == "127.0.0.1"))// || (addr_tbx.Text == "127.0.0.1:10080"))
            {
                // Local mode
                // 
                device = MobileDeviceManager.ConnectLocal();

                if (device != null)
                {
                    Debug.WriteLine("Device connected locally.");
                    SuperWriteLine ("Device connected locally.");
                }
            }
            else
            {
                // Remote mode 

                device = MobileDeviceManager.ConnectRemote(addr_tbx.Text);

                if (device != null)
                {
                   
                    Debug.WriteLine("Device connected remotely.");
                    SuperWriteLine("Device connected remotely.");
                }
            }
        }

        // Show System Processes
        private async void showprocesses_btn_Click(object sender, RoutedEventArgs e)
        {
            // check connect
            if (device != null)
            {
                if (device.IsAuthed == false)
                {

                    // Connect Credential
                    device.Auth(new ConnectCredential()
                    {
                        UserName = "Administrator",
                        Pin = pin_tbx.Text

                    }
                    );

                    if (device.IsAuthed)
                    {
                        Debug.WriteLine("Device is authed");
                        SuperWriteLine("Device is authed");

                    }
                    else
                    {
                        Debug.WriteLine("Device is NOT authed (or no Pin )");
                        SuperWriteLine("Device is NOT authed (or no Pin )");
                    }
                }

                // DELAY
                await Task.Delay(100);

                try
                {
                    // get device process list
                    var list = await device.GetProcessesInfoAsync();

                    ResponseBox.Items.Clear();

                    foreach (var Item in list)
                    {
                        Debug.WriteLine
                        (
                            "Process Id " + Item.ProcessId.ToString()
                            +
                            "  CPU Usage " + Item.CPUUsage.ToString()
                             +
                            "  Image Name " + Item.ImageName.ToString()
                        );
                        SuperWriteLine
                        (
                            "Process Id " + Item.ProcessId.ToString()
                            +
                            "  CPU Usage " + Item.CPUUsage.ToString()
                             +
                            "  Image Name " + Item.ImageName.ToString()
                        );


                    }
                }
                catch (Exception e1)
                {

                    Debug.WriteLine("Exception: " + e1.Message);
                    SuperWriteLine("Exception: " + e1.Message);
                }
            }
            else
            {
                Debug.WriteLine("Please, connect first!");
                SuperWriteLine("Please, connect first!");
            }
        }

        // Get installed App Info Btn Click handler
        private async void getappinfo_btn_Click(object sender, RoutedEventArgs e)
        {
            // check connect
            if (device != null)
            {
                if (device.IsAuthed == false)
                {

                    // Connect Credential
                    device.Auth(new ConnectCredential()
                    {
                        UserName = "Administrator",
                        Pin = pin_tbx.Text

                    }
                    );

                    if (device.IsAuthed)
                    {
                        Debug.WriteLine("Device is authed");
                        SuperWriteLine("Device is authed");

                    }
                    else
                    {
                        Debug.WriteLine("Device is NOT authed (or no Pin )");
                        SuperWriteLine("Device is NOT authed (or no Pin )");
                    }
                }

                // DELAY
                await Task.Delay(100);

                try
                {
                    // get installed app info list
                    var list = await device.GetAppsInfoAsync();

                    ResponseBox.Items.Clear();

                    foreach (var Item in list)
                    {
                        Debug.WriteLine
                        (
                            "Name " + Item.Name.ToString()
                             +
                            "  Is Xap " + Item.IsXap.ToString()
                             +
                            "  Pckg Family Name " + Item.PackageFamilyName.ToString()
                            +
                            "  Pcg Full Usage " + Item.PackageFullName.ToString()
                           
                            
                        );
                        SuperWriteLine
                        (
                            "Name " + Item.Name.ToString()
                             +
                            "  Is Xap " + Item.IsXap.ToString()
                             +
                            "  Pckg Family Name " + Item.PackageFamilyName.ToString()
                            +
                            "  Pcg Full Usage " + Item.PackageFullName.ToString()
                        );

                    }
                }
                catch (Exception e1)
                {

                    Debug.WriteLine("Exception: " + e1.Message);
                    SuperWriteLine("Exception: " + e1.Message);
                }
            }//if (device = null)
            else
            {
                Debug.WriteLine("Please, connect first!");
                SuperWriteLine("Please, connect first!");
            }
        }//getappinfo_btn_Click end
    }//class end
}// namespace end
