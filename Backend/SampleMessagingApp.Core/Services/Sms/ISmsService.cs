// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace SampleMessagingApp.Core.Services.Sms
{
    public interface ISmsService
    {
        Task SendSmsAsync(string number, string message);
    }
}