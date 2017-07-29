﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System.Windows.Forms;

namespace CurtainFireMakerPlugin
{
    internal class PythonRunner
    {
        private ScriptEngine Engine { get; }
        private ScriptScope RootScope { get; }

        public PythonRunner(string settingScriptPath, string modullesDirPath)
        {
            Engine = Python.CreateEngine();
            RootScope = Engine.CreateScope();

            ICollection<string> paths = Engine.GetSearchPaths();
            paths.Add(modullesDirPath);
            Engine.SetSearchPaths(paths);

            Engine.Execute(
            "# -*- coding: utf-8 -*-\n" +
            "import sys\n" +
            "sys.path.append(r\"" + Application.StartupPath + "\\Plugins\")\n" +
            "import clr\n" +
            "clr.AddReference(\"CurtainFireMakerPlugin\")\n" +
            "clr.AddReference(\"CsPmx\")\n" +
            "clr.AddReference(\"CsVmd\")\n" +
            "clr.AddReference(\"DxMath\")\n" +
            "clr.AddReference(\"VecMath\")\n" +
            "clr.AddReference(\"MikuMikuPlugin\")\n", RootScope);

            ScriptScope scope = Engine.CreateScope(RootScope);

            Engine.ExecuteFile(settingScriptPath, scope);
        }

        public void RunSpellScript(string path)
        {
            ScriptScope scope = Engine.CreateScope(RootScope);

            Engine.ExecuteFile(path, scope);
        }

        public void SetOut(Stream stream)
        {
            Engine.Runtime.IO.SetOutput(stream, Encoding.ASCII);
        }

        public string FormatException(Exception e) => Engine.GetService<ExceptionOperations>().FormatException(e);
    }
}
