﻿using System.Collections.Generic;

namespace ComponentAsService
{
    public interface IComponentAsServiceDiagnostics
    {
        Dictionary<string, object> GetRouteValues(Dictionary<string, object> allMvcRouteValues);
        (string, string)           GetParameters(string a, string b);
        (string, int)              GetParameters(string a, int b);
        (object, object, object)   GetParameters(object a, object b, object c);
        object[]                   GetParameters(params object[] args);
        int                        GetParameterCount(string a);
        int                        GetParameterCount(string a, string b);
        int                        GetParameterCount(object a, object b, object c);
        int                        GetParameterCount(params object[] args);
    }
    
    
    public class ComponentAsServiceDiagnostics : IComponentAsServiceDiagnostics 
    {
        public Dictionary<string, object> GetRouteValues(Dictionary<string, object> allMvcRouteValues) => allMvcRouteValues;
        public (string, string)           GetParameters(string a, string b)                             => (a, b);
        public (string, int)              GetParameters(string a, int b)                                => (a, b);
        public (object, object, object)   GetParameters(object a, object b, object c)                   => (a, b, c);
        public object[]                   GetParameters(params object[] args)                           => args;
        public int GetParameterCount(string a) => 1;
        public int GetParameterCount(string a, string b) => 2;
        public int GetParameterCount(object a, object b, object c) => 3;
        public int GetParameterCount(params object[] args) => args?.Length ?? -1;
    }
    
    public class ComponentAsServiceDiagnosticsB
    {
        public Dictionary<string, object> ReturnParameters(Dictionary<string, object> args) => args;
    }
}