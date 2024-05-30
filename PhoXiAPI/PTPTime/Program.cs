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

        public void FrameStartTimeExample()
        {
            Console.WriteLine("\nFrameStartTime Example");

            _factory = new PhoXiFactory();
            _phoXiDevice = _factory.CreateAndConnectFirstAttached();
            if (_phoXiDevice)
            {
                Console.WriteLine("You have already PhoXi device opened in PhoXi Control, the API Example is connected to device: "
                    + _phoXiDevice.HardwareIdentification);
            }
            else
            {
                Console.WriteLine("You have no PhoXi device opened in PhoXi Control, the API Example will try to connect to first device in device list");
            }

            //Check if the device is connected
            if (_phoXiDevice == null || !_phoXiDevice.isConnected())
            {
                Console.WriteLine("Device is not created or not connected!");
                return;
            }

            _phoXiDevice.TriggerMode = PhoXiTriggerMode.Value.Software;
            if (!_phoXiDevice.TriggerModeFeature.isLastOperationSuccessful())
            {
                Console.WriteLine("Failed to set trigger mode to Software!");
                return;
            }

            if (!_phoXiDevice.isAcquiring())
            {
                if (!_phoXiDevice.StartAcquisition())
                {
                    Console.WriteLine("Error in StartAcquisition");
                    return;
                }
            }

            var frameIndex1 = _phoXiDevice.TriggerFrame();
            if (frameIndex1 < 0)
            {
                Console.WriteLine("Failed to trigger frame!");
                return;
            }

            var frame1 = _phoXiDevice.GetSpecificFrame(frameIndex1);
            if (!frame1)
            {
                Console.WriteLine("Failed to getting frame!");
                return;
            }

            var frameIndex2 = _phoXiDevice.TriggerFrame();
            if (frameIndex2 < 0)
            {
                Console.WriteLine("Failed to trigger frame!");
                return;
            }

            var frame2 = _phoXiDevice.GetSpecificFrame(frameIndex2);
            if (!frame2)
            {
                Console.WriteLine("Failed to getting frame!");
                return;
            }

            // Get two frames and calculate delay between them
            PhoXiPTPTime ptpTime1 = frame1.Info.FrameStartTime;
            PhoXiPTPTime ptpTime2 = frame2.Info.FrameStartTime;

            if (!ptpTime1.IsValid || !ptpTime1.IsValid)
            {
                Console.WriteLine("Frame doesn't contains valid start acquisition time!");
                return;
            }

            Console.WriteLine("Frame 1 start acquisition time is {0}", ptpTime1.TimeUTC);
            Console.WriteLine("Frame 2 start acquisition time is {0}", ptpTime2.TimeUTC);

            var duration = ptpTime2.TimeUTC - ptpTime1.TimeUTC;
            Console.WriteLine("Duration between frames is {0} ms", duration.TotalMilliseconds);

            _phoXiDevice.StopAcquisition();
            //Disconnect and keep PhoXiControl connected
            _phoXiDevice.Disconnect();
        }

        public PhoXiExamples()
        {
            try
            {
                FrameStartTimeExample();
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