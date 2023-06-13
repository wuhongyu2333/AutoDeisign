using Rhino;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;
using System;
using System.Collections.Generic;
using WhyWindowsForms;

namespace WhyRhinoPlugin
{
    public class WhyRhinoCommand : Command
    {
        public WhyRhinoCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static WhyRhinoCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "WhyRhinoCommand";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Usually commands in render plug-ins are used to modify settings and behavior.
            // The render work itself is performed by the WhyRhinoPlugin class.
            Form1 form = new Form1();
            form.Show();
            return Result.Success;
        }
    }
}
