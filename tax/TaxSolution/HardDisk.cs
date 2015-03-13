using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Management;

namespace TaxSolution
{
    class HardDisk
    {
        private string model = null;
        private string type = null;
        private string serialNo = null;

        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        public string getserial()
        {
            ArrayList hdCollection = new ArrayList();

			ManagementObjectSearcher searcher = new
				ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

			foreach(ManagementObject wmi_HD in searcher.Get())
			{
                HardDisk hd = new HardDisk();
				hd.Model	= wmi_HD["Model"].ToString();
				hd.Type		= wmi_HD["InterfaceType"].ToString();

				hdCollection.Add(hd);
			}

			searcher = new
				ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMedia");

			foreach(ManagementObject wmi_HD in searcher.Get())
			{
				// get the hardware serial no.
                if (wmi_HD["SerialNumber"] == null)
                {
                }
                else
                {
                    return wmi_HD["SerialNumber"].ToString();
                }
			}

            return "";
        }
    }
}
