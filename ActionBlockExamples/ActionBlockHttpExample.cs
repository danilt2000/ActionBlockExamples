using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ActionBlockExamples
{
        internal class ActionBlockHttpExample
        {
                internal async void Example()
                {

                        var actionBlock = new ActionBlock<string>(async url =>
                        {
                                using (HttpClient client = new HttpClient())
                                {
                                        Console.WriteLine($"Начало скачивания: {url}");
                                        var data = await client.GetStringAsync(url);
                                        Console.WriteLine($"Завершено скачивание: {url}, длина данных: {data.Length}");
                                }
                        }, new ExecutionDataflowBlockOptions
                        {
                                MaxDegreeOfParallelism = 3 // Download 3 entity in parallel 
                        });

                        string[] urls =
                        {
                                "https://example.com/file1",
                                "https://example.com/file2",
                                "https://example.com/file3",
                                "https://example.com/file4"
                        };

                        foreach (var url in urls)
                        {
                                actionBlock.Post(url);
                        }

                        actionBlock.Complete();
                        await actionBlock.Completion;

                }

        }
}
