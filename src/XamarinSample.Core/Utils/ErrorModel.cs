// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace XamarinSample.Core.Utils
{
    public class ErrorModel
    {
        public Exception Exception { get; }
        public string Comment { get; }

        public ErrorModel(Exception exception, string comment = null)
        {
            Exception = exception;
            Comment = comment;
        }
    }
}
