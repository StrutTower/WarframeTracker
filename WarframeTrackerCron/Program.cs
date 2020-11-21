using System;
using System.Collections.Generic;
using WesternStateHospital.WSHUtilities;

namespace WarframeTrackerCron {
    class Program {
        static void Main(string[] args) {
#if DEBUG
            //new Utilities.PushBulletHelper().SendNotificationToCellPhone("testing new code");
            //System.Threading.Thread.Sleep(10000);
            //Environment.Exit(0);
#endif

            bool validArgumentsFound = false;
            Dictionary<string, AppTask> tasks = AppTask.GetTasks();
            if (args.SafeAny()) {
                foreach (var arg in args) {
                    if (tasks.ContainsKey(arg)) {
                        validArgumentsFound = true;
                        AppTask task = tasks[arg];
                        IAppTask taskObject = (IAppTask)Activator.CreateInstance(task.Type);
                        taskObject.StartTask();
                    }
                }
            }

            if (!validArgumentsFound) {
                DisplayHelpMessage(tasks);
                Environment.Exit(160);
            }
        }

        private static void DisplayHelpMessage(Dictionary<string, AppTask> tasks) {
            Console.WriteLine("No valid arguments found. Valid arguments are:");
            Console.WriteLine();

            foreach (var task in tasks) {
                Console.WriteLine("  " + task.Key);
                Console.WriteLine("        " + task.Value.Description);
                Console.WriteLine();
            }
        }
    }
}