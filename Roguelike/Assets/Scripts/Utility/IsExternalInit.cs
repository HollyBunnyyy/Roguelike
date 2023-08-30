// Allows us to use to init setters with record types until Unity ports over to .net 5.0
// Super useful for meta data inheritance.
// https://stackoverflow.com/questions/64749385/predefined-type-system-runtime-compilerservices-isexternalinit-is-not-defined

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit {};
}