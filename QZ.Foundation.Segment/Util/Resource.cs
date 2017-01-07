using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace QZ.Foundation.Segment
{
    public class Resource
    {
        public static readonly string[] Area_Lvls = new string[]
        {
            "国",
            "省",
            "市",
            "县",
            "区",
            "镇",
            "村"
        };

        private static AreaNode _root;
        public static AreaNode Root
        {
            get
            {
                if (_root == null)
                    _root = AreaTree_Load();
                return _root;
            }
        }

        public static AreaNode AreaTree_Load() =>
            AreaTree_Load(AppDomain.CurrentDomain.BaseDirectory + "Resource\\Area.txt");

        public unsafe static AreaNode AreaTree_Load(string path)
        {
            var root = new AreaNode();
            if (!File.Exists(path)) return root;

            var strings = File.ReadAllLines(path);
            foreach(var str in strings)
            {
                if (string.IsNullOrWhiteSpace(str)) continue;

                fixed(char * ptr = str)
                {
                    Node_Fill(ptr, root);
                }
            }
            return root;
        }

        /// <summary>
        /// Fill the char pointer's content into a given area node
        /// Be caution to make sure tail-recurse optimization is implementation
        /// Can C# do it for us?
        /// </summary>
        /// <param name="ptr"></param>
        /// <param name="node"></param>
        private unsafe static void Node_Fill(char * ptr, AreaNode node)
        {
            char c = *ptr;
            if (c == '省' || c == '市' || c == '县' || c == '\0') return;

            if(node.Areas.ContainsKey(c))
            {
                Node_Fill(++ptr, node.Areas[c]);
            }
            else
            {
                var subNode = new AreaNode();
                node.Areas[c] = subNode;
                Node_Fill(++ptr, subNode);
            }
        }


        
    }
}
