// Copyright 2024, Google LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using Google.Cloud.Tools.ApiIndex.V1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Google.Cloud.Tools.ReportGenerator;

/// <summary>
/// Stats for a single API
/// </summary>
public class ApiStats
{
    [JsonProperty("id")]
    public string Id { get; }

    [JsonProperty("methods")]
    public int Methods { get; }

    [JsonProperty("services")]
    public int Services { get; }

    [JsonProperty("requestDepths")]
    public OrderedDictionary<int, int> RequestDepths { get; }

    [JsonProperty("responseDepths")]
    public OrderedDictionary<int, int> ResponseDepths { get; }

    [JsonProperty("maxRequestDepth")]
    public int MaxRequestDepth { get; }

    [JsonProperty("maxResponseDepth")]
    public int MaxResponseDepth { get; }

    [JsonProperty("worstRequestDepthMethods")]
    public OrderedDictionary<string, int> WorstRequestDepthMethods { get; }
    [JsonProperty("worstResponseDepthMethods")]
    public OrderedDictionary<string, int> WorstResponseDepthMethods { get; }

    private ApiStats(Api api)
    {
        var allMethods = api.Services.SelectMany(s => s.Methods).ToList();

        Id = api.Id;
        Methods = allMethods.Count;
        Services = api.Services.Count;
        RequestDepths = Counter<int>.CreateHistogram(allMethods.Select(m => m.RequestDepth));
        ResponseDepths = Counter<int>.CreateHistogram(allMethods.Select(m => m.ResponseDepth));
        MaxRequestDepth = allMethods.Select(m => m.RequestDepth).DefaultIfEmpty().Max();
        MaxResponseDepth = allMethods.Select(m => m.ResponseDepth).DefaultIfEmpty().Max();

        WorstRequestDepthMethods = new(allMethods.OrderByDescending(m => m.RequestDepth)
            .Take(5).Select(m => KeyValuePair.Create(m.FullName, m.RequestDepth)));
        WorstResponseDepthMethods = new(allMethods.OrderByDescending(m => m.ResponseDepth)
            .Take(5).Select(m => KeyValuePair.Create(m.FullName, m.ResponseDepth)));
    }

    public static ApiStats FromApi(Api api) => new(api);
}
