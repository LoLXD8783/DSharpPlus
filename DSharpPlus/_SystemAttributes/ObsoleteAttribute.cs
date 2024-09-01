// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#if !NET7_0_OR_GREATER
namespace System;

/// <summary>
/// Marks program elements that are no longer in use.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum |
    AttributeTargets.Interface | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Delegate,
    Inherited = false)]
internal sealed class ObsoleteAttribute : Attribute
{
    public ObsoleteAttribute()
    {
    }

    public ObsoleteAttribute(string? message) => this.Message = message;

    public ObsoleteAttribute(string? message, bool error)
    {
        this.Message = message;
        this.IsError = error;
    }

    public string? Message { get; }

    public bool IsError { get; }

    public string? DiagnosticId { get; set; }

    public string? UrlFormat { get; set; }
}
#endif 
