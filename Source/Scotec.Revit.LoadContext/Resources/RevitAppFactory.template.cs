﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autodesk.Revit.UI;
using Scotec.Revit.LoadContext;

namespace {0}
{
    public class {1}Factory : IExternalApplication
    {
        public static AddinLoadContext Context { get; }
        private IExternalApplication _instance;
        private static Assembly s_assembly;
                                    
        static {1}Factory()
        {
            var location = Assembly.GetExecutingAssembly().Location;
            var path = Path.GetDirectoryName(location)!;
                                    
            Context = new AddinLoadContext(path);
            s_assembly = Context.LoadFromAssemblyPath(location);
        }
                                        
        public {1}Factory()
        {
            var types = s_assembly.GetTypes();
            var t = types.First(type => type.Name == "{1}");
            _instance = (IExternalApplication)Activator.CreateInstance(t);
        }
                                    
        public Result OnStartup(UIControlledApplication application)
        {
            return _instance.OnStartup(application);
        }
                                    
        public Result OnShutdown(UIControlledApplication application)
        {
            return _instance.OnShutdown(application);
        }
    }
}
