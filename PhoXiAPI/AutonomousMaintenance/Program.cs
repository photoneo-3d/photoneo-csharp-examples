using System;
using pho.api.csharp;

class Program {
    static void printDeviceInfo(ref PhoXiDeviceInformation deviceInfo)
    {
        Console.WriteLine("  Name:                    " + deviceInfo.Name);
        Console.WriteLine("  Hardware Identification: " + deviceInfo.HWIdentification);
        Console.WriteLine("  Type:                    " + deviceInfo.Type);
        Console.WriteLine("  Firmware version:        " + deviceInfo.FirmwareVersion);
        Console.WriteLine("  Variant:                 " + deviceInfo.Variant);
        Console.WriteLine("  IsFileCamera:            " + (deviceInfo.IsFileCamera ? "Yes" : "No"));
        Console.WriteLine("  Feaure-Alpha:            " + (deviceInfo.CheckFeature("Alpha") ? "Yes" : "No"));
        Console.WriteLine("  Feaure-Color:            " + (deviceInfo.CheckFeature("Color") ? "Yes" : "No"));
        Console.WriteLine("  Status:                  " +
                            (deviceInfo.Status.Attached ? "Attached to PhoXi Control. " : "Not Attached to PhoXi Control. ") +
                            (deviceInfo.Status.Ready ? "Ready to connect" : "Occupied") + "\n");
    }

    static void Main(string[] args)
    {
        PhoXiFactory factory = new PhoXiFactory();
        //Wait for the PhoXi Control
        while (!factory.isPhoXiControlRunning())
        {
            System.Threading.Thread.Sleep(100);
        }

        Console.WriteLine("PhoXi Control version: {0}\n", factory.GetPhoXiControlVersion());
        Console.WriteLine("PhoXi API version: {0}\n", factory.GetAPIVersion());

        PhoXiDeviceInformation[] deviceList = factory.GetDeviceList();
        Console.WriteLine("PhoXi Factory found {0}  devices by GetDeviceList call.\n", deviceList.Length);
        for (var i = 0; i < deviceList.Length; i++)
        {
            Console.WriteLine("Device: {0}", i);
            printDeviceInfo(ref deviceList[i]);
        }

        //Try to connect Device opened in PhoXi Control, if Any
        PhoXi phoXiDevice = factory.CreateAndConnectFirstAttached();
        if (phoXiDevice != null)
        {
            Console.WriteLine("You have already PhoXi device opened in PhoXi Control, the API Example is connected to device: " + (String)phoXiDevice.HardwareIdentification);
        }
        else
        {
            Console.WriteLine("You have no PhoXi device opened in PhoXi Control, the API Example will try to connect to last device in device list");
            if (deviceList.Length > 0)
            {
                phoXiDevice = factory.CreateAndConnectFirstAttached();
            }
        }
        if (phoXiDevice == null || !phoXiDevice.isConnected())
        {
            Console.WriteLine("No device is connected!");
            return;
        }


        var currentGeneralSettings = phoXiDevice.MotionCam;
        currentGeneralSettings.MaintenanceMode = PhoXiMaintenanceMode.Value.Auto;
        phoXiDevice.MotionCam = currentGeneralSettings;

        if (phoXiDevice.MotionCam.MaintenanceMode != PhoXiMaintenanceMode.Value.Auto)
        {
            Console.WriteLine("Device does not support MaintenanceMode!");
            return;
        }

        int frameID = phoXiDevice.TriggerFrame();
        if (frameID < 0)
        {
            Console.WriteLine("Trigger was unsuccessful!");
            return;
        }
        Frame myFrame = phoXiDevice.GetSpecificFrame(frameID, PhoXiTimeout.Value.Infinity);
        if (myFrame != null)
        {
            for (int i = 0; i < myFrame.Messages.Length; i++)
            {
                Console.WriteLine(myFrame.Messages[i].Text);

            }
        }
        else
        {
            Console.WriteLine("Failed to retrieve the frame!");
        }

        phoXiDevice.Disconnect();
    }
}
