using System;
using System.IO;
using Mono.Cecil;

namespace NETRenamer
{
    class Program
    {

        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Enclose artifact path on args with double-quote.");
                return;
            }
            string path = args[0];
            foreach (string entry in Directory.GetFiles(path))
            {
                try
                {
                    FileInfo fi = new FileInfo(entry);
                    AssemblyDefinition ass = AssemblyDefinition.ReadAssembly(entry);
                    ModuleDefinition mdl = ModuleDefinition.ReadModule(entry);
                    string at = ass.Name.Name.ToString();
                    string ext = ".";
                    switch (mdl.Kind)
                    {
                        case ModuleKind.Console:
                        case ModuleKind.Windows:
                            ext += "exe";
                            break;
                        case ModuleKind.Dll:
                            ext += "dll";
                            break;
                        default:
                            ext = "";
                            break;
                    }
                    Console.WriteLine(at);
                    ass.Dispose();
                    mdl.Dispose();
                    fi.MoveTo(fi.Directory.FullName + "\\" + at + ext);
                } catch (Exception e)
                {
                    Console.WriteLine("Error processing {0}: {1}", entry, e.Message);
                }
            }
        }
    }
}
