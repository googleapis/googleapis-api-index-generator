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

using Newtonsoft.Json;

namespace Google.Cloud.Tools.ReportGenerator;

public class OverallStats
{
    public int Apis { get; }
    public int Services { get; }
    public int Methods { get; }

    [JsonProperty("maxRequestDepths")]
    public OrderedDictionary<int, int> MaxRequestDepths { get; }
    [JsonProperty("maxResponseDepths")]
    public OrderedDictionary<int, int> MaxResponseDepths { get; }

    [JsonProperty("worstMaxRequestDepthApis")]
    public OrderedDictionary<string, int> WorstMaxRequestDepthApis { get; }
    [JsonProperty("worstMaxResponseDepthApis")]
    public OrderedDictionary<string, int> WorstMaxResponseDepthApis { get; }

    private OverallStats(IEnumerable<ApiStats> apiStats)
    {
        apiStats = apiStats.ToList();
        Apis = apiStats.Count();
        Services = apiStats.Sum(api => api.Services);
        Methods = apiStats.Sum(api => api.Methods);
        MaxRequestDepths = Counter<int>.CreateHistogram(apiStats.Select(api => api.MaxRequestDepth));
        MaxResponseDepths = Counter<int>.CreateHistogram(apiStats.Select(api => api.MaxResponseDepth));

        WorstMaxRequestDepthApis = new(apiStats.OrderByDescending(api => api.MaxRequestDepth)
            .Take(10).Select(api => KeyValuePair.Create(api.Id, api.MaxRequestDepth)));
        WorstMaxResponseDepthApis = new(apiStats.OrderByDescending(api => api.MaxResponseDepth)
            .Take(10).Select(api => KeyValuePair.Create(api.Id, api.MaxResponseDepth)));
    }

    public static OverallStats FromApiStats(IEnumerable<ApiStats> apiStats) => new(apiStats);
}
