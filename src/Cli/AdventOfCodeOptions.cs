// SPDX-FileCopyrightText: Copyright (c) 2025 Logan Bussell
// SPDX-License-Identifier: MIT

namespace AdventOfCode.Cli;

internal sealed record AdventOfCodeOptions
{
    public string BaseUrl { get; set; } = "";
    public string SessionToken { get; set; } = "";
}
