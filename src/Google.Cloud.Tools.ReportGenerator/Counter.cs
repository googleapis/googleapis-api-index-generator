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

public class Counter<T>
{
    [JsonProperty("value")]
    public T Value { get; }
    [JsonProperty("count")]
    public int Count { get; }

    public Counter(T value, int count) =>
        (Value, Count) = (value, count);

    /// <summary>
    /// Creates a histogram from a sequence of values. The returned dictionary is
    /// ordered by count, descending.
    /// </summary>
    public static OrderedDictionary<T, int> CreateHistogram(IEnumerable<T> values) =>
        new(values.GroupBy(v => v)
            .Select(g => new Counter<T>(g.Key, g.Count()))
            .OrderByDescending(c => c.Count)
            .Select(c => KeyValuePair.Create(c.Value, c.Count)));
}
