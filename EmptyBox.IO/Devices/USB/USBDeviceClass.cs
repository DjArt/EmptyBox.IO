using System;
using System.Collections.Generic;
using System.Text;

namespace EmptyBox.IO.Devices.USB
{
    public enum USBDeviceClass : byte
    {
        /// <summary>
        /// Device class is unspecified, interface descriptors are used to determine needed drivers.
        /// </summary>
        Unspecified = 0x00,
        /// <summary>
        /// Speaker, microphone, sound card, MIDI.
        /// </summary>
        Audio = 0x01,
        /// <summary>
        /// Modem, Ethernet adapter, Wi-Fi adapter, RS-232 serial adapter. Used together with class CDCData.
        /// </summary>
        CDCControl = 0x02,
        /// <summary>
        /// Keyboard, mouse, joystick.
        /// </summary>
        HID = 0x03,
        /// <summary>
        /// Force feedback joystick.
        /// </summary>
        PID = 0x05,
        /// <summary>
        /// Webcam, scanner.
        /// </summary>
        PTP_MTP = 0x06,
        /// <summary>
        /// Laser printer, inkjet printer, CNC machine.
        /// </summary>
        Printer = 0x07,
        /// <summary>
        /// USB flash drive, memory card reader, digital audio player, digital camera, external drive.
        /// </summary>
        MassStorage = 0x08,
        USBHub              = 0x09,
        /// <summary>
        /// Used together with class CDCControl.
        /// </summary>
        CDCData = 0x0A,
        /// <summary>
        /// USB smart card reader.
        /// </summary>
        SmartCard = 0x0B,
        /// <summary>
        /// Fingerprint reader.
        /// </summary>
        ContentSecurity = 0x0D,
        /// <summary>
        /// Webcam.
        /// </summary>
        Video = 0x0E,
        /// <summary>
        /// Pulse monitor.
        /// </summary>
        PHDC = 0x0F,
        /// <summary>
        /// Webcam, TV.
        /// </summary>
        AV = 0x10,
        /// <summary>
        /// Describes USB Type-C alternate modes supported by device.
        /// </summary>
        Billboard = 0x11,
        /// <summary>
        /// USB compliance testing device.
        /// </summary>
        DiagnosticDevice = 0xDC,
        /// <summary>
        /// Bluetooth adapter, Microsoft RNDIS.
        /// </summary>
        WirelessController = 0xE0,
        /// <summary>
        /// ActiveSync device.
        /// </summary>
        Miscellaneous       = 0xEF,
        /// <summary>
        /// IrDA Bridge, Test & Measurement Class (USBTMC), USB DFU (Device Firmware Upgrade).
        /// </summary>
        ApplicationSpecific = 0xFE,
        /// <summary>
        /// Indicates that a device needs vendor-specific drivers.
        /// </summary>
        VendorSpecific = 0xFF,
    }
}
