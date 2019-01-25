using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Contentful.Core;
using Contentful.Core.Models.Management;

namespace ContentfulExtentionParametersUpdate
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create your extension in Contentful, then run this. It will create a new one which you'll need to amend. I know, it's weird.
            await Create();
        }

        public static async Task Create()
        {
            try
            {
                var httpClient = new HttpClient();

                var client = new ContentfulManagementClient(httpClient,
                    "<content_management_api_key>", "<space_id>");

                var extension = await client.GetExtension("<extension_id>");

                extension.SystemProperties.Version = extension.SystemProperties.Version + 1;

                extension.Parameters = new UiExtensionParametersLists
                {
                    InstanceParameters = new List<UiExtensionParameters>
                    {
                        new UiExtensionParameters
                        {
                            // Update these fields as required
                            Id = "sections",
                            Name = "Section names",
                            Type = "Symbol",
                            Required = false,
                            Description = "Define the sections that will be re-ordered. The list is separated by commas."
                        }
                    }
                };

                extension.SystemProperties.Id = null;
                extension.Src = "https://www.google.com";
                extension.SrcDoc = null;

                // You will need to over-ride the srcdoc after you run this

                await client.CreateExtension(extension);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
            
    }
}
