using System;
namespace ObjectBrowser.MetadataMenu
{
    public static class FormatingUtils
    {
        public static string CreatePrintFormat(int maxNameLen)
        {
            return "\t{0,-" + maxNameLen.ToString() + "}: {1}";
        }
    }
}
