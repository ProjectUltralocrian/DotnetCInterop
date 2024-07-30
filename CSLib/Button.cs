using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSLib;

public class Button(string name)
{
    public string Name { get; set; } = name;

    public delegate void ButtonClicked(object sender, ButtonEventArgs args);
    public event ButtonClicked? ButtonClickedEvent;
    public void Click()
    {
        ButtonClickedEvent?.Invoke(this, new ButtonEventArgs(Name));
    }
}

public class ButtonEventArgs(string id) : EventArgs
{
    public string Id { get; private set; } = id;
}
