﻿// Copyright © 2023 - 2024 Olaf Meyer
// Copyright © 2023 - 2024 scotec Software Solutions AB, www.scotec-software.com
// This file is licensed to you under the MIT license.

using System;
using Autodesk.Revit.UI;
using Microsoft.Extensions.DependencyInjection;

namespace Scotec.Revit;

/// <summary>
///     Basic implementation for a Revit app. Derive from this class to use services like logging, dependency injection, or
///     hosted services.
/// </summary>
public abstract class RevitApp : RevitAppBase, IExternalApplication
{
    /// <summary>
    /// </summary>
    public UIControlledApplication Application { get; private set; }

    /// <inheritdoc />
    Result IExternalApplication.OnStartup(UIControlledApplication application)
    {
        Application = application;
        var configureServices = new Action<IServiceCollection>(services =>
        {
            services.AddSingleton(application);
            services.AddSingleton(application.ActiveAddInId);
            services.AddSingleton(application.ControlledApplication);

        });

        return OnStartup(application.ActiveAddInId, configureServices) 
            ? Result.Succeeded 
            : Result.Failed;
    }

    /// <inheritdoc />
    Result IExternalApplication.OnShutdown(UIControlledApplication application)
    {
        return OnShutdown(application.ControlledApplication) ? Result.Succeeded : Result.Failed;
    }
}
