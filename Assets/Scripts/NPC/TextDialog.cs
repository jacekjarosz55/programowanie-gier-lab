using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DialogOption;


public class DialogOption
{
    public delegate void DialogAction(Player player);
    public string text;
    public DialogAction action;
    public DialogOption(string text, DialogAction action)
    {
        this.text = text;
        this.action = action;
    }
}
public class TextDialog
{
    public string text;
    public List<DialogOption> options;
    public TextDialog(string text, List<DialogOption> options)
    {
        this.text = text;
        this.options = options;
    }
}
