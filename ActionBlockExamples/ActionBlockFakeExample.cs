using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ActionBlockExamples
{
        internal class ActionBlockFakeExample
        {
                internal async void Example()
                {
                        var actionBlock = new ActionBlock<string>(async fileName =>
                        {
                                Console.WriteLine($"Начата обработка: {fileName}");
                                await Task.Delay(1000);
                                Console.WriteLine($"Завершена обработка: {fileName}");
                        }, new ExecutionDataflowBlockOptions
                        {
                                MaxDegreeOfParallelism = 2 // Processing 2 entity in parallel 
                        });

                        string[] imageFiles = { "image1.jpg", "image2.png", "image3.bmp", "image4.gif" };

                        foreach (var file in imageFiles)
                        {
                                actionBlock.Post(file);
                        }

                        actionBlock.Complete();
                        await actionBlock.Completion;
                }
        }
}
