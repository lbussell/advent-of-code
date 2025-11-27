// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AdventOfCode.Cli;

internal static class AdventOfCodeClientExtensions
{
    public static HostApplicationBuilder AddAdventOfCodeClient(this HostApplicationBuilder builder)
    {
        // This registers IOptions<AdventOfCodeOptions> for dependency injection.
        // We also need to set property EnableConfigurationBindingGenerator to true or else we will
        // get trimming/AOT warnings.
        var aocOptionsSection = builder.Configuration.GetSection(nameof(AdventOfCodeOptions));
        builder.Services.Configure<AdventOfCodeOptions>(aocOptionsSection);

        builder.Services.AddHttpClient<AdventOfCodeClient>((serviceProvider, httpClient) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<AdventOfCodeOptions>>().Value;
            httpClient.BaseAddress = new Uri(options.BaseUrl);
            httpClient.DefaultRequestHeaders.Add("Cookie", $"session={options.SessionToken}");
        });

        return builder;
    }
}
