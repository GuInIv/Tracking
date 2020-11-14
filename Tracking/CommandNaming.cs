using System;
using System.Collections.Generic;
using Tracking.BusinessLayer;

namespace Tracking
{
    public static class CommandNaming
    {
        public static readonly string Add = "add";
        public static readonly string Read = "read";
        public static readonly string Find = "find";

        public static readonly IDictionary<string, Type> Commands;

        static CommandNaming()
        {
            Commands = new Dictionary<string, Type>()
            {
                [Add] = typeof(AddTrackingInfo),
                [Read] = typeof(ReadTrackingInfo),
                [Find] = typeof(FindTrackingInfo)
            };
        }
    }
}