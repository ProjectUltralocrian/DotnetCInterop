using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLib;


public class Window : IDisposable
{
    private readonly ILogger _logger;
    public Button OkButton { get; private set; } = new Button("OK");
    public Button ExitButton { get; private set; } = new Button("EXIT");
    private int _counter = 0;
    public bool Running { get; private set; } = true;
    public Window(ILogger logger)
    {
        OkButton.ButtonClickedEvent += OnButtonClicked;
        ExitButton.ButtonClickedEvent += OnButtonClicked;
        _logger = logger;
    }
    public void OnButtonClicked(object sender, ButtonEventArgs args)
    {
        if (!Running) return;
        if (sender == OkButton)
        {
            _counter++;
            _logger.Info($"Button clicked: {OkButton.Name}, Id: {args.Id}, Counter: {_counter}.");
        }
        else if (sender == ExitButton)
        {
            _logger.Info($"Button clicked: {ExitButton.Name}, Id: {args.Id}");
            _logger.Warn("Exiting, goodbye...");
            Running = false;
        }
        else
        {
            throw new Exception($"Unknown button raised ButtonClickedEvent: {sender}");
        }
    }
    public void Dispose()
    {
        Console.WriteLine("Window disposed.");
        OkButton.ButtonClickedEvent -= OnButtonClicked;
        ExitButton.ButtonClickedEvent -= OnButtonClicked;
    }
}