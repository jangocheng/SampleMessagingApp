// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using DotLiquid;
using SampleMessagingApp.Templates.Model;

namespace SampleMessagingApp.Templates.Engine
{
    public class TemplateEngine : ITemplateEngine
    {
        public string Render(TemplateData source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Template template = Template.Parse(source.Template);
                       
            template.MakeThreadSafe();

            return template.Render(Hash.FromAnonymousObject(source.Parameters));
        }
    }
}
