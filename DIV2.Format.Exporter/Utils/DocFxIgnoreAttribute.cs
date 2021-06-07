using System;

namespace DIV2.Format.Exporter.Utils
{
    /// <summary>
    /// Ignore this element on DocFX build process.
    /// </summary>
    [DocFxIgnore, AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    internal class DocFxIgnoreAttribute : Attribute
    {
    }
}
