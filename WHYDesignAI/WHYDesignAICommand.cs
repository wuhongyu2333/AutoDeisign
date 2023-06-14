using Rhino;
using Rhino.Commands;
using myWindowsForms;

namespace WHYDesignAI
{
    public class WHYDesignAICommand : Command
    {
        public WHYDesignAICommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static WHYDesignAICommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "WHYDesignAI";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Form1 form = new Form1();
            form.Show();
            return Result.Success;
        }
    }
}
