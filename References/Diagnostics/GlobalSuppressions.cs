// THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.

// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project. 
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc. 
//
// To add a suppression to this file, right-click the message in the 
// Error List, point to "Suppress Message(s)", and click 
// "In Project Suppression File". 
// You do not need to add suppressions to this file manually. 

using System.Diagnostics.CodeAnalysis;

[assembly :
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes",
        Scope = "namespace", Target = "Ukadc.Diagnostics.Filters")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes",
        Scope = "namespace", Target = "Ukadc.Diagnostics.Listeners")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes",
        Scope = "namespace", Target = "Ukadc.Diagnostics.Utils")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly",
        MessageId = "Multi", Scope = "type", Target = "Ukadc.Diagnostics.Filters.MultiFilter")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly",
        MessageId = "Utils", Scope = "type", Target = "Ukadc.Diagnostics.Utils.TraceUtils")]
[assembly :
    SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly",
        MessageId = "Utils", Scope = "namespace", Target = "Ukadc.Diagnostics.Utils")]
[assembly :
    SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays",
        Scope = "member", Target = "Ukadc.Diagnostics.Listeners.InMemoryTraceObject.#Data")]
[assembly :
    SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames")]
[assembly :
    SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly",
        Scope = "member", Target = "Ukadc.Diagnostics.ExtendedSource+MethodProfiler.#System.IDisposable.Dispose()")]
[assembly :
    SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly",
        Scope = "member",
        Target = "Ukadc.Diagnostics.Utils.RelatedActivityIdStore+RemoveOnDispose.#System.IDisposable.Dispose()")]