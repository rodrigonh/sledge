using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Sledge.BspEditor.Documents;
using Sledge.BspEditor.Modification;
using Sledge.BspEditor.Modification.Operations;
using Sledge.BspEditor.Primitives.MapObjects;
using Sledge.Common.Shell.Commands;
using Sledge.Common.Shell.Hotkeys;
using Sledge.Common.Shell.Menu;

namespace Sledge.BspEditor.Commands.Modification
{
    [Export(typeof(ICommand))]
    [CommandID("BspEditor:Edit:SelectNone")]
    [DefaultHotkey("Shift+Q")]
    [MenuItem("Edit", "", "Selection", "D")]
    public class SelectNone : BaseCommand
    {
        public override string Name => "Select None";
        public override string Details => "Clear selection";
        protected override Task Invoke(MapDocument document, CommandParameters parameters)
        {
            var op = new Deselect(document.Map.Root.FindAll());
            return MapDocumentOperation.Perform(document, op);
        }
    }
}