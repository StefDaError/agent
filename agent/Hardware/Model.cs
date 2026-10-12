using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agent.Hardware
{
    public sealed record CpuInfo(
        string Name,
        int Cores,
        int Threads,
        double MaxClockGhz
        );

    public sealed record RamModule(
        string Manufacturer,
        long SizeBytes,
        int SpeedMhz,
        string Type
        );

    public sealed record DiskInfo(
        string Model,
        long SizeBytes,
        string InterfaceType,
        string MediaType
        );

    public sealed record GpuInfo(
        string Name,
        long? VramBytes,
        string DriverVersion
        );

    public sealed record MotherboardInfo(
        string Manufacturer,
        string Product,
        string SerialNumber
        );

    public sealed record OsInfo(
        string Caption,
        string Version,
        string Architecture,
        string MachineName,
        TimeSpan Uptime
        );

    public sealed record HardwareInfo(
        CpuInfo Cpu,
        IReadOnlyList<RamModule> RamModules,
        IReadOnlyList<DiskInfo> Disks,
        IReadOnlyList<GpuInfo> Gpus,
        MotherboardInfo Motherboard,
        OsInfo Os
        )
    {
        public long TotalRamBytes
        {
            get
            {
                long total = 0;
                foreach (var m in RamModules) total += m.SizeBytes;
                return total;
            }
        }

        public long TotalDiskBytes
        {
            get
            {
                long total = 0;
                foreach (var b in Disks) total += b.SizeBytes;
                return total;
            }
        }
    }
}
