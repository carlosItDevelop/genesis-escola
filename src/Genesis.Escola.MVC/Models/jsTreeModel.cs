using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Genesis.Escola.MVC.Models
{
    public class JsTreeRoot
    {
        public string text;
        public JsTreeModel[] children;
    }
    public class JsTreeModel
    {
        public string text;
        public string id;
        public string parent { get; set; }
        // public JsTreeAttribute attr;
        public JsTreeState state;
        //  public JsTreeModel[] children;
        public JsTreeChildren[] children;
    }

    public class JsTreeAttribute
    {
        public string id;
        public bool selected;
    }

    public class JsTreeState
    {
        public bool opened;
        public bool selected;
        public bool disabled;
    }
    public class JsTreeChildren
    {
        public string id;
        public string text;
        public JsTreeState state;
        //  public bool disabled;
    }
}
