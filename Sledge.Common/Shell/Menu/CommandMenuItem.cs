﻿using System.Threading.Tasks;
using LogicAndTrick.Oy;
using Sledge.Common.Shell.Commands;
using Sledge.Common.Shell.Context;

namespace Sledge.Common.Shell.Menu
{
    public class CommandMenuItem : IMenuItem
    {
        private readonly ICommand _command;

        public string ID => "Command:" + _command.GetID();
        public string Name => _command.Name;
        public string Description => _command.Details;
        public string Section { get; }
        public string Path { get; }
        public string Group { get; }
        public string OrderHint { get; }

        public CommandMenuItem(ICommand command, string section, string path, string group, string orderHint)
        {
            Section = section;
            Path = path;
            Group = group;
            OrderHint = orderHint;
            _command = command ?? this as ICommand;
        }

        public bool IsInContext(IContext context)
        {
            return _command.IsInContext(context);
        }

        public async Task Invoke()
        {
            await Oy.Publish("Command:Run", new CommandMessage(_command.GetID()));
        }
    }
}