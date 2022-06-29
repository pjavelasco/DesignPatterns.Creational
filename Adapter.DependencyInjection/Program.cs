using Autofac;
using Autofac.Features.Metadata;
using System;
using System.Collections.Generic;

namespace Creational.Adapter.DependencyInjection
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            // for each ICommand, a ToolbarButton is created to wrap it, and all are passed to the editor
            var b = new ContainerBuilder();
            b.RegisterType<OpenCommand>()
              .As<ICommand>()
              .WithMetadata("Name", "Open");
            b.RegisterType<SaveCommand>()
              .As<ICommand>()
              .WithMetadata("Name", "Save");
            //b.RegisterType<Button>();
            b.RegisterAdapter<ICommand, Button>(cmd => new Button(cmd, ""));
            b.RegisterAdapter<Meta<ICommand>, Button>(cmd =>
              new Button(cmd.Value, (string)cmd.Metadata["Name"]));
            b.RegisterType<Editor>();

            using var c = b.Build();
            var editor = c.Resolve<Editor>();
            editor.ClickAll();

            // problem: only one button

            foreach (var btn in editor.Buttons)
                btn.PrintMe();
        }

        interface ICommand
        {
            void Execute();
        }

        class SaveCommand : ICommand
        {
            public void Execute()
            {
                Console.WriteLine("Save command");
            }
        }

        class OpenCommand : ICommand
        {
            public void Execute()
            {
                Console.WriteLine("Open command");
            }
        }

        class Button
        {
            private readonly ICommand _command;
            private readonly string _name;

            public Button(ICommand command, string name)
            {
                _command = command ?? throw new ArgumentNullException(nameof(command));
                _name = name;
            }

            public void Click()
            {
                _command.Execute();
            }

            public void PrintMe()
            {
                Console.WriteLine($"I am a button called {_name}");
            }
        }

        class Editor
        {
            public Editor(IEnumerable<Button> buttons)
            {
                Buttons = buttons ?? throw new ArgumentNullException(nameof(buttons));
            }

            public IEnumerable<Button> Buttons { get; }

            public void ClickAll()
            {
                foreach (var button in Buttons)
                {
                    button.Click();
                }
            }
        }
    }
}
