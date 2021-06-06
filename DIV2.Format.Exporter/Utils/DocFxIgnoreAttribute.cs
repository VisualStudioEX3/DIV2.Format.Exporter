using System;

namespace DIV2.Format.Exporter.Utils
{
    /// <summary>
    /// Ignore this element on DocFX build process.
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    [DocFxIgnore]
    internal class DocFxIgnoreAttribute : Attribute
    {
    }
}
