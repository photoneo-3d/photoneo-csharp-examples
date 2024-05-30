/*
* Photoneo's API Example - RecordingExample.cpp
* Sets up and starts recording
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pho.api.csharp;

class Program
{
    public class PhoXiExamples
    {
        private PhoXi _phoXiDevice;
        private PhoXiFactory _factory;

        public void RecordingExample()
        {
            Console.WriteLine("\nRecording Example");

            _factory = new PhoXiFactory();
            _phoXiDevice = _factory.CreateAndConnectFirstAttached();
            if (_phoXiDevice) {
                Console.WriteLine("You have already PhoXi device opened in PhoXi Control, the API Example is connected to device: "
                    + _phoXiDevice.HardwareIdentification);
            }
            else {
                Console.WriteLine("You have no PhoXi device opened in PhoXi Control, the API Example will try to connect to first device in device list");
            }

            //Check if the device is connected
            if (_phoXiDevice == null || !_phoXiDevice.isConnected()) {
                Console.WriteLine("Device is not created or not connected!");
                return;
            }

            if (_phoXiDevice.IsRecording()) {
                //Recording may be started by default when connected to device, let's stop it
                _phoXiDevice.StopRecording();
            }

            //Read last stored recording options for the device
            var recordingOptions = _phoXiDevice.RecordingOptions();
            Console.WriteLine("Current recording options:\n" + recordingOptions);

            //Setup PLY recording with enabled Point Cloud, Depth Map and Texture
            /* Note:
             * When `folder` path specified is a relative path, then it is relative to PhoXiControl working directory:
             * Windows: 'c:\Users\{user name}\AppData\Roaming\PhotoneoPhoXiControl'
             * Linux: '~/.PhotoneoPhoXiControl'
             */
            string plyRecordingOptions = @"{
                ""folder"": ""RecordingExampleOutput"",
                ""every"": 1,
                ""capacity"": -1,
                ""pattern"": ""my_scan_####"",
                ""containers"": {
                    ""ply"": {
                        ""enabled"": true,
                        ""point_cloud"": true,
                        ""depth_map"": true,
                        ""texture"": true
                    }
                }
            }";

            //Start recording with setup json options for PLY container, do not store options persistently
            var ret = _phoXiDevice.StartRecording(plyRecordingOptions, false);
            if (ret != PhoXi.StartRecordingResult.Success) {
                Console.WriteLine("Failed to start recording for PLY container! Error: " + ret.ToString());
                return;
            }

            //Get changed recording options
            var changedRecordingOptions = _phoXiDevice.RecordingOptions();
            Console.WriteLine("Changed recording options:\n" + changedRecordingOptions);

            _phoXiDevice.TriggerMode = PhoXiTriggerMode.Value.Software;
            if (!_phoXiDevice.TriggerModeFeature.isLastOperationSuccessful()) {
                Console.WriteLine("Failed to set trigger mode to Software!");
                return;
            }

            if (!_phoXiDevice.isAcquiring()) {
                if (!_phoXiDevice.StartAcquisition()) {
                    Console.WriteLine("Error in StartAcquisition");
                    return;
                }
            }

            var frameIndex = _phoXiDevice.TriggerFrame(true, true);
            if (frameIndex < 0) {
                Console.WriteLine("Failed to trigger frame!");
                return;
            }

            //Wait for recorder to finish recording all wanted frames before stopping recording.
            //This function will return frame index even for frames which were not actually recorded
            //due to `every` (every n-th) skipping.
            while(frameIndex != _phoXiDevice.LastRecordedFrameIndex()) {
                System.Threading.Thread.Sleep(100);
            }

            if (!_phoXiDevice.StopRecording()) {
                Console.WriteLine("Failed to stop recording!");
                return;
            }

            _phoXiDevice.StopAcquisition();
            //Disconnect and keep PhoXiControl connected
            _phoXiDevice.Disconnect();
        }

        public PhoXiExamples()
        {
            try
            {
                RecordingExample();
            }
            catch (Exception internalException)
            {
                Console.WriteLine(internalException.Message);
            }
        }
    };

    static void Main(string[] args)
    {
        var examples = new PhoXiExamples();
    }
}