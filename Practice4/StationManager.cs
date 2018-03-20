using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice4
{
    static class StationManager
    {
        internal static readonly string WorkingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TseSarka"); 
        public static User CurrentUser { get; set; }
    }
}
