using Grasshopper.Kernel;
using System;
using System.Drawing;

namespace gh2
{
    public class gh2Info : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "gh2";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("e371403a-0e5d-4b3d-a62e-b7ec1fbf3a50");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
