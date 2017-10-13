// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System;

namespace XamarinSample.Core.Utils
{
    public class ErrorOr<TSuccess> where TSuccess : class 
    {
        public ErrorModel Error { get; }
        public TSuccess Success { get; }

        public bool IsSuccess => Success != null;
        public bool IsError => Error != null;

        public ErrorOr(TSuccess success)
        {
            Success = success ?? throw new ArgumentNullException(nameof(success));
        }
        public ErrorOr(ErrorModel error)
        {
            Error = error ?? throw new ArgumentNullException(nameof(error));
        }
    }
}
