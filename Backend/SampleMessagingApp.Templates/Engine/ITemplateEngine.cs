// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using SampleMessagingApp.Templates.Model;

namespace SampleMessagingApp.Templates.Engine
{
    public interface ITemplateEngine
    {
        string Render(TemplateData template);
    }
}
