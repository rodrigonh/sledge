﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogicAndTrick.Gimme;
using LogicAndTrick.Oy;
using Sledge.Common.Commands;
using Sledge.Common.Context;
using Sledge.Common.Documents;
using Sledge.Common.Hotkeys;
using Sledge.Common.Menu;

namespace Sledge.MinimalEditor.TextEditor
{
    [Export(typeof(ICommand))]
    [CommandID("File:Open")]
    [DefaultHotkey("Ctrl+O")]
    [MenuItem("File", "", "File")]
    public class OpenFile : ICommand
    {
        public string Name => "Open";
        public string Details => "Open...";

        public bool IsInContext(IContext context)
        {
            return true;
        }

        public async Task Invoke(CommandParameters parameters)
        {
            using (var ofd = new OpenFileDialog() { Filter = "All files|*.*"})
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var loader = await Gimme.FetchOne<IDocumentLoader>(ofd.FileName, "");
                    if (loader != null)
                    {
                        var doc = await loader.Load(ofd.FileName);
                        if (doc != null)
                        {
                            await Oy.Publish("Document:Opened", doc);
                        }
                    }
                }
            }
        }
    }
}