// Guids.cs
// MUST match guids.h
using System;

namespace JosePedroSilva.TFSScrumExtensions
{
    static class GuidList
    {
        public const string guidTFSScrumExtensionsPkgString = "59e02031-1f61-4fba-9c44-b40543411b8d";
        public const string guidTFSScrumExtensionsCmdSetString = "82030a81-71db-473d-8544-cbcf9e09f540";

        public static readonly Guid guidTFSScrumExtensionsCmdSet = new Guid(guidTFSScrumExtensionsCmdSetString);
    };
}