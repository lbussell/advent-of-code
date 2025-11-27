// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace AdventOfCode.Cli.Client;

internal sealed record AdventOfCodeOptions
{
    [Required]
    public string BaseUrl { get; set; } = "";

    [Required]
    public string SessionToken { get; set; } = "";

    [Required]
    public string CacheDirectory { get; set; } = "";
}

// Required for trimming/AOT
[OptionsValidator]
internal partial class ValidateAdventOfCodeOptions : IValidateOptions<AdventOfCodeOptions>;
