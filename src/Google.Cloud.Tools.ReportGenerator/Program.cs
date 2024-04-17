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

using Google.Cloud.Tools.ReportGenerator;
using Newtonsoft.Json;
using Index = Google.Cloud.Tools.ApiIndex.V1.Index;

if (args.Length != 2)
{
    Console.WriteLine("Arguments: <api-index> <output-file>");
    return 1;
}

string apiIndexJson = File.ReadAllText(args[0]);
var apiIndex = Index.Parser.ParseJson(apiIndexJson);
var report = Report.FromIndex(apiIndex);
var reportJson = JsonConvert.SerializeObject(report, Formatting.Indented);
File.WriteAllText(args[1], reportJson);

return 0;