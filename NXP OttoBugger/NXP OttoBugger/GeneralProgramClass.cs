using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXP_OttoBugger
{
    
    public static class GeneralProgramClass
    {
        public enum UploadMode
        {
            NONE,
            CONFIG,
            PROGRAM
        }
        public static UploadMode ModeForUpload;
        public static bool READCFG = false;
        public static string DefaultFileLocation;
    }
}
